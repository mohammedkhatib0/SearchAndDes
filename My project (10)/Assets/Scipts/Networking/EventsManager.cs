using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventsManager : MonoBehaviourPun, IOnEventCallback
{
    public static EventsManager instance;


    private void Awake()
    {
        if (!instance)
        {
            instance = this;
            PhotonNetwork.AddCallbackTarget(this);
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void OnEvent(EventData photonEvent)
    {
        // events here
        switch (photonEvent.Code)
        {
            case CONSTANTS.EVENTS.ISREADY :

                PhotonNetwork.LocalPlayer.IsReady = true;
                Debug.Log("IS READY EVENT SENT");
                //Here set the player is ready
                break;
            case CONSTANTS.EVENTS.START_GAME:
                Debug.Log("START GAME EVENT SENT");
                break;
            default:
                break;
        }
    }

    public void SendEvent(byte eventCode, object[] param)
    {
        foreach (var player in PhotonNetwork.PlayerList)
        {
            PhotonNetwork.RaiseEvent(eventCode, param,
                                    new RaiseEventOptions
                                    {
                                        TargetActors = new[] { player.ActorNumber },
                                        Receivers = ReceiverGroup.All
                                    },
                                    SendOptions.SendReliable);
        }
    }
}

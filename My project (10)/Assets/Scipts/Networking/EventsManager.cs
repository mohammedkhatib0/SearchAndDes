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
        Player playerSender = null;
        // events here
        switch (photonEvent.Code)
        {
            case CONSTANTS.EVENTS.ISREADY:
                Debug.Log("IS READY EVENT SENT"); 
                foreach(Player p in PhotonNetwork.PlayerList)
                {
                    if(p.ActorNumber==photonEvent.Sender)
                    {
                        playerSender = p;
                        break;
                    }
                }
                if(playerSender!=null)
                GetComponent<PhotonView>().RPC("SetReady", RpcTarget.All,playerSender);
                break;
            case CONSTANTS.EVENTS.START_GAME:
                Debug.Log("START GAME EVENT SENT");
                break;
            default:
                break;
        }
    }
    [PunRPC]
    void SetReady(Player player)
    {
        player.IsReady = true;
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

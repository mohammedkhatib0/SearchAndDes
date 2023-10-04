using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNetwork : MonoBehaviourPun, IPunObservable
{
    public bool isReady = false;
    public int Actor;
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(isReady);
        }
        else
        {
            isReady = (bool)stream.ReceiveNext();
        }
    }
    private void Start()
    {
        GRoomInfo.instance.Players.Add(this);
    }
}
using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;

public class PlayerNetwork : MonoBehaviourPunCallbacks,IPunObservable
{
	Vector3 targetPos;

	PhotonView PV;
	public static PlayerNetwork instance { get; set; }
	public bool IsReady;
	public Dictionary<int, PlayerNetwork> PlayerList = new Dictionary<int, PlayerNetwork>();
	public int Actor;
	private void Awake()
	{
		if (!instance)
		{
			instance = this;
		}

		else
		{
			//Destroy(this.gameObject);
		}
	}
	// Use this for initialization
	void Start()
	{
		PV = GetComponent<PhotonView>();
		PhotonNetwork.SendRate = 60;
		PhotonNetwork.SerializationRate = 30;
		Actor = PhotonNetwork.LocalPlayer.ActorNumber;
		IsReady = true;
	}

	// Update is called once per frame
	void Update()
	{
		if (!PV.IsMine)
		{
			//smoothMove();
		}
	}

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
			stream.SendNext(IsReady);
        }
        if (stream.IsReading)
        {
			IsReady = (bool)stream.ReceiveNext();
        }
    }
}
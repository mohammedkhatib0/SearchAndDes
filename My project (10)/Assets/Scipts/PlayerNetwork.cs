using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;

public class PlayerNetwork : MonoBehaviourPunCallbacks,IPunObservable
{

	PhotonView PV;
	public static PlayerNetwork instance { get; set; }
	public bool IsReady;
	public Dictionary<int, PlayerNetwork> PlayerList = new Dictionary<int, PlayerNetwork>();
	public int Actor;


	//Values that will be synced over network
	Vector3 latestPos;
	Quaternion latestRot;
	//Lag compensation
	float currentTime = 0;
	double currentPacketTime = 0;
	double lastPacketTime = 0;
	Vector3 positionAtLastPacket = Vector3.zero;
	Quaternion rotationAtLastPacket = Quaternion.identity;
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
			//Lag compensation
			double timeToReachGoal = currentPacketTime - lastPacketTime;
			currentTime += Time.deltaTime;

			//Update remote player
			transform.position = Vector3.Lerp(positionAtLastPacket, latestPos, (float)(currentTime / timeToReachGoal));
			transform.rotation = Quaternion.Lerp(rotationAtLastPacket, latestRot, (float)(currentTime / timeToReachGoal));
		}
	}

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
		if (stream.IsWriting)
		{
			//We own this player: send the others our data
			stream.SendNext(transform.position);
			stream.SendNext(transform.rotation);
		}
		else
		{
			//Network player, receive data
			latestPos = (Vector3)stream.ReceiveNext();
			latestRot = (Quaternion)stream.ReceiveNext();

			//Lag compensation
			currentTime = 0.0f;
			lastPacketTime = currentPacketTime;
			currentPacketTime = info.SentServerTime;
			positionAtLastPacket = transform.position;
			rotationAtLastPacket = transform.rotation;
		}

	}
}
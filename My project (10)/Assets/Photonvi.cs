using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Photonvi : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
   public GameObject g;
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to Master");
        PhotonNetwork.JoinLobby();
        PhotonNetwork.AutomaticallySyncScene = true;
        
    }
    public override void OnJoinedLobby()
    {
        PhotonNetwork.JoinRandomOrCreateRoom();
    }
    public override void OnJoinedRoom()
    {
        PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "asd"), new Vector3(0, 5.77f, 0), Quaternion.identity);


    }
}

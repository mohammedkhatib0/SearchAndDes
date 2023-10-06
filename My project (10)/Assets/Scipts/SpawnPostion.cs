using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SpawnPostion : MonoBehaviour
{
	[Header("PlayerSpawn")]
	public List<Transform> AlphaTeam;
	public List<Transform> BravoTeam;

	public static SpawnPostion Instanse { get; private set; }
    private void Awake()
    {
        Instanse = this;
        //DontDestroyOnLoad(gameObject);
    }
    private void Start()
    {
        GetComponent<PhotonView>().RPC("InstantiateFunction", RpcTarget.All);
        //PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PlayerManager"), AlphaTeam[0].position, AlphaTeam[0].rotation);
       // AlphaTeam.RemoveAt(0);
    }

    [PunRPC]
    void InstantiateFunction()
    {
        PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PlayerManager"), AlphaTeam[0].position, AlphaTeam[0].rotation);
        AlphaTeam.RemoveAt(0);
    }
    //GameManager isinde map
    //Network manager script in player
    //GRoomInfo from lancher to map


}

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
    private PhotonView PV;

	public static SpawnPostion Instanse { get; private set; }
    private void Awake()
    {
        Instanse = this;
        //DontDestroyOnLoad(gameObject);
    }
    private void Start()
    {
        PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PlayerManager"), AlphaTeam[0].position, AlphaTeam[0].rotation);

        InstantiateFunction();
    }

   
    void InstantiateFunction()
    {
           // AlphaTeam.RemoveAt(Random.Range(0, AlphaTeam.Count - 1));
        
    }
    //GameManager isinde map
    //Network manager script in player
    //GRoomInfo from lancher to map


}

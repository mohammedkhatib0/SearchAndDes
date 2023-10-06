using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMapManager : MonoBehaviour
{

    public static GameMapManager instance;
    PhotonView PV;

    private void Awake()
    {
        if (!instance)
        {
            instance = this;
        }

        else
        {
            Destroy(this.gameObject);
        }
    }
    private void Start()
    {
        PV = GetComponent<PhotonView>();
        BeforeGameStarted();
    }
    void BeforeGameStarted()
    {
      
    }
    public IEnumerator WaitForPlayer()
    {
        yield return new WaitUntil(() => AllPlayersAreReady());
        Debug.Log("All player is ready");
        PV.RPC("GameStarted", RpcTarget.All);
        GameStarted();
        //Start the game
    }
    [PunRPC]
    private void GameStarted()
    {
        LoadMapCanvas.instance.Hide();
    }
  
    bool AllPlayersAreReady()
    {
        //PlayerNetwork[] photonPlayer;
        print(GRoomInfo.instance.Players.Count);
        foreach (Player player in PhotonNetwork.PlayerList)
        {
            if (!player.IsReady) return false;
        }
        return true;
        //photonPlayer = FindObjectsOfType<PlayerNetwork>();
        //if (photonPlayer.Length != PhotonNetwork.CountOfPlayers) return false;
        //foreach (PlayerNetwork player in photonPlayer)
        //{
        //    if (player == null) return false;
        //    if (!player.GetComponent<PlayerNetwork>().IsReady)
        //        return false;
        //}
        //foreach (PlayerNetwork player in photonPlayer)
        //{
        //    foreach(PlayerNetwork player1 in photonPlayer){
        //        player.PlayerList.Add(player1.Actor, player1);
        //    }
        //}
      //  return true;
    }
       // var players = PhotonNetwork.PlayerList;

    
    // when you join the scene you have to get all the players inside the scene
    // wait until you find all the players inside the scene that are == to PhotonNetwork.CurrentRoom.MaxPlayers
    // wait until all players are ready
    // start the game when all players are ready

    /*public bool AllPlayersAreReady()
    {
        // go a for loop for all players to check if they are ready
    }*/
}

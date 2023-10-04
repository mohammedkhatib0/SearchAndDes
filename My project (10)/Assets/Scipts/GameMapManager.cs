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
    public Canvas Base;
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
        Show();

    }
    void Show()
    {
        Base.enabled = true;
    }
    void Hide()
    {
        Base.enabled = false;
    }
    bool AllPlayersAreReady()
    {
        //print(GRoomInfo.instance.Players.Count);
        //foreach (PlayerNetwork player in GRoomInfo.instance.Players)
        //{
        //    if (!player.isReady) return false;
        //}
        //return true;
        foreach (var photonPlayer in PhotonNetwork.PlayerList)
        {
            print(photonPlayer.IsReady);
             if (photonPlayer == null||(bool)photonPlayer.IsReady == false) return false;
        }
        return true;
    }
    // when you join the scene you have to get all the players inside the scene
    // wait until you find all the players inside the scene that are == to PhotonNetwork.CurrentRoom.MaxPlayers
    // wait until all players are ready
    // start the game when all players are ready

    /*public bool AllPlayersAreReady()
    {
        // go a for loop for all players to check if they are ready
    }*/
}

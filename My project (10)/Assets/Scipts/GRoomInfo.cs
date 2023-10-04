using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class GRoomInfo : MonoBehaviour
{
    public static GRoomInfo instance;

    // convert to dict
    public List<PlayerNetwork> Players = new List<PlayerNetwork>();
    private void Awake()
    {
        if (!instance)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}

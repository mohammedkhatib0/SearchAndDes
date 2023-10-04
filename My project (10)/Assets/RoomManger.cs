using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
using System.IO;

public class RoomManger : MonoBehaviourPunCallbacks


{

    public static RoomManger Instance;
    // Start is called before the first frame update

    private void Awake()
    {
        if (Instance) //CHECK if onother RoomManger exits
        {
            Destroy(gameObject); //there can only be one!
            return;
        }
        DontDestroyOnLoad(gameObject); //the only one here!
        Instance = this;
    }
    public override void OnEnable()
    {
        base.OnEnable();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    public override void OnDisable()
    {
        base.OnDisable();
        SceneManager.sceneLoaded -= OnSceneLoaded;

    }
    void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
    {
        if (scene.buildIndex == 1)/// amuri make sure your scene is index 1 :D
        {
            //This is will be spawn item
            ///
            ///
            ///

            //PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PlayerManger"), Vector3.zero, Quaternion.identity);
        }
       
    }
}
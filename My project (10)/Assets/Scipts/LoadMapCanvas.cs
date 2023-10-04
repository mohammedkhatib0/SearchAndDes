using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadMapCanvas : MonoBehaviour
{
    public PhotonView PV;
    public Canvas Base;
    public static LoadMapCanvas instance;

    public TextMeshProUGUI loadSceneBarText;
    public Slider loadSceneBar;


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
        PV = GetComponent<PhotonView>();
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void Hide()
    {
        Base.enabled = false;
    }

    public void Show()
    {
        Base.enabled = true;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Map")
        {
            StartCoroutine(SceneLoaded(scene, mode));
        }

    }
    IEnumerator LoadAsynchronously(string sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
        
        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            loadSceneBarText.text = "Loading Map: " + (operation.progress * 100) + "%";

            Debug.Log(operation.progress);
            loadSceneBar.value = progress;
            yield return null;
        }

    }
    IEnumerator SceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("PlayerIsReady" + PhotonNetwork.LocalPlayer.IsReady);
        Debug.Log("Loading Scene");

        //StartCoroutine(LoadAsynchronously(scene.name));

        yield return new WaitUntil(() => scene.isLoaded);
        loadSceneBarText.text = "Loading Map:100% ";
        loadSceneBar.value = 1;
        yield return new WaitForSeconds(3);
        //    EventsManager.instance.SendEvent(CONSTANTS.EVENTS.ISREADY, null);
        PV.RPC("ChangeReadyState", RpcTarget.MasterClient, PhotonNetwork.LocalPlayer, true);
        PhotonNetwork.LocalPlayer.IsReady = true;
        //Hide();
        // Here u send an event to the other player that you are ready
        //yield return new WaitUntil(() => AllPlayersAreReady);
        Debug.Log("Scene Has Finished Loading, set player to ready");
        if(PhotonNetwork.IsMasterClient)
        StartCoroutine(GameMapManager.instance.WaitForPlayer());
        }
    [PunRPC]
    void ChangeReadyState(Player player, bool ready )
    {
        player.IsReady = ready;
    }

    }

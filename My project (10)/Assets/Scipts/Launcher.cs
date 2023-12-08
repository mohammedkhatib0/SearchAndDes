using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using Photon.Realtime;
using System.Linq;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;

public class Launcher : MonoBehaviourPunCallbacks
{


	public static Launcher Instance;
	public  TMP_InputField PlayerNameInputField;
	[SerializeField] TMP_InputField roomNameInputField;
	[SerializeField] TMP_Text errorText;
	[SerializeField] TMP_Text roomNameText;
	[SerializeField] Transform roomListContent;
	[SerializeField] GameObject roomListItemPrefab;
	[SerializeField] GameObject PlayerListItemPrefab;
	[SerializeField] GameObject startGameButton;

	[Header("Teams")]
	[SerializeField] Transform AlphaTeamContent;
	[SerializeField] Transform BravoTeamContent;
	GameObject playerRef;
	Dictionary<int, GameObject> PlayerList = new Dictionary<int, GameObject>();
	int key;

	[Header("PlayerSpawn")]
	public static List<Transform> AlphaTeam;
	public static List<Transform> BravoTeam;
	private static List<Transform> AlphaTeamRef;
	private static List<Transform> BravoTeamRef;

	[Header("UI")]
	[SerializeField] GameObject LoadingMapCanvas;
	[SerializeField] Slider loadSceneBar;
	[SerializeField] TextMeshProUGUI loadSceneBarText;

	PhotonView PV;



	void Awake()
	{
		Instance = this;
		//DontDestroyOnLoad(LoadingMapCanvas);
		PV = GetComponent<PhotonView>();
		
	}
	void Start()
	{
        AlphaTeamRef = AlphaTeam;
		BravoTeamRef = BravoTeam;
		Debug.Log("Connecting to Master");
		PhotonNetwork.ConnectUsingSettings();
		PhotonNetwork.SendRate = 60; //Default is 30
		PhotonNetwork.SerializationRate = 60; //5 is really laggy, jumpy. Default is 10?
		key = 1;
	}

	public override void OnConnectedToMaster()
	{
		Debug.Log("Connected to Master");
		PhotonNetwork.JoinLobby();
		PhotonNetwork.AutomaticallySyncScene = true;
	}

	public override void OnJoinedLobby()
	{
		//print(PlayerNameInputField.text);
		
		MenuManager.Instance.OpenMenu("Start");
		//Debug.Log("Joined Lobby");
	}

	public void StartickNameButoon()
    {
		PhotonNetwork.NickName = (PlayerNameInputField.text);
		MenuManager.Instance.OpenMenu("title");
		Debug.Log("Joined Lobby");
	}

	public void CreateRoom()
	{
		if(string.IsNullOrEmpty(roomNameInputField.text))
		{
			return;
		}
		PhotonNetwork.CreateRoom(roomNameInputField.text);
		MenuManager.Instance.OpenMenu("loading");
	}

	public override void OnJoinedRoom()
	{
		MenuManager.Instance.OpenMenu("room");
		roomNameText.text = PhotonNetwork.CurrentRoom.Name;

		Player[] players = PhotonNetwork.PlayerList;

		foreach(Transform child in AlphaTeamContent)
		{
			Destroy(child.gameObject);
		}

		for(int i = 0; i < players.Count(); i++)
		{
			PlayerList[key] = Instantiate(PlayerListItemPrefab, AlphaTeamContent);
			PlayerList[key++].GetComponent<PlayerListItem>().SetUp(players[i]);
		}

		 startGameButton.SetActive(PhotonNetwork.IsMasterClient);
	}

	public override void OnMasterClientSwitched(Player newMasterClient)
	{
		startGameButton.SetActive(PhotonNetwork.IsMasterClient);
	}

	public override void OnCreateRoomFailed(short returnCode, string message)
	{
		errorText.text = "Room Creation Failed: " + message;
		Debug.LogError("Room Creation Failed: " + message);
		  MenuManager.Instance.OpenMenu("error");
	}

	public void StartGame()
	{
	
		PhotonView PV = GetComponent<PhotonView>();
		PV.RPC("LoadScene", RpcTarget.All);
	}
	[PunRPC]
	void LoadScene()
	{
		LoadMapCanvas.instance.Show();
		StartCoroutine(LoadLevelAsync());
	}

	IEnumerator LoadLevelAsync()
	{
		//PhotonNetwork.LoadLevel("Map");
		SceneManager.LoadSceneAsync("Map");
		//while (PhotonNetwork.LevelLoadingProgress < 1)
		//{
		//	loadSceneBarText.text = "Loading Map:" + (int)(PhotonNetwork.LevelLoadingProgress * 100)+"%";
		//	//loadAmount = async.progress;
		//	loadSceneBar.value=PhotonNetwork.LevelLoadingProgress;
		//	print(PhotonNetwork.LevelLoadingProgress);
		//yield return new WaitForEndOfFrame();
		//}
		yield return new WaitForEndOfFrame();
	}


	public void LeaveRoom()
	{
		PhotonNetwork.LeaveRoom();
		MenuManager.Instance.OpenMenu("loading");
	}

	public void JoinRoom(RoomInfo info)
	{
		PhotonNetwork.JoinRoom(info.Name);
		MenuManager.Instance.OpenMenu("loading");
	}

	public override void OnLeftRoom()
	{
		MenuManager.Instance.OpenMenu("title");
	}

	public override void OnRoomListUpdate(List<RoomInfo> roomList)
	{
		foreach(Transform trans in roomListContent)
		{
			Destroy(trans.gameObject);
	}

		for(int i = 0; i < roomList.Count; i++)
		{
			if(roomList[i].RemovedFromList)
				continue;
			Instantiate(roomListItemPrefab, roomListContent).GetComponent<RoomListItem>().SetUp(roomList[i]);
		}
	}

	public override void OnPlayerEnteredRoom(Player newPlayer)
	{
		 
	}
	public void onClickedExitGame()
    {
		Application.Quit();
    }
	public void SwitchTeams(string team)
    {
			PV.RPC("OnTeamChanged", RpcTarget.All, team,key-1);
 
    }
	[PunRPC]
	public void OnTeamChanged(string team,int key)
    {
        if (team == "Alpha")
        {
			PlayerList[key].transform.SetParent(AlphaTeamContent, false);
		}
		if (team == "Bravo")
		{

				PlayerList[key].transform.SetParent(BravoTeamContent, false);
		}
	}
}
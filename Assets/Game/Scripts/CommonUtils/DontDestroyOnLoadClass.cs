using UnityEngine;
using System.Collections;

public class DontDestroyOnLoadClass : MonoBehaviour
{
	public static DontDestroyOnLoadClass Instance;
	public GameObject singlePlayerBtn;
	public GameObject multiPlayerBtn;
	public GameObject createRoomBtn;
	public GameObject joinRoomBtn;
	public GameObject backToMenuBtn;
	public GameObject levelPanel;
	public GameObject menMenu;
//	public GameObject Scrol_Panal;

	public bool isMultiPlayer = false;
	void Awake()
	{
		Instance = this;
		SetDataForMultiPlayer();
	}
	void Start()
	{
//		DontDestroyOnLoad (gameObject);
//		SetMenu();
		levelPanel.SetActive(false);
		menMenu.SetActive(true);
	}

	public void OnStartClicked()
	{
		levelPanel.SetActive(true);
		menMenu.SetActive(false);
	//	backToMenuBtn.SetActive (true);
	}
	public void SinglePlayer(int pLevel)
	{
		isMultiPlayer = false;
		string str = "Level" + pLevel;
		Application.LoadLevel (str);
	}

	public void SelectedLevel(int level)
	{
		SinglePlayer (level);
	}

	public void MultiPlayer()
	{
		isMultiPlayer = true;
		SetMenuBtnActive (false);
	}

	public void CreateRoom()
	{
		PhotonNetwork.CreateRoom("myRoom", new RoomOptions() { maxPlayers = 10 }, null);
	}

	public void JoinRoom()
	{
		PhotonNetwork.JoinRoom("myRoom");
	}

	public void SetMenu()
	{
		SetMenuBtnActive (true);
	}

	void SetMenuBtnActive(bool pState)
	{
		backToMenuBtn.SetActive (!pState);
		singlePlayerBtn.SetActive (pState);
//		multiPlayerBtn.SetActive ( pState );
		createRoomBtn.SetActive (!pState);
		joinRoomBtn.SetActive (!pState);
	}
	
	void SetDataForMultiPlayer()
	{
		PhotonNetwork.automaticallySyncScene = true;
		
		if (PhotonNetwork.connectionStateDetailed == PeerState.PeerCreated)
		{
			PhotonNetwork.ConnectUsingSettings("0.9");
		}
		PhotonNetwork.playerName = "Guest" + Random.Range(1, 9999);
	}

	public void OnCreatedRoom()
	{
		PhotonNetwork.LoadLevel("GamePlay");
	}

	public void On_Level_Get()
	{
		if (Input.GetMouseButtonDown (0)) 
		{
			for (int i = 0; i < 10; i++)
			{
				
			}
		}
	}
}

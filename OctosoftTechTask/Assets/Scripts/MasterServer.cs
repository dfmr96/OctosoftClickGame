using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MasterServer : MonoBehaviourPunCallbacks
{
    [Header("UI Buttons")]
    [SerializeField] Button createRoom;
    [SerializeField] Button joinRoom;
    [SerializeField] Button exitRoom;
    [Space]
    [Header("UI Text")]
    [SerializeField] TMP_Text countdownToStart;
    [SerializeField] TMP_Text playerJoined;
    [Space]
    [Header("Countdown settings")]
    [SerializeField] float counter = 5f;
    public TimeSpan timeSpan;
    [Header("Game Objects")]
    [SerializeField] GameObject playersReadyGO;
    [Header("Misc")]
    [SerializeField] byte maxPlayersRoom;
    bool playersReady = false;
    void Start()
    {
        /// Single Player / Multiplayer setter ///
        if (GameModeManager.sharedInstance.isSinglePlayer)
        {
            PhotonNetwork.OfflineMode = true;
            maxPlayersRoom = 1;
        }
        else
        {
            PhotonNetwork.ConnectUsingSettings();
            maxPlayersRoom = 2;
            Debug.Log("Connecting to Server...");
        }

        /// Room Leave on mainscreen ///
        if (PhotonNetwork.InRoom)
        {
            ActiveCreateJoinBtn();
            PhotonNetwork.LeaveRoom();
        }
    }
    public void ActiveCreateJoinBtn()
    {
        createRoom.interactable = true;
        joinRoom.interactable = true;
    }
    public void DeactiveCreateJoinBtn()
    {
        createRoom.interactable = false;
        joinRoom.interactable = false;
    }
    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to Main Server");
        PhotonNetwork.NickName = PlayerPrefs.GetString("PLAYER_NAME");
        PhotonNetwork.AutomaticallySyncScene = true;
        ActiveCreateJoinBtn();
    }
    public void CreateRoom()
    {
        AudioManager.sharedInstance.btnSound.Play();
        Debug.Log("Creating Room...");
        PhotonNetwork.CreateRoom(null, new RoomOptions() { MaxPlayers = maxPlayersRoom }, TypedLobby.Default);
    }
    public override void OnCreatedRoom()
    {
        Debug.Log(PhotonNetwork.CurrentRoom + "created succesfully");
        DeactiveCreateJoinBtn();
        DifficultyManager.sharedInstance.isHost = true;
        counter = 5f;
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        base.OnCreateRoomFailed(returnCode, message);
        Debug.Log(message);
    }
    public void JoinRoom()
    {
        AudioManager.sharedInstance.btnSound.Play();
        Debug.Log("Joining Room...");
        PhotonNetwork.JoinRandomRoom();
    }
    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        Debug.Log(PhotonNetwork.NickName + " connected to " + PhotonNetwork.CurrentRoom);
        exitRoom.interactable = true;
        DeactiveCreateJoinBtn();
        StartCoroutine(matchStarting());
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        base.OnJoinRandomFailed(returnCode, message);
        Debug.Log(message);
    }
    public void ExitRoom()
    {
        AudioManager.sharedInstance.btnSound.Play();
        PhotonNetwork.LeaveRoom();
        Debug.Log("Leaving Room...");
    }

    public override void OnLeftRoom()
    {
        base.OnLeftRoom();
        Debug.Log("You left the room");
        ActiveCreateJoinBtn();
        exitRoom.interactable = false;
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        base.OnPlayerEnteredRoom(newPlayer);
        Debug.Log(newPlayer + " joined");
        playerJoined.text = newPlayer.NickName + " has joined";
        StartCoroutine(matchStarting());
    }

    IEnumerator matchStarting()
    {
        if (PhotonNetwork.PlayerList.Length == (int)maxPlayersRoom)
        {
            playersReady = true;
            playersReadyGO.SetActive(true);
            DifficultyManager.sharedInstance.ShowDifficulties();
            yield return new WaitForSeconds(counter);
            PhotonNetwork.LoadLevel("MainGame");
        }
    }
    private void Update()
    {
        if (playersReady)
        {
            counter -= Time.deltaTime;
            timeSpan = TimeSpan.FromSeconds(counter);
            countdownToStart.text = string.Format("{0:D2}:{1:D2}", (timeSpan.Seconds), (timeSpan.Milliseconds));
        }
    }
}

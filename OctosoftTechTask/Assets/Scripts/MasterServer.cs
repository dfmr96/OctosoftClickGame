using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.Diagnostics.Tracing;
using System;

public class MasterServer : MonoBehaviourPunCallbacks
{
    [SerializeField] Button createRoom, joinRoom, exitRoom;
    [SerializeField] TMP_Text countdownToStart, playerJoined;
    [SerializeField] float counter = 5f;
    public TimeSpan timeSpan;
    bool playersReady = false;
    [SerializeField] GameObject playersReadyGO;
    [SerializeField] byte maxPlayersRoom;

    void Start()
    {
        if (GameModeManager.sharedInstance.isSinglePlayer)
        {
            PhotonNetwork.OfflineMode = true;
            maxPlayersRoom = 1;
        } else
        {
        PhotonNetwork.ConnectUsingSettings();
            maxPlayersRoom = 2;
        Debug.Log("Connecting to Server...");
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
        Debug.Log("Creating Room...");
        PhotonNetwork.CreateRoom(null, new RoomOptions() { MaxPlayers = maxPlayersRoom }, TypedLobby.Default);
    }
    public override void OnCreatedRoom()
    {
        Debug.Log(PhotonNetwork.CurrentRoom + "created succesfully");
        DeactiveCreateJoinBtn();

        DifficultyManager.sharedInstance.isHost = true;

    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        base.OnCreateRoomFailed(returnCode, message);
        Debug.Log(message);
    }


    public void JoinRoom()
    {
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
        Debug.Log( newPlayer + " joined");
        playerJoined.text = newPlayer.NickName + " has joined";
        StartCoroutine(matchStarting());
    }

    IEnumerator matchStarting()
    {

        if (PhotonNetwork.PlayerList.Length == (int)maxPlayersRoom)
        {
            playersReady = true;
            playersReadyGO.SetActive(true);
            yield return new WaitForSeconds(counter);
            PhotonNetwork.LoadLevel("MainGame");
        }
    }

    private void Update()
    {
        if (playersReady) {

        counter -= Time.deltaTime;

        timeSpan = TimeSpan.FromSeconds(counter);
        countdownToStart.text = string.Format("{0:D2}:{1:D2}", (timeSpan.Seconds), (timeSpan.Milliseconds));
        }
    }
}

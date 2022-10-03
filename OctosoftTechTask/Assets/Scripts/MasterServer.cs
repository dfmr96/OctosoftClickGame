using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
public class MasterServer : MonoBehaviourPunCallbacks
{
    [SerializeField] Button createRoom, joinRoom, exitRoom;

    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
        Debug.Log("Connecting to Server...");
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
        
        ActiveCreateJoinBtn();

        PhotonNetwork.NickName = PlayerPrefs.GetString("PLAYER_NAME");
    }

    public void CreateRoom()
    {
        Debug.Log("Creating Room...");
        PhotonNetwork.CreateRoom(null, new RoomOptions() { MaxPlayers = 2 }, TypedLobby.Default);
    }
    public override void OnCreatedRoom()
    {
        Debug.Log(PhotonNetwork.CurrentRoom + "created succesfully");
        DeactiveCreateJoinBtn();
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

    }
}
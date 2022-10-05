using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DifficultyManager : MonoBehaviourPun
{
    public static DifficultyManager sharedInstance;
    [SerializeField] Toggle easyBtn, normalbtn, hardbtn;
    public int player1Difficulty;
    public int player2Difficulty;
    public bool isHost = false;
    private void Awake()
    {
        if (sharedInstance == null)
        {
            sharedInstance = this;
        }
    }

    [PunRPC]
    void ChangeDifficulty(int difficulty, bool host)
    {
        if (host)
        {
            player1Difficulty = difficulty;
            Debug.Log("Player 1 act Difficulty changed to " + player1Difficulty + "for " + PhotonNetwork.PlayerList[0].NickName + PhotonNetwork.PlayerList[1].NickName);
        }
        else
        {
            player2Difficulty = difficulty;
            Debug.Log("Player 2 act Difficulty changed to " + player2Difficulty + "for " + PhotonNetwork.PlayerList[1].NickName + PlayerPrefs.GetString("PLAYER_NAME"));
        }
    }
}

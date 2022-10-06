using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DifficultyManager : MonoBehaviourPun
{
    public static DifficultyManager sharedInstance;
    [SerializeField] Toggle easyBtn, normalbtn, hardbtn;
    public int player1Difficulty = 1;
    public int player2Difficulty = 1;
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
        }
        else
        {
            player2Difficulty = difficulty;
        }
    }
}

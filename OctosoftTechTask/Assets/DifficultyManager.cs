using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DifficultyManager : MonoBehaviourPun, IPunObservable
{
    public static DifficultyManager sharedInstance;
    [SerializeField] Toggle easyBtn, normalbtn, hardbtn;
    public int myDifficulty;
    public int player1Difficulty;
    public int player2Difficulty;
    private void Awake()
    {
        if (sharedInstance == null)
        {
            sharedInstance = this;
        }
    }
    private void Start()
    {
        normalbtn.isOn = true;
    }

    private void Update()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            player1Difficulty = myDifficulty;
        }
        else
        {
            player2Difficulty = myDifficulty;
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(player1Difficulty);
            stream.SendNext(player2Difficulty);
        }
        else
        {
            player1Difficulty = (int)stream.ReceiveNext();
            player2Difficulty = (int)stream.ReceiveNext();
        }
    }
}

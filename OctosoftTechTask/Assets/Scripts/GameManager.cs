using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using System;
using Photon.Pun;
using System.Security.Cryptography;

public class GameManager : MonoBehaviourPun , IPunObservable
{
    public static GameManager sharedInstance;

    public int player1TotalPoints = 0;
    public int player2TotalPoints = 0;

    public TMP_Text player1Points;
    public TMP_Text player2Points;

    public int pointsToWin = 100;
    public TMP_Text timeLeft;

    public float counter = 0;
    public float maxTime = 120;

    public bool targetDestroyed = false;

    public int coinsToSpawn;
    public int player1CoinsToSpawn = 0;
    public int player2CoinsToSpawn = 0;

    public GameObject gameOverScreen;
    public TMP_Text resultsText;

    public TimeSpan timeSpan;

    private void Awake()
    {
        if (sharedInstance == null)
        {
            sharedInstance = this;
        }
    }

    private void Start()
    {
        gameOverScreen.SetActive(false);
        
    }

    private void Update()
    {
        player1Points.GetComponent<TMP_Text>().text = player1TotalPoints.ToString("000");
        player2Points.GetComponent<TMP_Text>().text = player2TotalPoints.ToString("000");


        maxTime -= Time.deltaTime;
        timeSpan = TimeSpan.FromSeconds(maxTime);
        timeLeft.text = string.Format("{0:D2}:{1:D2}", (timeSpan.Minutes), ( timeSpan.Seconds));

        if (timeSpan.TotalSeconds <= 0)
        {
            GameOverScreen(false);
        }
    }

    public void GameOverScreen(bool haveWon)
    {
        Time.timeScale = 0;
        gameOverScreen.SetActive(true);

        if (haveWon)
        {
            resultsText.text = "You WON!!!";
        } else
        {
            resultsText.text = "You lose!";
        }
    }

    void IPunObservable.OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(player1TotalPoints);
            stream.SendNext(player2TotalPoints);
        }
        else
        {
            player1TotalPoints = (int)stream.ReceiveNext();
            player2TotalPoints = (int)stream.ReceiveNext();
        }
    }
}

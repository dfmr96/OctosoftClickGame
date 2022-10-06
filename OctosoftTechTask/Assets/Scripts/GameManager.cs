using Photon.Pun;
using System;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviourPun, IPunObservable
{
    public static GameManager sharedInstance;

    [Header("Player Points")]
    public int player1TotalPoints = 0;
    public int player2TotalPoints = 0;
    [Space]
    [Header("Player coins to be spawned")]
    public int coinsToSpawn;
    public int player1CoinsToSpawn = 0;
    public int player2CoinsToSpawn = 0;
    [Space]
    [Header("UI Text")]
    public TMP_Text player1Points;
    public TMP_Text player2Points;
    public TMP_Text timeLeft;
    public TMP_Text resultsText;
    [Space]
    [Header("Game Setting")]
    public int pointsToWin = 100;
    public float counter = 0;
    public float maxTime = 120;
    public TimeSpan timeSpan;
    [Space]
    [Header("Game Objects")]
    public GameObject gameOverScreen;

    private void Awake()
    {
        if (sharedInstance == null)
        {
            sharedInstance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        gameOverScreen.SetActive(false);
    }
    private void Update()
    {

        /// Points Update ///
        player1Points.GetComponent<TMP_Text>().text = PhotonNetwork.PlayerList[0].NickName + "\n Score: " + player1TotalPoints.ToString("000");

        if (GameModeManager.sharedInstance.isSinglePlayer)
        {
            player2Points.GetComponent<TMP_Text>().text = "LOOK FOR A CHALLENGER";
        }
        else
        {
            player2Points.GetComponent<TMP_Text>().text = PhotonNetwork.PlayerList[1].NickName + "\n Score: " + player2TotalPoints.ToString("000");
        }

        /// Time Update ///
        maxTime -= Time.deltaTime;
        timeSpan = TimeSpan.FromSeconds(maxTime);
        timeLeft.text = string.Format("{0:D2}:{1:D2}", (timeSpan.Minutes), (timeSpan.Seconds));

        /// TimeOut conditions ///
        if (timeSpan.TotalSeconds <= 0)
        {
            if (!GameModeManager.sharedInstance.isSinglePlayer)
            {
                if (player1TotalPoints > player2TotalPoints)
                {
                    GameOverScreen(true);
                }
                else
                {
                    GameOverScreen(false);
                }
            }
            else
            {
                GameOverScreen(false);
            }
        }
    }
    public void GameOverScreen(bool haveWon)
    {
        Time.timeScale = 0;
        gameOverScreen.SetActive(true);
        if (!GameModeManager.sharedInstance.isSinglePlayer)
        {
            /// Multiplayer Winner
            if (haveWon)
            {
                resultsText.text = PhotonNetwork.PlayerList[0].NickName + " has WON!!!";
            }
            else
            {
                resultsText.text = PhotonNetwork.PlayerList[1].NickName + " has WON!!!";
            }
        }
        else
        {
            /// Single player winner / Timeout ///
            if (haveWon)
            {
                resultsText.text = "You WON!";
            }
            else
            {
                resultsText.text = "You loose";
            }
        }
    }
    void IPunObservable.OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(player1TotalPoints);
            stream.SendNext(player2TotalPoints);
            stream.SendNext(player1CoinsToSpawn);
            stream.SendNext(player2CoinsToSpawn);

        }
        else
        {
            player1TotalPoints = (int)stream.ReceiveNext();
            player2TotalPoints = (int)stream.ReceiveNext();
            player1CoinsToSpawn = (int)stream.ReceiveNext();
            player2CoinsToSpawn = (int)stream.ReceiveNext();
        }
    }
}

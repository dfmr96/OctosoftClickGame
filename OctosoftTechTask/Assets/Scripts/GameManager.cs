using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager sharedInstance;
    public int totalPoints = 0;
    public TMP_Text points;
    public int pointsToWin = 100;
    public TMP_Text timeLeft;

    public float counter = 0;

    public bool targetDestroyed = false;
    public int CoinsToSpawn = 0;

    public GameObject gameOverScreen;
    public TMP_Text resultsText;

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
        counter = Time.fixedTime;
        points.GetComponent<TMP_Text>().text = totalPoints.ToString("000");
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
}

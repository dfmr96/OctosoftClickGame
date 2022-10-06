using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using Photon.Pun;

public class MainMenuUIManager : MonoBehaviour
{
    [Header("UI Buttons")]
    [SerializeField] Button playBtn;
    [SerializeField] Button multiplayerBtn;
    [SerializeField] Button exitBtn; 
    [SerializeField] Button nameConfirmBtn;
    [Space]
    [Header("Name Input Field")]
    [SerializeField] TMP_InputField nameInput;
    [Header("Game Objects")]
    [SerializeField] GameObject insertNameScreen;

    private void Start()
    {
        Application.targetFrameRate = 60;
        Time.timeScale = 1;
        Debug.Log(PlayerPrefs.GetString("PLAYER_NAME"));
        PhotonNetwork.Disconnect();
    }
    public void PlayGame()
    {
        GameModeManager.sharedInstance.isSinglePlayer = true;
        AudioManager.sharedInstance.btnSound.Play();
        SceneManager.LoadScene(1);
    }

    public void Multiplayer()
    {
        GameModeManager.sharedInstance.isSinglePlayer = false;
        AudioManager.sharedInstance.btnSound.Play();

        if (!PlayerPrefs.HasKey("PLAYER_NAME"))
        {
            insertNameScreen.SetActive(true);
        } else
        {
            SceneManager.LoadScene(1);
        }
    }

    public void ConfirmName()
    {
        AudioManager.sharedInstance.btnSound.Play();
        PlayerPrefs.SetString("PLAYER_NAME", nameInput.text);
        Debug.Log(PlayerPrefs.GetString("PLAYER_NAME"));
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        UnityEngine.Application.Quit();
    }
}

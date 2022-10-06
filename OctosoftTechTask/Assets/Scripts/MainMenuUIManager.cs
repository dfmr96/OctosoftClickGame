using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenuUIManager : MonoBehaviour
{
    [SerializeField] Button playBtn, multiplayerBtn, exitBtn, nameConfirmBtn;
    [SerializeField] TMP_InputField nameInput;
    [SerializeField] GameObject insertNameScreen;

    private void Start()
    {
        Debug.Log(PlayerPrefs.GetString("PLAYER_NAME"));
    }
    public void PlayGame()
    {
        GameModeManager.sharedInstance.isSinglePlayer = true;
        SceneManager.LoadScene(1);
    }

    public void Multiplayer()
    {
        GameModeManager.sharedInstance.isSinglePlayer = false;
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
        PlayerPrefs.SetString("PLAYER_NAME", nameInput.text);
        Debug.Log(PlayerPrefs.GetString("PLAYER_NAME"));
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        UnityEngine.Application.Quit();
    }
}

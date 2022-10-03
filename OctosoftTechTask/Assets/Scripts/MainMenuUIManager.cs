using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.Device;
using UnityEngine.iOS;

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
        SceneManager.LoadScene(1);
    }

    public void Multiplayer()
    {
        if (!PlayerPrefs.HasKey("PLAYER_NAME"))
        {
            insertNameScreen.SetActive(true);
        } else
        {
            SceneManager.LoadScene(2);
        }
    }

    public void ConfirmName()
    {
        PlayerPrefs.SetString("PLAYER_NAME", nameInput.text);
        Debug.Log(PlayerPrefs.GetString("PLAYER_NAME"));
        SceneManager.LoadScene(2);
    }

    public void QuitGame()
    {
        UnityEngine.Application.Quit();
    }
}

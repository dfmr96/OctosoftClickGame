using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnMainMenu : MonoBehaviour
{
    public void GoToMainMenu()
    {
        AudioManager.sharedInstance.btnSound.Play();
        SceneManager.LoadScene(0);
    }
}

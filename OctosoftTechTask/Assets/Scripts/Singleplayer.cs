using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleplayer : MonoBehaviour
{
    [SerializeField] Camera player1Cam;
    [SerializeField] GameObject player2Objects;
    [SerializeField] GameObject splitScreenBar;
    void Awake()
    {
        if(GameModeManager.sharedInstance.isSinglePlayer)
        {
            player1Cam.transform.position = new Vector3 (0,0,-10);
            player1Cam.rect = new Rect(0, 0, 1, 1);
            player2Objects.SetActive(false);
            splitScreenBar.SetActive(false);
        }
    }
}

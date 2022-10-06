using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleplayer : MonoBehaviour
{
    [SerializeField] Camera player1Cam;
    [SerializeField] GameObject player2Objects;
    // Start is called before the first frame update
    void Awake()
    {
        if(GameModeManager.sharedInstance.isSinglePlayer)
        {
            player1Cam.transform.position = new Vector3 (0,0,-10);
            player1Cam.rect = new Rect(0, 0, 1, 1);
            player2Objects.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

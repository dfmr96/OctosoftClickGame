using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameModeManager : MonoBehaviour
{
    public static GameModeManager sharedInstance;
    public bool isSinglePlayer;

    private void Awake()
    {
        if (sharedInstance == null)
        {
            sharedInstance = this;
        } else
        {
            Destroy(gameObject);
        }
    }
}

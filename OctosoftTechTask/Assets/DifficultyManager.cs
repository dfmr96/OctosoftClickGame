using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DifficultyManager : MonoBehaviour
{
    public static DifficultyManager sharedInstance;
    [SerializeField] Toggle easyBtn;
    public int difficulty;
    private void Awake()
    {
        if (sharedInstance == null)
        {
            sharedInstance = this;
        }
    }
    private void Start()
    {
        easyBtn.isOn = true;
    }

}

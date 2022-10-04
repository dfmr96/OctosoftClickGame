using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DifficultyManager : MonoBehaviour
{
    [SerializeField] Toggle easyBtn, normalBtn, hardBtn;
    public int easy, normal, hard;
    public int difficulty;

    private void Start()
    {
        easyBtn.isOn = true;
    }

}

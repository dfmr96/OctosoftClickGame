using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ToggleScript : MonoBehaviour
{
    Toggle toggle;
    [SerializeField] int difficulty;
    Color normalColor, selectedColor, originalNormalColor;

    private void Start()
    {
        toggle = GetComponent<Toggle>();
        toggle.onValueChanged.AddListener(OnToggleValueChanged);
        normalColor = toggle.colors.normalColor;
        originalNormalColor = toggle.colors.normalColor;
        selectedColor = toggle.colors.selectedColor;
    }
    private  void OnToggleValueChanged(bool isOn)
    {
        ColorBlock cb = toggle.colors;
        if (toggle.isOn)
        {
            DifficultyManager.sharedInstance.myDifficulty = difficulty;
            cb.normalColor = selectedColor;
        } else
        {
            cb.normalColor = originalNormalColor;
        }
        toggle.colors = cb;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class ObjectStats : MonoBehaviour, IPointerClickHandler
{
    public int pointsGranted = 0;
    public int pointsLost = 0;
    public int health = 0;
    [SerializeField] int secondsToBeDestroyed = 5;
    [SerializeField] bool isTarget = false;


    private void Start()
    {
        StartCoroutine(LifeTimer());
    }
    IEnumerator LifeTimer()
    {
        yield return new WaitForSeconds(secondsToBeDestroyed);
        LossPointsOnDisappear(pointsLost);
        Destroy(gameObject);
    }

    
    private void GrantPoints(int points)
    {
        GameManager.sharedInstance.totalPoints += points;

        if(GameManager.sharedInstance.totalPoints >= GameManager.sharedInstance.pointsToWin)
        {
            GameManager.sharedInstance.totalPoints = 100;
            GameManager.sharedInstance.GameOverScreen(true);
        }
    }

    void LossPointsOnDisappear(int points)
    {
        GameManager.sharedInstance.totalPoints -= points;

        if (GameManager.sharedInstance.totalPoints < 0)
        {
            GameManager.sharedInstance.totalPoints = 0;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Clicked: " + eventData.pointerCurrentRaycast.gameObject.name);

        health--;

        if(health <= 0)
        {
            if (isTarget)
            {
                GameManager.sharedInstance.targetDestroyed = true;
                GameManager.sharedInstance.CoinsToSpawn += 3;
            }
            GrantPoints(pointsGranted);
            Destroy(gameObject);
        }
    }
}

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
    }

    void LossPointsOnDisappear(int points)
    {
        GameManager.sharedInstance.totalPoints -= points;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Clicked: " + eventData.pointerCurrentRaycast.gameObject.name);

        health--;

        if(health <= 0)
        {
            GrantPoints(pointsGranted);
            Destroy(gameObject);
        }
    }
}

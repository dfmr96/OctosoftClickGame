using Photon.Pun;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(PhotonView))]
[RequireComponent(typeof(PhotonTransformView))]
public class ObjectStats : MonoBehaviourPun, IPointerClickHandler
{
    [Header("Objects Stats")]
    public int health = 0;
    public int pointsGranted = 0;
    public int pointsLost = 0;
    public int coinsBonusSpawn = 0;
    [SerializeField] int secondsToBeDestroyed = 5;
    //[SerializeField] bool isTarget = false;
    private void Start()
    {
        StartCoroutine(LifeTimer());
    }
    IEnumerator LifeTimer()
    {
        yield return new WaitForSeconds(secondsToBeDestroyed);
        LossPointsOnDisappear(pointsLost);
        AudioManager.sharedInstance.objectDestroyed.Play();
        Destroy(gameObject);
    }

    [PunRPC]
    private void GrantPoints(int points, int coinsBonus)
    {
        switch (gameObject.GetPhotonView().OwnerActorNr)
        {
            case 1:
                GameManager.sharedInstance.player1TotalPoints += points;
                GameManager.sharedInstance.player1CoinsToSpawn += coinsBonus;

                if (GameManager.sharedInstance.player1TotalPoints < 0)
                {
                    GameManager.sharedInstance.player1TotalPoints = 0;
                }

                if (GameManager.sharedInstance.player1TotalPoints >= GameManager.sharedInstance.pointsToWin)
                {
                    GameManager.sharedInstance.player1TotalPoints = 100;
                    GameManager.sharedInstance.GameOverScreen(true);
                }
                break;
            case 2:
                GameManager.sharedInstance.player2TotalPoints += points;
                GameManager.sharedInstance.player2CoinsToSpawn += coinsBonus;


                if (GameManager.sharedInstance.player2TotalPoints < 0)
                {
                    GameManager.sharedInstance.player2TotalPoints = 0;
                }

                if (GameManager.sharedInstance.player2TotalPoints >= GameManager.sharedInstance.pointsToWin)
                {
                    GameManager.sharedInstance.player2TotalPoints = 100;
                    GameManager.sharedInstance.GameOverScreen(false);
                }
                break;

        }
    }

    void LossPointsOnDisappear(int points)
    {
        switch (gameObject.GetPhotonView().OwnerActorNr)
        {
            case 1:
                GameManager.sharedInstance.player1TotalPoints -= points;

                if (GameManager.sharedInstance.player1TotalPoints < 0)
                {
                    GameManager.sharedInstance.player1TotalPoints = 0;
                }
                break;
            case 2:
                GameManager.sharedInstance.player2TotalPoints -= points;

                if (GameManager.sharedInstance.player2TotalPoints < 0)
                {
                    GameManager.sharedInstance.player2TotalPoints = 0;
                }
                break;

        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (photonView.IsMine)
        {
            health--;
            if (health <= 0)
            {
                photonView.RPC("GrantPoints", RpcTarget.All, pointsGranted, coinsBonusSpawn);
                AudioManager.sharedInstance.objectDestroyed.Play();
                PhotonNetwork.Destroy(gameObject);
            }
        }
    }
}


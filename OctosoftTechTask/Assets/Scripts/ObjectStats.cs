using Photon.Pun;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(PhotonView))]
[RequireComponent(typeof(PhotonTransformView))]
public class ObjectStats : MonoBehaviourPun, IPointerClickHandler
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

    [PunRPC]
    private void GrantPoints(int points)
    {
        switch (gameObject.GetPhotonView().OwnerActorNr)
        {
            case 1:
                GameManager.sharedInstance.player1TotalPoints += points;

                if(GameManager.sharedInstance.player1TotalPoints < 0)
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

                if(GameManager.sharedInstance.player2TotalPoints < 0)
                {
                    GameManager.sharedInstance.player2TotalPoints = 0;
                }

                if (GameManager.sharedInstance.player2TotalPoints >= GameManager.sharedInstance.pointsToWin)
                {
                    GameManager.sharedInstance.player2TotalPoints = 100;
                    GameManager.sharedInstance.GameOverScreen(true);
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

            Debug.Log("Clicked: " + eventData.pointerCurrentRaycast.gameObject.name);

            health--;

            if (health <= 0)
            {
                if (isTarget)
                {
                    GameManager.sharedInstance.targetDestroyed = true;
                    switch (gameObject.GetPhotonView().OwnerActorNr)
                    {
                        case 1:
                            GameManager.sharedInstance.player1CoinsToSpawn += 3;
                            break;
                        case 2:
                            GameManager.sharedInstance.player2CoinsToSpawn += 3;
                            break;

                    }

                }
                photonView.RPC("GrantPoints", Photon.Pun.RpcTarget.All, pointsGranted);
                PhotonNetwork.Destroy(gameObject);
            }

        }
    }
}

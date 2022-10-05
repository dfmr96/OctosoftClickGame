using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

[RequireComponent(typeof(PhotonView))]
public class ObjectsSpawner : MonoBehaviourPun
{
    public GameObject[] objectsPrefabs = new GameObject[5];
    public GameObject zoneToSpawn;
    float maxX, minX, maxY, minY, maxZ, minZ;
    [SerializeField] float timeToSpawn = 3f;

    int randomPrefab;
    public GameObject playerParent;

    private void Start()
    {
        maxX = zoneToSpawn.GetComponent<BoxCollider>().bounds.max.x;
        minX = zoneToSpawn.GetComponent<BoxCollider>().bounds.min.x;

        maxY = zoneToSpawn.GetComponent<BoxCollider>().bounds.max.y;
        minY = zoneToSpawn.GetComponent<BoxCollider>().bounds.min.y;

        maxZ = zoneToSpawn.GetComponent<BoxCollider>().bounds.max.z;
        minZ = zoneToSpawn.GetComponent<BoxCollider>().bounds.min.z;

        StartCoroutine(SpawnObjectTimer());

        SpawnObject();
    }

    IEnumerator SpawnObjectTimer()
    {
        yield return new WaitForSeconds(timeToSpawn);
        SpawnObject();
        StartCoroutine(SpawnObjectTimer());
    }

    void SpawnObject()
    {
        int objectsToSpawn;

        if (gameObject.tag == "Player1")
        {
            objectsToSpawn = DifficultyManager.sharedInstance.player1Difficulty;
            Debug.Log(objectsToSpawn + "a Player 1");
        } else
        {
            objectsToSpawn = DifficultyManager.sharedInstance.player2Difficulty;
            Debug.Log(objectsToSpawn + "a Player 2");
        }

        for (int i = 0; i < objectsToSpawn; i++)
        if (PhotonNetwork.IsMasterClient)
        {

        randomPrefab = Random.Range(0, objectsPrefabs.Length);

        if (GameManager.sharedInstance.player1CoinsToSpawn > 0 && this.tag == "Player1")
        {
            GameManager.sharedInstance.player1CoinsToSpawn--;
            randomPrefab = 0;
        }
        if (GameManager.sharedInstance.player2CoinsToSpawn > 0 && this.tag == "Player2")
        {
            GameManager.sharedInstance.player2CoinsToSpawn--;
            randomPrefab = 0;
        }
        GameObject randomObject = objectsPrefabs[randomPrefab];
        float randomX = Random.Range(minX, maxX);
        float randomY = Random.Range(minY, maxY);
        float randomZ = Random.Range(maxZ, minZ);
        Vector3 randomPos = new Vector3(randomX, randomY, randomZ);
        randomObject = PhotonNetwork.Instantiate(randomObject.name, randomPos, Quaternion.identity);
        randomObject.transform.parent = playerParent.transform;
        if (gameObject.tag == "Player1") {
            randomObject.GetPhotonView().TransferOwnership(PhotonNetwork.PlayerList[0]);
            randomObject.tag = "Player1";
        } else
        {
            randomObject.GetPhotonView().TransferOwnership(PhotonNetwork.PlayerList[1]);
            randomObject.tag = "Player2";
        }

        }
    }
}

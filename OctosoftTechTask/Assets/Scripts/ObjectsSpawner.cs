using Photon.Pun;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(PhotonView))]
public class ObjectsSpawner : MonoBehaviourPun
{
    public GameObject[] objectsPrefabs = new GameObject[5];
    public GameObject zoneToSpawn;
    float maxX, minX, maxY, minY, maxZ, minZ;
    [SerializeField] float randomTimeToSpawn;

    public GameObject playerParent;

    public Vector3 randomPos = Vector3.zero;
    public int randomPrefab = 0;

    int objectsToSpawn = 0;

    float randomX;
    float randomY;
    float randomZ;

    GameObject randomObject;

    private void Start()
    {
        maxX = zoneToSpawn.GetComponent<BoxCollider>().bounds.max.x;
        minX = zoneToSpawn.GetComponent<BoxCollider>().bounds.min.x;

        maxY = zoneToSpawn.GetComponent<BoxCollider>().bounds.max.y;
        minY = zoneToSpawn.GetComponent<BoxCollider>().bounds.min.y;

        maxZ = zoneToSpawn.GetComponent<BoxCollider>().bounds.max.z;
        minZ = zoneToSpawn.GetComponent<BoxCollider>().bounds.min.z;
        numberObjectsToSpawn();
        StartCoroutine(SpawnObjectTimer());
    }

    public void RandomSpawnTime()
    {
        float easyMinTimeToSpawn = 3f;
        float easyMaxTimeToSpawn = 4f;
        float normalMinTimeToSpawn = 2f;
        float normalMaxTimeToSpawn = 3f;
        float hardMinTimeToSpawn = 1f;
        float hardMaxTimeToSpawn = 2f;


        if (gameObject.tag == "Player 1")
            switch (DifficultyManager.sharedInstance.player1Difficulty)
            {
                case 1:
                    randomTimeToSpawn = Random.Range(easyMinTimeToSpawn, easyMaxTimeToSpawn);
                    break;

                case 2:
                    randomTimeToSpawn = Random.Range(normalMinTimeToSpawn, normalMaxTimeToSpawn);
                    break;
                case 3:
                    randomTimeToSpawn = Random.Range(hardMinTimeToSpawn, hardMaxTimeToSpawn);
                    break;
            }
        else
        {
            switch (DifficultyManager.sharedInstance.player2Difficulty)
            {
                case 1:
                    randomTimeToSpawn = Random.Range(easyMinTimeToSpawn, easyMaxTimeToSpawn);
                    break;

                case 2:
                    randomTimeToSpawn = Random.Range(normalMinTimeToSpawn, normalMaxTimeToSpawn);
                    break;
                case 3:
                    randomTimeToSpawn = Random.Range(hardMinTimeToSpawn, hardMaxTimeToSpawn);
                    break;
            }
        }
    }
    IEnumerator SpawnObjectTimer()
    {
        RandomSpawnTime();
        yield return new WaitForSeconds(randomTimeToSpawn);
        SpawnObject();
        StartCoroutine(SpawnObjectTimer());

        Debug.Log(randomTimeToSpawn);
    }

    public void numberObjectsToSpawn()
    {
        if (gameObject.tag == "Player1")
        {
            objectsToSpawn = DifficultyManager.sharedInstance.player1Difficulty;
            Debug.Log(objectsToSpawn + " a Player 1");
        }
        else
        {
            objectsToSpawn = DifficultyManager.sharedInstance.player2Difficulty;
            Debug.Log(objectsToSpawn + " a Player 2");
        }
    }

    public void CoinsChecker()
    {
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
    }

    public void RandomPosGenerator()
    {
        randomX = Random.Range(minX, maxX);
        randomY = Random.Range(minY, maxY);
        randomZ = Random.Range(minZ, maxZ);
    }

    public void SpawnersOwnerChecker()
    {
        if (gameObject.tag == "Player1")
        {
            randomObject.GetPhotonView().TransferOwnership(PhotonNetwork.PlayerList[0]);
            randomObject.tag = "Player1";
        }
        else
        {
            randomObject.GetPhotonView().TransferOwnership(PhotonNetwork.PlayerList[1]);
            randomObject.tag = "Player2";
        }
    }
    void SpawnObject()
    {
        if (PhotonNetwork.IsMasterClient)
        {

            for (int i = 0; i < objectsToSpawn; i++)
            {
                randomPrefab = Random.Range(0, objectsPrefabs.Length);
                CoinsChecker();
                RandomPosGenerator();
                randomPos = new Vector3(randomX, randomY, randomZ);
                randomObject = objectsPrefabs[randomPrefab];
                randomObject = PhotonNetwork.Instantiate(randomObject.name, randomPos, Quaternion.identity);
                randomObject.transform.parent = playerParent.transform;

                Debug.Log("Objeto #" + i + randomObject.name + " " + gameObject.tag);
                SpawnersOwnerChecker();
            }
        }
    }
}
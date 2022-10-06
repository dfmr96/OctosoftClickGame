using Photon.Pun;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(PhotonView))]
public class ObjectsSpawner : MonoBehaviourPun
{
    public GameObject[] objectsPrefabs = new GameObject[5];
    [Space]
    [Header("Single/Multiplayer Box Collider")]
    public BoxCollider zoneToSpawn;
    public BoxCollider singlePlayerZoneToSpawn;
    [Space]
    [SerializeField] float randomTimeToSpawn;
    public GameObject playerParent;
    public int randomPrefab = 0;
    public Vector3 randomPos = Vector3.zero;

    /// Private variables ///
    float maxX, minX, maxY, minY, maxZ, minZ;
    int objectsToSpawn = 0;
    float randomX;
    float randomY;
    float randomZ;
    GameObject randomObject;
    private void Start()
    {
        if(GameModeManager.sharedInstance.isSinglePlayer)
        {
            zoneToSpawn = singlePlayerZoneToSpawn;
        }
        //Bounds for objects to spawn ///
        maxX = zoneToSpawn.bounds.max.x;
        minX = zoneToSpawn.bounds.min.x;
        maxY = zoneToSpawn.bounds.max.y;
        minY = zoneToSpawn.bounds.min.y;
        maxZ = zoneToSpawn.bounds.max.z;
        minZ = zoneToSpawn.bounds.min.z;
        numberObjectsToSpawn();
        StartCoroutine(SpawnObjectTimer());
    }

    public void RandomSpawnTime()
    {
        /// Min/Max time for each difficulty ///
        float easyMinTimeToSpawn = 1.5f;
        float easyMaxTimeToSpawn = 3.5f;
        float normalMinTimeToSpawn = 1f;
        float normalMaxTimeToSpawn = 2f;
        float hardMinTimeToSpawn = 0.5f;
        float hardMaxTimeToSpawn = 1f;

        /// Check each player difficulty ///
        if (gameObject.tag == "Player1")
        {
            switch (DifficultyManager.sharedInstance.player1Difficulty)
            {
                case 1:
                    randomTimeToSpawn = Random.Range(easyMinTimeToSpawn, easyMaxTimeToSpawn);
                    Debug.Log("Dificultad 1" + easyMaxTimeToSpawn + easyMinTimeToSpawn);
                    break;

                case 2:
                    randomTimeToSpawn = Random.Range(normalMinTimeToSpawn, normalMaxTimeToSpawn);
                    Debug.Log("Dificultad 2" + normalMaxTimeToSpawn + normalMinTimeToSpawn);

                    break;
                case 3:
                    randomTimeToSpawn = Random.Range(hardMinTimeToSpawn, hardMaxTimeToSpawn);
                    Debug.Log("Dificultad 3" + hardMaxTimeToSpawn + hardMinTimeToSpawn);

                    break;
            }
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
        /// Gives ownership to 2nd player to be able to destroy objects ///
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
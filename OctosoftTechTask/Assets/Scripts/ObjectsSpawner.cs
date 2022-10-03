using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsSpawner : MonoBehaviour
{
    public GameObject[] objectsPrefabs = new GameObject[5];
    public GameObject zoneToSpawn;
    float maxX, minX, maxY, minY, maxZ, minZ;
    [SerializeField] float timeToSpawn = 3f;

    private void Start()
    {
        maxX = zoneToSpawn.GetComponent<BoxCollider>().bounds.max.x;
        minX = zoneToSpawn.GetComponent<BoxCollider>().bounds.min.x;

        maxY = zoneToSpawn.GetComponent<BoxCollider>().bounds.max.y;
        minY = zoneToSpawn.GetComponent<BoxCollider>().bounds.min.y;

        maxZ = zoneToSpawn.GetComponent<BoxCollider>().bounds.max.z;
        minZ = zoneToSpawn.GetComponent<BoxCollider>().bounds.min.z;

        StartCoroutine(SpawnObjectTimer());
    }

    IEnumerator SpawnObjectTimer()
    {
        yield return new WaitForSeconds(timeToSpawn);
        SpawnObject();
        StartCoroutine(SpawnObjectTimer());
    }

    void SpawnObject()
    {
        int randomPrefab;
        if (GameManager.sharedInstance.coinsToSpawn > 0 )
        {
            GameManager.sharedInstance.coinsToSpawn--;
            randomPrefab = 0;
        } else
        {
            GameManager.sharedInstance.targetDestroyed = false;
            randomPrefab = Random.Range(0, objectsPrefabs.Length);
        }
        GameObject randomObject = objectsPrefabs[randomPrefab];
        float randomX = Random.Range(minX, maxX);
        float randomY = Random.Range(minY, maxY);
        float randomZ = Random.Range(maxZ, minZ);
        Vector3 randonPos = new Vector3(randomX, randomY, randomZ);
        Instantiate(randomObject, randonPos, Quaternion.identity);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AuxSpawner : MonoBehaviour
{
    float maxX, minX, maxY, minY, maxZ, minZ;
    [SerializeField] BoxCollider zoneToSpawn;
    public int randomPrefab;
    public Vector3 randomPos;
    private void Start()
    {
        maxX = zoneToSpawn.GetComponent<BoxCollider>().bounds.max.x;
        minX = zoneToSpawn.GetComponent<BoxCollider>().bounds.min.x;

        maxY = zoneToSpawn.GetComponent<BoxCollider>().bounds.max.y;
        minY = zoneToSpawn.GetComponent<BoxCollider>().bounds.min.y;

        maxZ = zoneToSpawn.GetComponent<BoxCollider>().bounds.max.z;
        minZ = zoneToSpawn.GetComponent<BoxCollider>().bounds.min.z;
    }
    public void CalculateRandom()
    {

        randomPrefab = Random.Range(0, 5);
        float randomX = Random.Range(minX, maxX);
        float randomY = Random.Range(minY, maxY);
        float randomZ = Random.Range(maxZ, minZ);
        randomPos = new Vector3(randomX, randomY, randomZ);
    }
}

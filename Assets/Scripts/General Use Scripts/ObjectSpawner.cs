using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    [SerializeField] private GameObject objectToSpawnPrefab;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private bool autoRespawn = true;
    [SerializeField] private float spawnInterval = 5f;

    [SerializeField] private List<GameObject> allSpawnables;

    private GameObject currentSpawnedObject;
    private float respawnTimer;

    void Start()
    {
        objectToSpawnPrefab = allSpawnables[Random.Range(0, allSpawnables.Count - 1)];
        if (spawnPoint == null)
        {
            spawnPoint = transform; //use spawner's position if no spawn point assigned
        }
        if (objectToSpawnPrefab == null)
        {
            Debug.LogError("ObjectSpawner: No prefab assigned to spawn.");
            enabled = false;
            return;
        }
        if(autoRespawn)
        {
            SpawnObject();
        }
    }

    void Update()
    {
        if(respawnTimer > 0 && currentSpawnedObject == null)
        {
            respawnTimer -= Time.deltaTime;
            if(respawnTimer <= 0 && autoRespawn)
            {
                SpawnObject();
            }
        }
    }

    public void SpawnObject()
    {
        if(currentSpawnedObject != null)
        {
            return; //object already exists, do not spawn another
        }

        objectToSpawnPrefab = allSpawnables[Random.Range(0, allSpawnables.Count - 1)];
        currentSpawnedObject = Instantiate(objectToSpawnPrefab, spawnPoint.position, spawnPoint.rotation);
        respawnTimer = spawnInterval;
    }
}

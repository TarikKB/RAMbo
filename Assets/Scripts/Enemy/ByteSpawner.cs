using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ByteSpawner : MonoBehaviour
{

    public GameObject[] spawnPoints;

    public GameObject bytePrefab;

    public int spawnRate = 1;
    public int spawnCount = 10;

    private bool canSpawn = true;
    
    private int byteCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        SpawnByte();
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        if (canSpawn && byteCount < spawnCount) {
                canSpawn = false;
                SpawnByte();
                Invoke("ResetSpawn", spawnRate);
                byteCount++;
        }
        
    }

    private void ResetSpawn() {
        canSpawn = true;
    }

    public void SpawnByte() {
        int spawnPointIndex = Random.Range(0, spawnPoints.Length);
        Instantiate(bytePrefab, spawnPoints[spawnPointIndex].transform.position, Quaternion.identity);
    }
}

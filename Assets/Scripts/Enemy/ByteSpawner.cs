using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Analytics;

public class ByteSpawner : MonoBehaviour
{

    public GameObject[] spawnPoints;

    public GameObject bytePrefab;

    public GameObject megaBytePrefab;

    public GameObject pointPrefab;

    public int spawnRate = 1;
    public int spawnCount = 0;

    public bool canSpawn = false;
    
    public int byteCount = 0;

    private Camera cam;

    public GameObject leftBound;
    public GameObject rightBound;
    public GameObject topBound;
    public GameObject bottomBound;

    private WaveManager waveManager;
    // Start is called before the first frame update
    void Start()
    {
        waveManager = GetComponent<WaveManager>();
        cam = Camera.main;
        float camHeight = 2f * cam.orthographicSize + 2f;
        float camWidth = camHeight * cam.aspect;
        spawnPoints = SetSpawnpoints(camHeight, camWidth, 4f, 3f);
        SetBounds();
        
    }

    public void CamUpdate() {
        foreach (GameObject point in spawnPoints) {
            Destroy(point);
        }
        cam = Camera.main;
        float camHeight = 2f * cam.orthographicSize + 2f;
        float camWidth = camHeight * cam.aspect;
        spawnPoints = SetSpawnpoints(camHeight, camWidth, 4f, 3f);
        SetBounds();
        
    }

    private GameObject[] SetSpawnpoints(float camHeight, float camWidth, float xOffset, float yOffset) {
        List<GameObject> tmp = new List<GameObject>();
        int xPointCount = (int) (camWidth / xOffset);
        int yPointCount = (int) (camHeight / yOffset);
        for (int i = 0; i < xPointCount-1; i++) {
            tmp.Add(Instantiate(pointPrefab, new Vector3(i * xOffset - camWidth/2 + xOffset, camHeight/2, 0), Quaternion.identity, this.transform));
            tmp.Add(Instantiate(pointPrefab, new Vector3(i * xOffset - camWidth/2 + xOffset, -camHeight/2, 0), Quaternion.identity, this.transform));
            
        }
        for (int i = 0; i < yPointCount-1; i++) {
            tmp.Add(Instantiate(pointPrefab, new Vector3(camWidth/2, i * yOffset - camHeight/2 + yOffset, 0), Quaternion.identity, this.transform));
            tmp.Add(Instantiate(pointPrefab, new Vector3(-camWidth/2, i * yOffset - camHeight/2 + yOffset, 0), Quaternion.identity, this.transform));
        }
        GameObject[] final = tmp.ToArray();
        return final;

    }

    private void SetBounds() {
        leftBound.GetComponent<BoxCollider2D>().size = new Vector2(1, cam.orthographicSize*2);
        leftBound.transform.position = new Vector3(-cam.aspect * cam.orthographicSize - 0.5f, 0, 0);

        rightBound.GetComponent<BoxCollider2D>().size = new Vector2(1, cam.orthographicSize*2);
        rightBound.transform.position = new Vector3(cam.aspect * cam.orthographicSize + 0.5f, 0, 0);

        topBound.GetComponent<BoxCollider2D>().size = new Vector2(cam.aspect * cam.orthographicSize * 2, 1);
        topBound.transform.position = new Vector3(0, cam.orthographicSize + 0.5f, 0);

        bottomBound.GetComponent<BoxCollider2D>().size = new Vector2(cam.aspect * cam.orthographicSize * 2, 1);
        bottomBound.transform.position = new Vector3(0, -cam.orthographicSize - 0.5f, 0);



    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        if (canSpawn && byteCount < spawnCount) {
            
            canSpawn = false;
            if (byteCount % 8 == 0 && waveManager.curWave > 2) {
                SpawnMegaByte();
            } else {
                SpawnByte();
            }
            Invoke("ResetSpawn", spawnRate);
            byteCount++;
        }
        
    }

    private void ResetSpawn() {
        canSpawn = true;
    }

    public void SpawnByte() {
        int spawnPointIndex = Random.Range(0, spawnPoints.Length);
        WaveManager.enemies.Add(Instantiate(bytePrefab, spawnPoints[spawnPointIndex].transform.position, Quaternion.identity));
    }

    public void SpawnMegaByte() {
        int spawnPointIndex = Random.Range(0, spawnPoints.Length);
        WaveManager.enemies.Add(Instantiate(megaBytePrefab, spawnPoints[spawnPointIndex].transform.position, Quaternion.identity));
    }

}

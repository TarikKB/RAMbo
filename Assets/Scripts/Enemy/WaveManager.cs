using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    private ByteSpawner byteSpawner;
    public int curWave = 1;

    public bool awaitingWave = false;

    public GameObject memUp;

    public static List<GameObject> enemies = new List<GameObject>();

    private float camStartSize;
    private float camEndSize;

    public GameObject canvas;

    private float t = 0;

    private GameObject player;

    void Start()
    {
        player = GameObject.Find("Player");
        byteSpawner = GetComponent<ByteSpawner>();
        camStartSize = Camera.main.orthographicSize;
        camEndSize = camStartSize;
        
    }

    void FixedUpdate()
    {
        if (enemies.Count == 0 && !awaitingWave)
        {
            awaitingWave = true;
            StartCoroutine(StartWave());
        }
        if (camStartSize != camEndSize) {
            t += Time.deltaTime/16 + 0.15f * t;
            Camera.main.orthographicSize = Mathf.Lerp(camStartSize, camEndSize, t);
            if (Camera.main.orthographicSize == camEndSize) {
                camStartSize = camEndSize;
            }
        } else {
            t = 0;
        }
    }

    public IEnumerator StartWave()
    {
        print("Starting wave " + curWave);
        byteSpawner.canSpawn = false;
        byteSpawner.byteCount = 0;
        if (curWave > 1)
        {
            GameObject memUpPrefab = Instantiate(memUp);
            StartCoroutine(DestroyMemUp(memUpPrefab));
            player.GetComponent<PlayerController>().speed += 2;
            if (player.GetComponent<AttackScript>().cooldown > 0.2f)
                player.GetComponent<AttackScript>().cooldown -= 0.2f;
            camEndSize += 1.5f;
            yield return new WaitForSeconds(1f);
            byteSpawner.CamUpdate();
        }
        yield return new WaitForSeconds(3f);
        byteSpawner.canSpawn = true;
        byteSpawner.spawnCount = 8 * curWave + (int)Mathf.Pow(curWave, Random.Range(3, 5));
        print("Spawning " + byteSpawner.spawnCount + " enemies");
        curWave++;
        Invoke("WaveDelay", 5f);
    }

    IEnumerator DestroyMemUp(GameObject memUp)
    {
        yield return new WaitForSeconds(3f);
        Destroy(memUp);
    }

    

    private void WaveDelay() {
        awaitingWave = false;
    }



}

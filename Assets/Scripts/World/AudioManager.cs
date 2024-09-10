using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public AudioClip explosionSound;
    public AudioClip playerHitSound;

    public AudioClip openingMusic;

    public AudioClip musicLoop;

    public AudioClip byteDead;

    private AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        
        
        
    }

    public void StartOpening() {
        
        audioSource.clip = openingMusic;
        audioSource.Play();
        audioSource.loop = false;
        Invoke("StartLoop", openingMusic.length -0.05f);
    }
    private void StartLoop() {
        audioSource.clip = musicLoop;
        audioSource.Play();
        audioSource.loop = true;
    }

    // Update is called once per frame
    public static void PlaySound(AudioClip clip, Vector3 position)
    {
        
        GameObject soundObject = new GameObject("Sound");
        soundObject.transform.position = position;
        AudioSource audioSource = soundObject.AddComponent<AudioSource>();
        audioSource.volume = 0.5f;
        audioSource.clip = clip;
        audioSource.Play();
        Destroy(soundObject, clip.length + 0.5f);
    }
}

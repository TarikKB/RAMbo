using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{

    public AudioClip explosionSound;
    public AudioClip playerHitSound;

    public AudioClip openingMusic;

    public AudioClip musicLoop;

    public AudioClip byteDead;

    private AudioSource audioSource;

    
    public static AudioMixer mixer;

    public static AudioMixerGroup master;

    public static AudioMixerGroup sfx;

    public static AudioMixerGroup player;

    private AudioMixerGroup music;

    const float minVolume = -80f;
    const float maxVolume = 20f;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        mixer = Resources.Load<AudioMixer>("Master");
        //master = mixer.FindMatchingGroups("Master")[0];
        sfx = mixer.FindMatchingGroups("SFX")[0];
        player = mixer.FindMatchingGroups("Player")[0];
        music = mixer.FindMatchingGroups("Music")[0];
        
        float musicVolume = PlayerPrefs.GetFloat("Volume", 1f);
        float sfxVolume = PlayerPrefs.GetFloat("SFXVolume", 1f);
        float masterVolume = PlayerPrefs.GetFloat("MasterVolume", 1f);
        //mixer.SetFloat("volume", sfxVolume * 7f);
        mixer.SetFloat("MasterVolume", masterVolume);
        sfx.audioMixer.SetFloat("SFXVolume", sfxVolume);
        player.audioMixer.SetFloat("PlayerVolume", sfxVolume);
        music.audioMixer.SetFloat("MusicVolume", musicVolume);
        
        
        
    }

    public void SetMusicVolume(Slider volume) {
        music.audioMixer.SetFloat("MusicVolume", volume.value);
        PlayerPrefs.SetFloat("MusicVolume", volume.value);
    }

    public void SetSFXVolume(Slider volume) {
        sfx.audioMixer.SetFloat("SFXVolume", volume.value);
        player.audioMixer.SetFloat("PlayerVolume", volume.value);
        PlayerPrefs.SetFloat("SFXVolume", volume.value);
    }

    public void SetMasterVolume(Slider volume) {
        mixer.SetFloat("MasterVolume", volume.value);
        PlayerPrefs.SetFloat("MasterVolume", volume.value);
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
    public static void PlaySound(AudioClip clip, Vector3 position, string mixer = "Master")
    {
        
        GameObject soundObject = new GameObject("Sound");
        soundObject.transform.position = position;
        AudioSource audioSource = soundObject.AddComponent<AudioSource>();
        if (mixer == "Master") {
            audioSource.volume = 0.5f;
            audioSource.outputAudioMixerGroup = sfx;
        } else if (mixer == "Player") {
            audioSource.outputAudioMixerGroup = player;
            
        }
        //audioSource.volume = 0.5f;
        audioSource.clip = clip;
        
        audioSource.Play();
        Destroy(soundObject, clip.length + 0.5f);
    }
}

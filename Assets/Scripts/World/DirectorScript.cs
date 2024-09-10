using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DirectorScript : MonoBehaviour
{

    public TextMeshProUGUI openingText;

    public GameObject openingPanel;
    private string fullText;

    public float typingSpeed = 0.1f;

    public AudioClip typingSound;

    public AudioManager audioManager;

    public GameObject player;

    public GameObject byteSpawner;

    public static bool firstLaunch = true;

    // Start is called before the first frame update
    void Start()
    {
        if (firstLaunch)
        {
            player.SetActive(false);
            byteSpawner.SetActive(false);
            fullText = openingText.text;
            StartCoroutine(DisplayLine(fullText));
            
            firstLaunch = false;
        }
        else
        {
            //openingText.gameObject.SetActive(false);
            openingPanel.SetActive(false);
        }
        
        
    }

    public void StartGame() {
        player.SetActive(true);
        byteSpawner.SetActive(true);
        Destroy(openingPanel);
        audioManager.StartOpening();
    }

    // Update is called once per frame
    private IEnumerator DisplayLine(string line)
    {
        openingText.text = "";
        foreach (char c in line)
        {
            float puncDelay = 0;
            openingText.text += c;
            if (c == '!' || c == '.' || c == '?')
            {
                puncDelay = 0.2f;
            } else if (c == ',')
            {
                puncDelay = 0f;
            }
            yield return new WaitForSeconds(typingSpeed + puncDelay);
        }
    }
}

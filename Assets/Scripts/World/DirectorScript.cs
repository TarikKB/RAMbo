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

    public static bool gameIsPaused = false;

    public GameObject pauseMenu;

    // Start is called before the first frame update
    void Start()
    {
        if (firstLaunch)
        {
            openingPanel.SetActive(true);
            player.SetActive(false);
            byteSpawner.SetActive(false);
            fullText = openingText.text;
            StartCoroutine(DisplayLine(fullText));
            
            firstLaunch = false;
        } else
        {
            StartGame();
        }
        
        
    }

    public void Pause() {
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
        gameIsPaused = true;
        
    }

    public void Resume() {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
        gameIsPaused = false;
    }

    public void StartGame() {
        player.SetActive(true);
        byteSpawner.SetActive(true);
        Destroy(openingPanel);
        audioManager.StartOpening();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameIsPaused)
            {
                Resume();
            } else
            {
                Pause();
            }
        }
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

    public void MainMenu()
    {
        Time.timeScale = 1;
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
    
    public void Restart()
    {
        Time.timeScale = 1;
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager controller;

    private void Awake()
    {
        if (controller == null)
        {
            DontDestroyOnLoad(gameObject);
            controller = this;
            EventManager.StartListening("EndGame", BackToMenu);
        }
        else if (controller != this)
        {
            Destroy(gameObject);
        }
    }

    public GameObject pauseMenuPanel;
    public KeyCode quitKey, continueKey;

    private bool pauseMenu;

    public GameObject fadeScreen;

	private void Update ()
    {
		if (Input.GetKeyDown(quitKey))
        {
            if (pauseMenu)
            {
                Time.timeScale = 1;
                UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
            }
            else
            {
                pauseMenu = true;
                pauseMenuPanel.SetActive(true);
                Time.timeScale = 0;
            }
        }

        if (Input.GetKeyDown(continueKey))
        {
            if (pauseMenu)
            {
                pauseMenu = false;
                pauseMenuPanel.SetActive(false);
                Time.timeScale = 1;
                
            }
        }

        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "MainMenu")
        {
            Destroy(gameObject);
        }
	}

    public void BackToMenu ()
    {
        fadeScreen.SetActive(true);
        StartCoroutine(Fade());
    }

    public IEnumerator Fade()
    {
        yield return new WaitForSeconds(2);
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }
}

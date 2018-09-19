using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public GameObject pauseMenuPanel;
    public KeyCode quitKey, continueKey;

    private bool pauseMenu;

	private void Update ()
    {
		if (Input.GetKeyDown(quitKey))
        {
            if (pauseMenu)
            {
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
	}
}

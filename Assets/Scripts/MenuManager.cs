using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//A script that takes care of all the transitions from the main menu.
//Uses two different versions.
public class MenuManager : MonoBehaviour {
    
    //The buttons used to transition, defined in the inspector.
    public KeyCode start, controls, credits, quit;
    //The two panels that hold the controls and credits information.
    public GameObject controlsScreen, creditsScreen;

    public GameObject fadeScreen;

    //Uncomment this if you want to press a button on the keyboard to start the game.

    private void Update()
    {
        if (Input.GetKeyDown(start))
        {
            Debug.Log("You have started the game!");
            StartCoroutine(Fade());
            
        }

        if (Input.GetKeyDown(quit))
        {
            Debug.Log("You quit the game!");
            Application.Quit();
        }

        if (Input.GetKeyDown(controls))
        {
            Debug.Log("Toggling the controls!");
            creditsScreen.SetActive(false);
            if (controlsScreen.activeInHierarchy) controlsScreen.SetActive(false);
            else controlsScreen.SetActive(true);
        }

        if (Input.GetKeyDown(credits))
        {
            Debug.Log("Toggling the credits!");
            controlsScreen.SetActive(false);
            if (creditsScreen.activeInHierarchy) creditsScreen.SetActive(false);
            else creditsScreen.SetActive(true);
        }
    }

    public IEnumerator Fade()
    {
        fadeScreen.SetActive(true);
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("GameScene");
    }


    //Uncomment these functions if you want to use UI buttons in the scene.

//  public void StartGame ()
//  {
//      SceneManager.LoadScene("GameScene");
//  }
//  
//  public void Controls ()
//  {
//      controlsScreen.SetActive(true);
//  }
//  
//  public void Credits ()
//  {
//      creditsScreen.SetActive(true);
//  }
//  
//  public void QuitGame ()
//  {
//      Application.Quit();
//  }
//  
//  public void ReturnToMainMenu ()
//  {
//      var currentScene = SceneManager.GetActiveScene().name;
//      if (currentScene == "MainMenu")
//      {
//          controlsScreen.SetActive(false);
//          creditsScreen.SetActive(false);
//      }
//  }
}

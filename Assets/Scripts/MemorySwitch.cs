using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemorySwitch : MonoBehaviour {

    public GameObject fadeImage;
    private Animator fadeAnim;

    public GameObject flashImage;

    public float waitTime;
    public string currentScene;

    private void Start()
    {
        fadeAnim = fadeImage.GetComponent<Animator>();
        currentScene = "GameScene";
    }

    private void OnEnable()
    {
        EventManager.StartListening("TextDone", FadeOut);
        EventManager.StartListening("OnMemoryGo", FadeIn);
        EventManager.StartListening("OnMemoryStop", FadeIn);
    }

    private void OnDisable()
    {
        EventManager.StopListening("TextDone", FadeOut);
        EventManager.StopListening("OnMemoryGo", FadeIn);
        EventManager.StopListening("OnMemoryStop", FadeIn);
    }

    public void FadeIn ()
    {
        Debug.Log("Fade In");
        StartCoroutine(WaitForSwitch());
    }

    public void SwitchToBus ()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("GameScene");
        currentScene = "GameScene";
    }

    public void SwitchToMemory ()
    {
        Debug.Log("Switch To Memory");
        UnityEngine.SceneManagement.SceneManager.LoadScene("Memory");
        currentScene = "Memory";
    }

    public IEnumerator WaitForSwitch()
    {
        Debug.Log("Wait for switch: " + currentScene + " " + waitTime);
        fadeAnim.SetTrigger("FadeIn");
        yield return new WaitForSeconds(waitTime);

        if (currentScene == "GameScene")
        {
            EventManager.TriggerEvent("OnMemoryText");
        }

        else if (currentScene == "Memory")
        {
            EventManager.TriggerEvent("OnMemoryText2");
        }
    }

    public void FadeOut ()
    {
        Debug.Log("FadeOut " + currentScene);
        if (currentScene == "GameScene")
        {
            SwitchToMemory();
        }
        else if (currentScene == "Memory")
        {
            SwitchToBus();
            StartCoroutine(MemorySwitchDelay());
        }

        fadeAnim.SetTrigger("FadeOut");
    }

    IEnumerator MemorySwitchDelay()
    {
        yield return null;
        Debug.Log("Memory Switch Delay");
        EventManager.TriggerEvent("OnCountdownStart");
        EventManager.TriggerEvent("WalkingToggle");
        flashImage.SetActive(true);
    }
}


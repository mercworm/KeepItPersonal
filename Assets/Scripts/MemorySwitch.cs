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
        StartCoroutine(WaitForSwitch());
    }

    public void SwitchToBus ()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("GameScene");
        currentScene = "GameScene";
    }

    public void SwitchToMemory ()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Memory");
        currentScene = "Memory";
    }

    public IEnumerator WaitForSwitch()
    {
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
        if (currentScene == "GameScene")
        {
            SwitchToMemory();
        }
        else if (currentScene == "Memory")
        {
            SwitchToBus();
            EventManager.TriggerEvent("OnCountdownStart");
            EventManager.TriggerEvent("WalkingToggle");
            flashImage.SetActive(true);
        }

        fadeAnim.SetTrigger("FadeOut");
    }
}


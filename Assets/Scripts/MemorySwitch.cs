using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemorySwitch : MonoBehaviour {

    public GameObject busScene;
    public GameObject memoryScene;

    public GameObject fadeImage;
    private Animator fadeAnim;

    public GameObject flashImage;

    public float waitTime;

    private void Start()
    {
        fadeAnim = fadeImage.GetComponent<Animator>();
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
        busScene.SetActive(true);
        memoryScene.SetActive(false);
    }

    public void SwitchToMemory ()
    {
        busScene.SetActive(false);
        memoryScene.SetActive(true);
    }

    public IEnumerator WaitForSwitch()
    {
        fadeAnim.SetTrigger("FadeIn");
        yield return new WaitForSeconds(waitTime);

        if (busScene.activeInHierarchy)
        {
            EventManager.TriggerEvent("OnMemoryText");
        }

        else if (memoryScene.activeInHierarchy)
        {
            EventManager.TriggerEvent("OnMemoryText2");
        }
    }

    public void FadeOut ()
    {
        if (busScene.activeInHierarchy)
        {
            SwitchToMemory();
        }
        else if (memoryScene.activeInHierarchy)
        {
            SwitchToBus();
            EventManager.TriggerEvent("OnCountdownStart");
            EventManager.TriggerEvent("WalkingToggle");
            flashImage.SetActive(true);
        }

        fadeAnim.SetTrigger("FadeOut");
    }
}


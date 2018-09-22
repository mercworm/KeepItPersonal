using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemorySwitch : MonoBehaviour {

    public GameObject busScene;
    public GameObject memoryScene;

    public GameObject fadeImage;
    private Animator fadeAnim;

    public float waitTime;

    private void Start()
    {
        fadeAnim = fadeImage.GetComponent<Animator>();
    }

    private void OnEnable()
    {
        EventManager.StartListening("TextDone", FadeOut);
        EventManager.StartListening("OnMemoryGo", SwitchToMemory);
        EventManager.StartListening("OnMemoryStop", SwitchToBus);
    }

    private void OnDisable()
    {
        EventManager.StopListening("TextDone", FadeOut);
        EventManager.StopListening("OnMemoryGo", SwitchToMemory);
        EventManager.StopListening("OnMemoryStop", SwitchToBus);
    }

    public void SwitchToMemory ()
    {
        StartCoroutine(WaitForSwitch());
    }

    public void SwitchToBus ()
    {
        StartCoroutine(WaitForSwitch());
    }

    public IEnumerator WaitForSwitch()
    {
        fadeAnim.SetTrigger("FadeIn");

        yield return new WaitForSeconds(waitTime);

        if (busScene.activeInHierarchy)
        {
            EventManager.TriggerEvent("OnMemoryText");
            busScene.SetActive(false);
            memoryScene.SetActive(true);
        }

        else if (memoryScene.activeInHierarchy)
        {
            EventManager.TriggerEvent("OnMemoryText2");
            busScene.SetActive(true);
            memoryScene.SetActive(false);
        }
    }

    public void FadeOut ()
    {
        fadeAnim.SetTrigger("FadeOut");
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemorySwitch : MonoBehaviour {

    public GameObject busScene;
    public GameObject memoryScene;

    public GameObject fadeImage;

    public float waitTime;

    private void OnEnable()
    {
        EventManager.StartListening("OnMemoryGo", SwitchToMemory);
        EventManager.StartListening("OnMemoryStop", SwitchToBus);
    }

    private void OnDisable()
    {
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
        var fadeAnim = fadeImage.GetComponent<Animator>();
        fadeAnim.SetTrigger("FadeIn");

        yield return new WaitForSeconds(waitTime);

        if (busScene.activeInHierarchy)
        {
            
            busScene.SetActive(false);
            memoryScene.SetActive(true);
        }

        else if (memoryScene.activeInHierarchy)
        {
            busScene.SetActive(true);
            memoryScene.SetActive(false);
        }
    }
}


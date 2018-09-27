using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimationToggle : MonoBehaviour {

    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        EventManager.StartListening("OnMemoryText", StartMemory);
        EventManager.StartListening("OnMemoryText2", StopMemory);
    }

    private void OnDisable()
    {
        EventManager.StopListening("OnMemoryText", StartMemory);
        EventManager.StopListening("OnMemoryText2", StopMemory);
    }

    //Shows the text before the memory plays.
    public void StartMemory ()
    {
        anim.SetTrigger("Text1Go");
    }

    //Shows the text when the memory is finished.
    public void StopMemory ()
    {
        anim.SetTrigger("Text2Go");
    }

    public void TextDone ()
    {
        Debug.Log("TextDone");
        EventManager.TriggerEvent("TextDone");
    }
}

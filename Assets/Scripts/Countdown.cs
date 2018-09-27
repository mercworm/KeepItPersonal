using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Countdown : MonoBehaviour {

    public float countdownTime = 10f;
    public bool counting = false;

    public Sprite[] numberSprites;
    public int currentSprite;
    public Image countdownImage;

    public GameObject panelHolder;

    private void OnEnable()
    {
        EventManager.StartListening("OnCountdownStart", StartCount);
    }

    private void OnDisable()
    {
        EventManager.StopListening("OnCountdownStart", StartCount);
    }

    public void StartCount ()
    {
        if (panelHolder != null)
        {
            panelHolder.SetActive(true);
            counting = true;
        }
    }

    private void Update ()
    {
        //If we're currently counting down, keep doing that!
		if (counting)
        {
            InvokeRepeating("TimeDown", 0.0f, 1.0f);
            counting = false;
        }

        //If we've reached 0.
        if (countdownTime < 0)
        {
            //Stop counting and tell everyone else that it is over. 
            countdownTime = 10f;
            CancelInvoke();
            EventManager.TriggerEvent("EndGame");
        }
    }

    public void TimeDown ()
    {
        countdownTime--;
        SwitchSprite();
    }

    public void SwitchSprite ()
    {
        currentSprite++;
        countdownImage.sprite = numberSprites[currentSprite];

        EventManager.TriggerEvent("OneSecondDown");
    }

    public void Stop ()
    {
        currentSprite = 0;
        EventManager.TriggerEvent("OnMemoryStop");
    }
}

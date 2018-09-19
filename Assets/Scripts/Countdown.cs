using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Countdown : MonoBehaviour {

    public float countdownTime = 10f;
    public bool counting = false;

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
        counting = true;
    }

    private void Update ()
    {
        //If we're currently counting down, keep doing that!
		if (counting)
        {
            //If there's still something to remove...
            if (countdownTime >= 0)
            {
                countdownTime -= Time.deltaTime;
                EventManager.TriggerEvent("OneSecondDown");
            }
            //If we've reached 0.
            if (countdownTime < 0)
            {
                //Stop counting and tell everyone else that it is over. 
                counting = false;
                countdownTime = 10f;
                EventManager.TriggerEvent("OnMemoryStop");
            }
        }
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

    public AudioSource musicSource, effectSource;

    public AudioClip busMusic, memoryMusic, breathing, boom, heartbeat;

    private void OnEnable()
    {
        //EventManager.StartListening("OnMemoryGo", Memory);
        //EventManager.StartListening("OnMemoryStop", MusicFade);
        //EventManager.StartListening("TextDone", Memory);
        //EventManager.StartListening("OnSecondDown", EverySecond);
    }

    private void OnDisable()
    {
        EventManager.StopListening("OnMemoryGo", MusicFade);
        EventManager.StopListening("OnMemoryStop", MusicFade);
        EventManager.StopListening("OnSecondDown", EverySecond);
    }

    public void MusicFade ()
    {
        StartCoroutine(FadeOut(musicSource, 0.5f));
    }

    public void Bus ()
    {

    }

    public void Memory ()
    {
        musicSource.clip = memoryMusic;
        musicSource.Play();

        effectSource.PlayOneShot(breathing);
        effectSource.PlayOneShot(heartbeat);
    }

    public void EverySecond ()
    {
        effectSource.PlayOneShot(boom);
    }

    public IEnumerator FadeOut(AudioSource audioSource, float FadeTime)
    {
        float startVolume = audioSource.volume;

        while (audioSource.volume > 0)
        {
            audioSource.volume -= startVolume * Time.deltaTime / FadeTime;

            yield return null;
        }

        audioSource.Stop();
        audioSource.volume = startVolume;
    }
}

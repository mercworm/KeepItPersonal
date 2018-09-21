using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

    private AudioSource source;

    public AudioClip busMusic, memoryMusic, breathing, boom, heartbeat;

    private void OnEnable()
    {
        EventManager.StartListening("OnMemoryGo", Memory);
        EventManager.StartListening("OnSecondDown", EverySecond);
    }

    private void OnDisable()
    {
        EventManager.StopListening("OnMemoryGo", Memory);
        EventManager.StopListening("OnSecondDown", EverySecond);
    }

    // Use this for initialization
    void Start () {
        source = GetComponent<AudioSource>();
	}

    public void Memory ()
    {
        StartCoroutine(FadeOut(source, 0.5f));
        source.clip = memoryMusic;
        source.Play();

        source.PlayOneShot(breathing);
        source.PlayOneShot(heartbeat);
    }

    public void EverySecond ()
    {
        source.PlayOneShot(boom);
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

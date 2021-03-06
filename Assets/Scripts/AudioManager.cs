﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

    public AudioSource musicSource, effectSource, _playerSource;

    public AudioSource PlayerSource
    {
        get
        {
            if (_playerSource == null)
            { 
                var p = GameObject.FindGameObjectWithTag("Player");
                if(p != null)
                {
                    _playerSource = p.GetComponent<AudioSource>();
                }
            }

            return _playerSource;
        }
    }

    public AudioClip busMusic, memoryMusic, breathing, boom, heartbeat, busSound;

    private void OnEnable()
    {
        EventManager.StartListening("OnMemoryGo", MusicFade);
        EventManager.StartListening("OnMemoryStop", MusicFade);
        EventManager.StartListening("TextDone", SwitchMusic);
        EventManager.StartListening("OneSecondDown", EverySecond);
    }

    private void OnDisable()
    {
        EventManager.StopListening("OnMemoryGo", MusicFade);
        EventManager.StopListening("OnMemoryStop", MusicFade);
        EventManager.StartListening("TextDone", SwitchMusic);
        EventManager.StopListening("OneSecondDown", EverySecond);
    }

    public void MusicFade ()
    {
        StartCoroutine(FadeOut(musicSource, 1f));
        StartCoroutine(FadeOut(effectSource, 1f));
    }

    public void SwitchMusic ()
    {
        if (musicSource.clip == busMusic)
        {
            musicSource.clip = memoryMusic;
            musicSource.Play();

            //effectSource.clip = breathing;
            //effectSource.Play();
            effectSource.PlayOneShot(heartbeat);
        }
        else if(musicSource.clip == memoryMusic)
        {
            musicSource.clip = busMusic; //This needs to be whatever music I decide that I want later.
            musicSource.Play();

            effectSource.clip = busSound; 
            effectSource.Play();
            effectSource.PlayOneShot(heartbeat);
        }
    }

    public void EverySecond ()
    {
        if(musicSource.clip == busMusic)
        {
            PlayerSource.PlayOneShot(boom);
        }
        else
        {
            effectSource.PlayOneShot(boom);
        }

        
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

﻿using UnityEngine.Audio;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{

    public Sound[] sounds;
    // Start is called before the first frame update
    void Awake()
    {
        foreach (Sound s in sounds){
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    // Update is called once per frame
    public void Play(string name){
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null) {
            Debug.LogWarning("Sound" + name +  "not found");
            return;
        }
        s.source.Play();
    }

    public void PlayInteractable(string name){
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null) {
            Debug.LogWarning("Sound" + name +  "not found");
            return;
        }
        if (!s.source.isPlaying){
            s.source.Play();
        }
    }
    public void StopInteractable(string name){
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null) {
            Debug.LogWarning("Sound " + name +  " not found");
            return;
        }
        s.source.Stop();
    }

    public void PlayOverlapping(string name){
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null) {
            Debug.LogWarning("Sound " + name +  " not found");
            return;
        }
        s.source.PlayOneShot(s.clip);
    }
}
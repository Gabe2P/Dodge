﻿using System;
using UnityEngine;
using UnityEngine.Audio;

//This Script is not of my Creation and All credit goes to Brackeys!!! I do not Claim any of this as mine!!!

public class AudioManager : MonoBehaviour
{

    //Manages all of the different audio files

    public Sound[] sounds;

    public static AudioManager instance;

    private void Awake()
    {
        if (AudioManager.instance == null)
        {
            AudioManager.instance = this;
        }

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    public void SetVolume(string name, float amount)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null && amount < 1 && amount > 0)
        {
            Debug.LogWarning("Sound: " + name + " not found or Amount not in Range!");
            return;
        }
        s.source.volume = amount;
    }

    public void SetMultipleVolumes(Sound[] sounds, float amount)
    {
        foreach (Sound s in sounds)
        {
            if (s == null && amount <= 1 && amount >= 0)
            {
                Debug.LogWarning("Sound: " + name + " not found!");
                return;
            }
            s.source.volume = amount;
        }
    }

    public void SetPitch(string name, float amount)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null && amount < .1f && amount > 3f)
        {
            Debug.LogWarning("Sound: " + name + " not found or Amount not in Range!");
            return;
        }
        s.source.pitch = amount;
    }

    public void SetLoop(string name, bool active)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        s.source.loop = active;
    }

    public void Play (string name)
    {
        if (name.Equals("") || name == null)
        {
            return;
        }
        else
        {
            Sound s = Array.Find(sounds, sound => sound.name == name);
            if (s == null)
            {
                Debug.LogWarning("Sound: " + name + " not found!");
                return;
            }
            s.source.Play();
        }
    }

    public void Stop(string name)
    {
        if (name.Equals("") || name == null)
        {
            return;
        }
        else
        {
            Sound s = Array.Find(sounds, sound => sound.name == name);
            if (s == null)
            {
                Debug.LogWarning("Sound: " + name + " not found!");
                return;
            }
            s.source.Stop();
        }
    }
}

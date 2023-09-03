using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{
    private static AudioManager _audioManager;
    private AudioSource _audioSource;

    private void Awake()
    {
        if (_audioManager == null)
        {
            _audioManager = this;
        }
        _audioSource = GetComponent<AudioSource>();
    }

    public static AudioManager Shared()
    {
        return _audioManager;
    }

    public void PlaySoundOnce(AudioClip audioOnce)
    {
        _audioSource.PlayOneShot(audioOnce);
    }
}

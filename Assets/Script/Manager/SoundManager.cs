using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
    [SerializeField] AudioSource _musicSource, _effectSource;
    public void PlaySound(AudioClip clip)
    {
        _effectSource.PlayOneShot(clip);
    }
    public void ChangeMasterVolume(float value)
    {
        AudioListener.volume = value;
    }
}

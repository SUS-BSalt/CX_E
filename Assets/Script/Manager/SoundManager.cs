using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
    [SerializeField] AudioSource _musicSource, _effectSource, _LoopBackGroundSource;

    float _musicVolume, _EffectVolume, _LoopBackGroundVolume;

    public AudioClip Button;
    public AudioClip Toggle;
    public void ButtonSound()
    {
        PlaySound(Button);
    }
    public void ToggleSound(bool input)
    {
        if (input)
        {
            PlaySound(Toggle);
        }
    }
    public void PlaySound(AudioClip clip, float Modify = 1f)
    {
        _effectSource.volume = _EffectVolume * Modify;
        _effectSource.PlayOneShot(clip);
    }
    public void PlayLoopSound(AudioClip clip, float Modify = 1f)
    {
        _LoopBackGroundSource.volume = _LoopBackGroundVolume * Modify;
        _LoopBackGroundSource.clip = clip;
        _LoopBackGroundSource.loop = true;
        _LoopBackGroundSource.Play();
    }
    public void StopLoopSound()
    {
        _LoopBackGroundSource.Stop();
    }
    public void PlayMusic(AudioClip clip, float Modify = 1f)
    {
        _musicSource.volume = _musicVolume * Modify;
        _musicSource.PlayOneShot(clip);
    }
    public void StopMusic()
    {
        _musicSource.Stop();
    }
    public void ChangeMasterVolume(float value)
    {
        AudioListener.volume = value;
    }
    public void ChangeMusicVolue(float value)
    {
        _musicSource.volume = value * _musicSource.volume / _musicVolume;
        _musicVolume = value;
    }
    public void ChangeEffectVolume(float value)
    {
        _effectSource.volume = value * _effectSource.volume / _EffectVolume;
        _EffectVolume = value;
    }
    public void ChangeBackGroundVolume(float value)
    {
        _LoopBackGroundSource.volume = value * _LoopBackGroundSource.volume / _LoopBackGroundVolume;
        _LoopBackGroundVolume = value;
    }
    public void SaveCFG()
    {
        DataManager.Instance.SetData<float>(AudioListener.volume, "Profile", "LocalOption", "3", "2");
        DataManager.Instance.SetData<float>(_musicVolume, "Profile", "LocalOption", "4", "2");
        DataManager.Instance.SetData<float>(_EffectVolume, "Profile", "LocalOption", "5", "2");
        DataManager.Instance.SetData<float>(_LoopBackGroundVolume, "Profile", "LocalOption", "6", "2");
    }
    protected override void Awake()
    {
        base.Awake();
        ChangeMasterVolume(DataManager.Instance.GetData<float>("Profile", "LocalOption", "3","2"));
        ChangeMusicVolue(DataManager.Instance.GetData<float>("Profile", "LocalOption", "4", "2"));
        ChangeEffectVolume(DataManager.Instance.GetData<float>("Profile", "LocalOption", "5", "2"));
        ChangeBackGroundVolume(DataManager.Instance.GetData<float>("Profile", "LocalOption", "6", "2"));
    }
}

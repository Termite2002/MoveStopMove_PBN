using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : Singleton<SoundManager>
{
    public AudioMixer audioMixer;

    private float musicVolume = 70;
    private float seVolume = 70;

    public AudioSource[] sounds;

    private void Awake()
    {
        //Init(70, 70);
    }

    //public void PlaySFX(int sfxToPlay)
    //{
    //    sfx[sfxToPlay].Stop();
    //    sfx[sfxToPlay].Play();
    //}

    public void Init(float musicVolume, float sfxvolume)
    {
        audioMixer.SetFloat(Constant._MUSICVOLUME, musicVolume);
        audioMixer.SetFloat(Constant._SEVOLUME, sfxvolume);
    }

    public void PlaySound(int index)
    {
        sounds[index].Play();
    }
    public void ChangeVolumeMusic(float volume)
    {
        audioMixer.SetFloat(Constant._MUSICVOLUME, volume);
    }

    public void ChangeVolumeSFX(float volume)
    {
        audioMixer.SetFloat(Constant._SEVOLUME, volume);
    }
    public void MuteSound()
    {
        ChangeVolumeMusic(-80);
        ChangeVolumeSFX(-80);
    }
    public void TurnOnMusic()
    {
        ChangeVolumeMusic(0);
        ChangeVolumeSFX(0);
    }
}

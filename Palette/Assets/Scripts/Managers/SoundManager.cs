using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using DG.Tweening;

[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;
}

public class SoundManager : MonoBehaviour
{
    [SerializeField] private Sound[] arraySFX = null;
    [SerializeField] private Sound[] arrayBGM = null;

    [SerializeField] private AudioSource bgmPlayer = null;
    [SerializeField] private AudioSource[] sfxPlayerArray = null;
    [SerializeField] private AudioSource typeWritePlayer = null;

    private List<AudioSource> sfxPlayers;
    private Dictionary<string, AudioClip> dicBGM;
    private Dictionary<string, AudioClip> dicSFX;

    [SerializeField] private float bgmVolume;
    [SerializeField] private float sfxVolume;

    void Awake()
    {
        dicBGM = new Dictionary<string, AudioClip>();
        dicSFX = new Dictionary<string, AudioClip>();

        foreach (Sound sound in arrayBGM)
        {
            dicBGM.Add(sound.name, sound.clip);
        }

        foreach (Sound sound in arraySFX)
        {
            dicSFX.Add(sound.name, sound.clip);
        }

        DontDestroyOnLoad(this.gameObject);

        sfxPlayers = sfxPlayerArray.ToList();
        SetSFXVolume(0.6f);
    }

    void Start()
    {
        sfxPlayers.ForEach(sfxPlayer => sfxPlayer.volume = sfxVolume);
    }

    public void PlayTypeWriteSFX(string sfxName)
    {
        if (!dicSFX.ContainsKey(sfxName))
        {
            Debug.LogWarning("SoundManager - Sound not found : " + sfxName);
            return;
        }

        typeWritePlayer.clip = dicSFX[sfxName];
        typeWritePlayer.volume = sfxVolume;

        typeWritePlayer.Play();
    }

    public void PlaySFX(string sfxName)
    {
        if (!dicSFX.ContainsKey(sfxName))
        {
            Debug.LogWarning("SoundManager - Sound not found: " + sfxName);
            return;
        }

        foreach (var sfxPlayer in sfxPlayers)
        {
            if (!sfxPlayer.isPlaying)
            {
                sfxPlayer.clip = dicSFX[sfxName];
                sfxPlayer.volume = sfxVolume;

                sfxPlayer.Play();
                return;
            }
        }
    }

    public void PlayBGM(string bgmName)
    {
        if (!dicBGM.ContainsKey(bgmName))
        {
            Debug.LogWarning("SoundManager - Sound not found: " + bgmName);
            return;
        }

        bgmPlayer.clip = dicBGM[bgmName];
        bgmPlayer.volume = bgmVolume;

        bgmPlayer.Play();
    }

    public void StopBGM()
    {
        bgmPlayer.Stop();
    }

    public void StopSFX()
    {
        sfxPlayers.ForEach(sfxPlayer => sfxPlayer.Stop());
    }

    public void SetBGMVolume(float volume)
    {
        bgmVolume = Mathf.Clamp01(volume);

        bgmPlayer.volume = bgmVolume;
    }

    public void SetSFXVolume(float volume)
    {
        sfxVolume = Mathf.Clamp01(volume * 0.5f);

        sfxPlayers.ForEach(sfxPlayer => sfxPlayer.volume = sfxVolume);
        typeWritePlayer.volume = sfxVolume;
    }
}

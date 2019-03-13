using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    public AudioSource efxSource;
    public AudioSource musicSource;
    public AudioSource playerSource;
    public static SoundManager instance = null;

    public float lowPitchRange = .95f;
    public float highPitchRange = 1.05f;

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);

        float soundVolume, musicVolume;

        soundVolume = PlayerPrefs.HasKey("Sound") ? PlayerPrefs.GetFloat("Sound") : 0.5f;
        musicVolume = PlayerPrefs.HasKey("Music") ? PlayerPrefs.GetFloat("Music") : 0.5f;

        efxSource.volume = soundVolume;
        playerSource.volume = soundVolume;
        musicSource.volume = musicVolume;
    }

    public void PlaySingle (AudioClip clip)
    {
        efxSource.clip = clip;
        efxSource.Play();
    }

    public void RandomizeSfx (params AudioClip [] clips)
    {
        int randomIndex = Random.Range(0, clips.Length);
        float randomPitch = Random.Range(lowPitchRange, highPitchRange);

        efxSource.pitch = randomPitch;
        efxSource.clip = clips[randomIndex];
        efxSource.Play();
    }

    public void RandomizePlayerSfx(params AudioClip[] clips)
    {
        int randomIndex = Random.Range(0, clips.Length);
        float randomPitch = Random.Range(lowPitchRange, highPitchRange);

        playerSource.pitch = randomPitch;
        playerSource.clip = clips[randomIndex];
        playerSource.Play();
    }

    public void ChangeMusicVolume(float newValue)
    {
        musicSource.volume = newValue;
        PlayerPrefs.SetFloat("Music", newValue);
    }

    public void ChangeSoundVolume(float newValue)
    {
        efxSource.volume = newValue;
        playerSource.volume = newValue;

        PlayerPrefs.SetFloat("Sound", newValue);
    }

    public void ChangeSoundVolumeWithMultiplier (float multiplier)
    {
        float soundVol = PlayerPrefs.HasKey("Sound") ? PlayerPrefs.GetFloat("Sound") : 0.5f;

        efxSource.volume = soundVol * multiplier;
    }
}

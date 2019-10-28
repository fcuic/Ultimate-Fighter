using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    #region Static Instance
    private static AudioManager instance;
    public static AudioManager Instance 
    {
        get 
        {
            if (instance == null) 
            {
                instance = FindObjectOfType<AudioManager>();
                if (instance == null) 
                {
                    instance=new GameObject("Spawned Audio Manager", typeof(AudioManager)).GetComponent<AudioManager>();
                }
            }
            return instance;
        }
        private set 
        {
            instance = value;

        }
    }
    #endregion

    #region Fields
    private AudioSource MusicSource;
    private AudioSource MusicSource2;
    private AudioSource sfxSource;

    private bool firstMusicSourceIsPlaying;

    #endregion

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);

        //create audio sources and save them as references
        MusicSource = this.gameObject.AddComponent<AudioSource>();
        MusicSource2 = this.gameObject.AddComponent<AudioSource>();
        sfxSource = this.gameObject.AddComponent<AudioSource>();

        //LOOPING MUSIC TRACKS

        MusicSource.loop = true;
        MusicSource2.loop = true;
    }

    public void PlayMusic(AudioClip musicClip) 
    {
        AudioSource activeSource = (firstMusicSourceIsPlaying) ? MusicSource : MusicSource2;

        MusicSource.clip = musicClip;
        activeSource.volume = 1; //MAX 1 MIN 0
        MusicSource.Play();

    }
    public void PlayMusicWithFade(AudioClip newClip, float transitionTime = 1.0f) 
    {
        AudioSource activeSource = (firstMusicSourceIsPlaying) ? MusicSource : MusicSource2;
        StartCoroutine(UpdateMusicWithFade(activeSource,newClip,transitionTime));
    }
    public void PlayMusicWithCrossFade(AudioClip musicClip, float transitionTime = 1.0f) 
    {
        //determining the active source
        AudioSource activeSource = (firstMusicSourceIsPlaying) ? MusicSource : MusicSource2;
        AudioSource newSource = (firstMusicSourceIsPlaying) ? MusicSource2 : MusicSource;

        //Swapping the source 
        firstMusicSourceIsPlaying = !firstMusicSourceIsPlaying;

        newSource.clip = musicClip;
        newSource.Play();
        StartCoroutine(UpdateMusicWithCrossFade(activeSource, newSource, transitionTime));

    }
    private IEnumerator UpdateMusicWithFade(AudioSource activeSource, AudioClip newClip, float transitionTime) 
    {
        //source is active and playing
        if (!activeSource.isPlaying)
            activeSource.Play();

        float t = 0.0f;

        //fading out Music

        for (t = 0; t < transitionTime; t+=Time.deltaTime) 
        {
            activeSource.volume = (1 - (t / transitionTime));
            yield return null;
        }
        activeSource.Stop();
        activeSource.clip = newClip;
        activeSource.Play();

        //fading in music
        for (t = 0; t < transitionTime; t += Time.deltaTime)
        {
            activeSource.volume = (t / transitionTime);
            yield return null;
        }
    }
    private IEnumerator UpdateMusicWithCrossFade(AudioSource original, AudioSource newSource, float TransitionTime) 
    {
        float t = 0.0f;
        for (t = 0.0f; t <= TransitionTime; t += Time.deltaTime) 
        {
            original.volume = (1 - (t / TransitionTime));
            newSource.volume = (t / TransitionTime);
            yield return null;
        }
    }
    public void PlaySFX(AudioClip clip) 
    {
        sfxSource.PlayOneShot(clip);

    }
    public void PlaySFX(AudioClip clip, float Volume) 
    {
        sfxSource.PlayOneShot(clip, Volume);
    }

    public void SetMusicVolume(float volume) 
    {
        MusicSource.volume = volume;
        MusicSource2.volume = volume;

    }
    public void SetSFXVolume(float volume) 
    {
        sfxSource.volume = volume;
    }
}

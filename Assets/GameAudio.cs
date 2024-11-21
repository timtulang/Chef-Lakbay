using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public AudioSource bgMusicSource;
    public AudioSource sfxSource;

    public AudioClip mainMenuMusic;
    public AudioClip stageMusic;

    public AudioClip popSound;
    public AudioClip fryingSound;
    public AudioClip choppingSound;

    private static AudioManager instance;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        PlayBackgroundMusic(mainMenuMusic);
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void PlayBackgroundMusic(AudioClip musicClip)
    {
        if (bgMusicSource.clip != musicClip)
        {
            bgMusicSource.Stop();
            bgMusicSource.clip = musicClip;
            bgMusicSource.loop = true;
            bgMusicSource.Play();
        }
    }
    public void StopBackgroundMusic(AudioClip musicClip)
    {
        bgMusicSource.clip = musicClip;
        bgMusicSource.Stop();
    }

    public void PlaySFX(AudioClip sfxClip)
    {
        sfxSource.clip = sfxClip;
        sfxSource.Play();
    }

    public void StopSFX()
    {
        if (sfxSource.isPlaying)
        {
            sfxSource.Stop();
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "MainMenu")
        {
            StopBackgroundMusic(stageMusic);
            PlayBackgroundMusic(mainMenuMusic);
        }
        else if (scene.name == "Stage(PLM)")
        {
            StopBackgroundMusic(mainMenuMusic);
            PlayBackgroundMusic(stageMusic);
        }
    }
}

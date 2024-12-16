using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;

public class MusicManager : MonoBehaviour
{
    public static MusicManager instance;

    private audioManager audioManager;

    public void Awake()
    {
        instance = this;
    }

    [SerializeField] AudioSource audioSource;
    [SerializeField] float timeToSwitch;

    [SerializeField] AudioClip playOnStart;

    private void Start()
    {
        Play(playOnStart, true);
        audioManager = FindObjectOfType<audioManager>();
    }

    public void Play(AudioClip musicToPlay, bool interrupt = false)
    {
        if (musicToPlay == null) { return;  }

        if (interrupt)
        {
            audioSource.volume = 1f;
            audioSource.clip = musicToPlay;
            audioSource.Play();

        }
        else
        {
            switchTo = musicToPlay;
            StartCoroutine(SmoothSwitchMusic());
        }
    }

    AudioClip switchTo;
    float volume;

    IEnumerator SmoothSwitchMusic() 
    {
        volume = 1f;
        while (volume > 0f)
        {
            volume -= Time.deltaTime / timeToSwitch;  // 0.01667초(1초를 60으로 나눈 값)
            if (volume < 0f) { volume = 0f; }
            audioSource.volume = volume;
            yield return new WaitForEndOfFrame();   
        }
        Play(switchTo, true);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    public GameObject soundNodePrefab;
    public AudioClip testAudio;

    [Header("For Background Music")]
    public bool SoundEnabled;
    public AudioSource musicSource;
    public AudioClip musicStart;

    void Awake()
    {
        if (instance != null)
        {
            Destroy(this.gameObject);
            return;
        }
        instance = this;
    }

    void Start()
    {
        musicSource.clip = musicStart;
        musicSource.loop = true;

        if (SoundEnabled)
        {
            musicSource.Play();
        }
        //musicSource.PlayOneShot(musicStart);
        //musicSource.PlayScheduled(AudioSettings.dspTime + musicStart.length);
        //musicSource.loop = true;
    }

    void Update()
    {
        if (SoundEnabled && !musicSource.isPlaying)
        {
            musicSource.Play();
        }
        else if (!SoundEnabled)
        {
            musicSource.Stop();
        }
    }

    public void playTestAudio() { playSound(testAudio); }

    private void playSound(AudioClip audioClip)
    {
        float audioClipLength = audioClip.length;

        GameObject soundNode = Instantiate(soundNodePrefab);

        AudioSource audioSource = soundNode.GetComponent<AudioSource>();

        audioSource.clip = audioClip;
        audioSource.Play();

        Destroy(soundNode, audioClipLength);
    }
}

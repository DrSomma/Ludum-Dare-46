using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    private bool isOpen = false;

    public Sprite IconMusicIsMuted;
    public Sprite IconMusicIsNotMuted;
    public Sprite IconSoundIsMuted;
    public Sprite IconSoundIsNotMuted;

    public Image btnMusic;
    public Image btnSound;

    void Start()
    {
        ShowMenu(false);
        if (!SoundManager.instance.MuteMusic)
        {
            btnMusic.sprite = IconMusicIsNotMuted;
        }
        else
        {
            btnMusic.sprite = IconMusicIsMuted;
        }

        if (!SoundManager.instance.MuteSounds)
        {
            btnSound.sprite = IconSoundIsNotMuted;
        }
        else
        {
            btnSound.sprite = IconSoundIsMuted;
        }
    }

    void Update()
    {
        if (!InfectionManager.Instance.isGameOver)
        {
            if (Input.GetKeyUp(KeyCode.Escape))
            {
                ShowMenu(!isOpen);
            }
        }
    }

    public void ShowMenu(bool status)
    {
        isOpen = status;
        Debug.Log("ShowPause");
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(status);
        }

        if (status)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    public void Restart()
    {
        Debug.Log("Restart");
        SceneSwap.Instance.TransitionToLevel(SceneSwap.GAME);
    }

    public void CloseGame()
    {
        Debug.Log("CloseGame");
        Application.Quit();
    }

    public void MainMenu()
    {
        Debug.Log("MainMenu");
        SceneSwap.Instance.TransitionToLevel(SceneSwap.MAIN_MENU);
    }

    public void UIMuteMusic()
    {
        Debug.Log("UIMuteMusic");
        bool audioStatus = !SoundManager.instance.MuteMusic;
        SoundManager.instance.muteMusic(audioStatus);
        if (!audioStatus)
        {
            btnMusic.sprite = IconMusicIsNotMuted;
        }
        else
        {
            btnMusic.sprite = IconMusicIsMuted;
        }
    }

    public void UIMuteSound()
    {
        Debug.Log("UIMuteSound");
        bool audioStatus = !SoundManager.instance.MuteSounds;
        SoundManager.instance.muteSounds(audioStatus);
        if (!audioStatus)
        {
            btnSound.sprite = IconSoundIsNotMuted;
        }
        else
        {
            btnSound.sprite = IconSoundIsMuted;
        }
    }
}

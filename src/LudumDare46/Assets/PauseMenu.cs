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

    void Start()
    {
        ShowMenu(false);
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
    }

    public void UIMuteSound()
    {
        Debug.Log("UIMuteSound");
    }
}

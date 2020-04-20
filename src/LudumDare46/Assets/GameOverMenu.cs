using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOverMenu : MonoBehaviour
{
    public TextMeshProUGUI score;

    private void Start()
    {   
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }
    }

    public void ShowMenu()
    {
        score.text = string.Format(score.text, ScoreManager.Instance.getScore());
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(true);
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
}

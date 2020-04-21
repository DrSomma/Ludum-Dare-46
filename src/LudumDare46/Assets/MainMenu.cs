using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public void StartGame()
    {
        Debug.Log("Restart");
        SceneSwap.Instance.TransitionToLevel(SceneSwap.GAME);
    }

    public void CloseGame()
    {
        Debug.Log("CloseGame");
        Application.Quit();
    }
}

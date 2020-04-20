using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneSwap : MonoBehaviour
{
    public Animator transition;
    public float transitionTime = 1f;

    public static SceneSwap Instance;

    //All Scenes 
    public static int GAME = 1;
    public static int MAIN_MENU = 0;

    private void Awake()
    {
        if (Instance != null)
            Destroy(this.gameObject);
        Instance = this;
    }

    public void TransitionToLevel(int levelIndex)
    {
        StartCoroutine(LoadLevel(levelIndex));
    }

    IEnumerator LoadLevel(int levelIndex)
    {
        Time.timeScale = 1f;

        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(levelIndex);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseScreenManager : MonoBehaviour
{
    [SerializeField] GameObject TitleScreen;
    [SerializeField] GameObject OptionsScreen;

    public void Resume()
    {
        TitleScreen.SetActive(false);
        Time.timeScale = 1.0f;
    }
    public void Restart()
    {
        SceneManager.LoadScene("");
    }
    public void Options()
    {
        TitleScreen.SetActive(false);
        OptionsScreen.SetActive(true);
    }
    public void Return()
    {
        if (GameObject.Find("OptionsScreen"))
        {
            OptionsScreen.SetActive(false);
        }
        TitleScreen.SetActive(true);
    }
    public void PauseGame()
    {
        Time.timeScale = 0.0f;
        TitleScreen.SetActive(false);
    }
    public void EndGame()
    {
        SceneManager.LoadScene("");
    }
}

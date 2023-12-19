using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseScreenManager : MonoBehaviour
{
    [SerializeField] GameObject Menu;
    [SerializeField] GameObject TitleScreen;
    [SerializeField] GameObject OptionsScreen;

    private void OnEnable()
    {
        PauseGame();
    }
    public void Resume()
    {
        Menu.SetActive(false);
        Time.timeScale = 1.0f;
    }
    public void Restart()
    {
        SceneManager.LoadScene(1);
        Time.timeScale = 1.0f;
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
        TitleScreen.SetActive(true);
    }
    public void EndGame()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1.0f;
    }
}

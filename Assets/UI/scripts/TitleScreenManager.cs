using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreenManager : MonoBehaviour
{
    [SerializeField] GameObject TitleScreen;
    [SerializeField] GameObject OptionsScreen;
    [SerializeField] GameObject CreditsScreen;

    public void Play()
    {
        SceneManager.LoadScene(1);
    }
    public void Options()
    {
        TitleScreen.SetActive(false);
        OptionsScreen.SetActive(true);
    }
    public void Credits()
    {
        TitleScreen.SetActive(false);
        CreditsScreen.SetActive(true);
    }
    public void Return()
    {
        if (GameObject.Find("OptionsScreen"))
        {
            OptionsScreen.SetActive(false);
        }
        else if (GameObject.Find("CreditsScreen"))
        {
            CreditsScreen.SetActive(false);
        }
        TitleScreen.SetActive(true);
    }
    public void QuitGame()
    {
       Application.Quit();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathScreenManager : MonoBehaviour
{
    [SerializeField] GameObject TitleScreen;
    
    public void Restart()
    {
        SceneManager.LoadScene("");
    }
    public void EndGame()
    {
        SceneManager.LoadScene("");
    }
}

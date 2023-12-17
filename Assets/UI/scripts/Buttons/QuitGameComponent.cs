using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitGameComponent : MonoBehaviour
{
    public void QuitGame()
    {
        if (UnityEditor.EditorApplication.isPlaying)
            UnityEditor.EditorApplication.isPlaying = false;
        else
            Application.Quit();
    }
}

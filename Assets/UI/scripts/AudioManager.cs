using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // video : https://www.youtube.com/watch?v=pbuJUaO-wpY (нн~8:50)
    // How to save the audio settings changed in the menu 
    // to the actual game scene.
    public static AudioManager instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}

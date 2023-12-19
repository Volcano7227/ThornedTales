using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseInput : MonoBehaviour
{
    [SerializeField] GameObject PauseMenu;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && !PauseMenu.activeInHierarchy)
            PauseMenu.SetActive(true);
    }
}

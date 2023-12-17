using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnButtonComponent : MonoBehaviour
{
    [SerializeField] GameObject TitleScreen;

    public void Return()
    {
        this.transform.parent.gameObject.SetActive(false);
        TitleScreen.SetActive(true);
    }
}

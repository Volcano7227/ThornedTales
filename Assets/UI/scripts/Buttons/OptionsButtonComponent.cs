using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsButtonComponent : MonoBehaviour
{
    [SerializeField] GameObject OptionsScreen;

    public void Options()
    {
        this.transform.parent.gameObject.SetActive(false);
        OptionsScreen.SetActive(true);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsButtonComponent : MonoBehaviour
{
    [SerializeField] GameObject CreditsScreen;

    public void Credits()
    {
        this.transform.parent.gameObject.SetActive(false);
        CreditsScreen.SetActive(true);
    }
}

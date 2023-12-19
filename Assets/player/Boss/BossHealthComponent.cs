using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealthComponent : MonoBehaviour
{
    public Slider slider;


    public void SetHealth(int health)
    {
        slider.value = health;
    }
    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;
    }

    public void Show(int nbPointVie)
    {

    }
    public void TakeHit()
    {

    }
}

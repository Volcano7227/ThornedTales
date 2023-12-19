using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealthComponent : MonoBehaviour
{
    [SerializeField] Slider healthSlider;
    
    public void SetHealth(int health)
    {
        healthSlider.value = health;
    }
    public void SetMaxHealth(int health)
    {
        healthSlider.maxValue = health;
        healthSlider.value = health;
    }
    [ContextMenu("Show")]
    public void Show() => gameObject.SetActive(true);

    [ContextMenu("Hide")]
    public void Hide() => gameObject.SetActive(false);
}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public int maxHp;
    public int currentHp;
    public BossHealthComponent health;

    void Start()
    {
        currentHp = maxHp;
        health.SetMaxHealth(maxHp);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        currentHp--;
        health.SetHealth(currentHp);
    }
}

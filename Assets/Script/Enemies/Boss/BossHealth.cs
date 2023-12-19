using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : BaseEnemyBehavior
{
    BossHealthComponent bossHealth;
    protected override void Awake()
    {
        base.Awake();
        bossHealth = FindObjectOfType<BossHealthComponent>();
    }

    protected override void manageDeath()
    {
        ParentRoom.ClearRoom();

        gameObject.SetActive(false);
    }
    public override void inflictDamage(int damage)
    {
        base.inflictDamage(damage);
        bossHealth.SetHealth(Hp);
    }
}

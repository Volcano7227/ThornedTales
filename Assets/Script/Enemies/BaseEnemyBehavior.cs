using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BaseEnemyBehavior : MonoBehaviour
{
    [SerializeField] int hp;
    
    GameObject player;
    public int Hp => hp;
    public Room ParentRoom { get; private set; }

    SpriteRenderer spriteRenderer;

    private bool isDead;

    protected virtual void  Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }
    private void OnEnable()
    {
        ParentRoom = GetComponentInParent<Room>();
    }
    void Update()
    {
        if (isDead)
            manageDeath();
        if (player.transform.position.x - transform.position.x < 0)
            spriteRenderer.flipX = false;
        else
            spriteRenderer.flipX = true;
    }

    protected virtual void manageDeath()
    {
        ParentRoom.Difficulty--;
        if (ParentRoom.Difficulty == -1)
            ParentRoom.ClearRoom();
        gameObject.SetActive(false);
    }

    public virtual void inflictDamage(int damage)
    {
        hp -= damage;
        if (Hp <= 0)
            isDead = true;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PlayerBullet")
            inflictDamage(1);
    }
}

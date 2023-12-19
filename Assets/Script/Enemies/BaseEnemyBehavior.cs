using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BaseEnemyBehavior : MonoBehaviour
{
    [SerializeField] int Hp;
    
    GameObject player;

    public Room ParentRoom { get; private set; }

    SpriteRenderer spriteRenderer;

    private bool isDead;

    void Awake()
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

    private void manageDeath()
    {
        ParentRoom.Difficulty--;
        if (ParentRoom.Difficulty == -1)
            ParentRoom.ClearRoom();
        gameObject.SetActive(false);
    }

    public void inflictDamage(int damage)
    {
        Hp -= damage;
        if (Hp <= 0)
            isDead = true;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PlayerBullet")
            inflictDamage(1);
    }
}

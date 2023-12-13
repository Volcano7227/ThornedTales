using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BaseEnemyBehavior : MonoBehaviour
{
    [SerializeField] int Hp;
    
    GameObject player;

    SpriteRenderer spriteRenderer;

    private bool isDead;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
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
        print("An ennemy died");
        gameObject.SetActive(false);
    }

    public void inflictDamage(int damage)
    {
        Hp = Hp - damage;
        if (Hp <= 0)
            isDead = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PlayerBullet")
            inflictDamage(1);
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            print("Player hit");//player.inflictDamage(1);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "PlayerBullet")
            inflictDamage(1);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            print("Player hit");//player.inflictDamage(1);
        }
    }
}

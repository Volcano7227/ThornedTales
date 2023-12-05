using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BaseEnemyBehavior : MonoBehaviour
{
    [SerializeField] int Hp;
    
    NavMeshAgent agent;
    GameObject player;

    private bool isDead;


    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        agent = GetComponent<NavMeshAgent>();
        agent.destination = player.transform.position;
    }

    void Update()
    {
        agent.destination = player.transform.position;
        if (isDead)
            manageDeath();
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "PlayerBullet")
            inflictDamage(1);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
            print("Player hit");//player.inflictDamage(1);
    }
}

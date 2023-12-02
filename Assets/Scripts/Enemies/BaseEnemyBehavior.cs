using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BaseEnemyBehavior : MonoBehaviour
{
    [SerializeField] Transform destination;
    NavMeshAgent agent;

    private int hp;
    private bool isDead;

    public int Hp
    {
        get { return hp; }
        private set
        {
            if (value < 0)
                hp = value;
            else
                hp = 1;
        }
    }



    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.destination = destination.position;
    }

    void Update()
    {
        agent.destination = destination.position;
    }

    private void OnTriggerEnter(Collider collider)
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        print("hit");
        if (collision.gameObject.tag == "PlayerBullet")
        {
            Hp--;
            if (Hp < 0)
                isDead = true;
        }
    }
}

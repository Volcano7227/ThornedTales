using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossBehavior : MonoBehaviour
{
    [SerializeField] float chaseCooldown = 2;
    [SerializeField] float shootCooldown = 2;
    [SerializeField] float detectionRange = 3;
    GameObject player;
    Transform agent;
    Node root;
    // Start is called before the first frame update
    void Awake()
    {
        agent = GetComponent<Transform>();
        player = GameObject.FindGameObjectWithTag("Player");
        SetupTree();
    }

    private void SetupTree()
    {
        root = new Selector(new List<Node>()
        {
            //ChargeTree(),
            //new PatrolTask(destinations, waitTime, agent)
            new Chase(player.transform, chaseCooldown, agent),
            new Shoot(player.transform, shootCooldown)
        });
    }

    private Node ChargeTree()
    {
        List<Node> list = new List<Node>();
        list.Add(new IsWithinRange(player.transform, transform, detectionRange));

        return new Sequence(new List<Node>()
        {
            new XOR(list),
            //new GoToTarget(agent),
            new Steal()
        });
    }

    // Update is called once per frame
    void Update()
    {
        root.Evaluate();
    }
}

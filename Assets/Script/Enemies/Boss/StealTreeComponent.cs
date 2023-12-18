using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public class StealTreeComponent : MonoBehaviour
{
    [SerializeField] Transform[] targets;
    [SerializeField] Transform[] destinations;
    [SerializeField] float waitTime = 2;
    [SerializeField] float detectionRange = 3;
    NavMeshAgent agent;
    Node root;
    // Start is called before the first frame update
    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        SetupTree();
    }

    private void SetupTree()
    {
        root = new Selector(new List<Node>()
        {
            StealTree(),
            new PatrolTask(destinations, waitTime, agent)
        });
    }

    private Node StealTree()
    {
        List<Node> list = new List<Node>();
        foreach (Transform t in targets)
        {
            list.Add(new IsWithinRange(t, transform, detectionRange));
        }

        return new Sequence(new List<Node>()
        {
            new XOR(list),
            new GoToTarget(agent),
            new Steal()
        });
    }

    // Update is called once per frame
    void Update()
    {
        root.Evaluate();
    }
}

using UnityEngine;
using UnityEngine.AI;

public class FollowComponent : MonoBehaviour
{
    [SerializeField] Transform destination;
    NavMeshAgent agent;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.destination = destination.position;
    }

    void Update()
    {
        agent.destination = destination.position;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Chase : Node
{
    Transform target;
    Transform agent;
    float cooldown;
    float elapsedTime = 0;
    bool isWaiting = false;

    public Chase(Transform target, float cooldown, Transform agent)
    {
        this.target = target;
        this.cooldown = cooldown;
        this.agent = agent;
    }

    public override NodeState Evaluate()
    {
        if (isWaiting)
        {
            elapsedTime += Time.deltaTime;
            if (elapsedTime >= cooldown)
            {
                isWaiting = false;
            }
            state = NodeState.Failure;
            Debug.Log($"Chase {target.gameObject.name} Failure");
        }
        else
        {
            isWaiting = true;
            elapsedTime = 0;
            /*if (Vector3.Distance(agent.transform.position, target.position) < agent.stoppingDistance)
            {
            }
            else
            {
                agent.destination = target.position;
            }*/
            state = NodeState.Success;
            Debug.Log($"Chase {target.gameObject.name} Success");
        }
        return state;
    }
}

public class Shoot : Node
{
    Transform target;
    NavMeshAgent agent;
    float cooldown;
    float elapsedTime = 0;
    bool isWaiting = false;

    public Shoot(Transform target, float cooldown)
    {
        this.target = target;
        this.cooldown = cooldown;
    }

    public override NodeState Evaluate()
    {
        Debug.Log($"Shoot");
        state = NodeState.Running;
        return state;
    }
}
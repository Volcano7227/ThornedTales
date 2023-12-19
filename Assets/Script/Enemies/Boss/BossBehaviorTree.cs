using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Pool;

public class IsCooldownOver : Node
{
    float cooldown;
    float elapsedTime = 0;

    public IsCooldownOver(float cooldown)
    {
        this.cooldown = cooldown;
    }

    public override NodeState Evaluate()
    {
        Debug.Log($"CooldownRunning");
        state = NodeState.Running;
        elapsedTime += Time.deltaTime;
        if (elapsedTime >= cooldown)
        {
            Debug.Log($"CooldownOver");
            state = NodeState.Success;
            elapsedTime = 0;
        }
        return state;
    }
}
public class Charge : Node
{
    Transform target;
    Transform agent;
    bool isMovementDone = true;

    public Charge(Transform target, Transform agent)
    {
        this.target = target;
        this.agent = agent;
    }

    public override NodeState Evaluate()
    {
        /*if (isWaiting)
        {
            elapsedTime += Time.deltaTime;
            if (elapsedTime >= cooldown)
            {
                isWaiting = false;
            }
            state = NodeState.Failure;
            Debug.Log($"Charge {target.gameObject.name} Failure");
        }
        else
        {
            isWaiting = true;
            elapsedTime = 0;*/
            /*if (Vector3.Distance(agent.transform.position, target.position) < agent.stoppingDistance)
            {
            }
            else
            {
                agent.destination = target.position;
            }*/
            if (isMovementDone)
            {
                state = NodeState.Success;
                Debug.Log($"Charge {target.gameObject.name} Success");
            }
            else
            {
                state = NodeState.Running;
                Debug.Log($"Charge {target.gameObject.name} Running");
            }
        //}
        return state;
    }
}

public class Shoot : Node
{
    Transform target;
    Transform self;
    GameObject objectToSpawn;
    ObjectPool objectPoolScript;

    public Shoot(Transform target, Transform self, GameObject objectToSpawn, string poolName)
    {
        this.target = target;
        this.self = self;
        this.objectToSpawn = objectToSpawn;
        objectPoolScript = GameObject.FindGameObjectWithTag(poolName).GetComponent<ObjectPool>();
    }

    public override NodeState Evaluate()
    {
        Debug.Log($"Shoot");
        Fire();
        return state;
    }

    /// <summary>
    /// Shoot a bullet
    /// </summary>
    void Fire()
    {
        GameObject obj = objectPoolScript.objectPoolInstance.GetPooledObject(objectToSpawn);
        if (obj != null)
        {
            //Vector3 distance =  + (self.position - target.position).normalized;
            //distance.z = -0.01f;
            obj.transform.position = self.position;
            obj.SetActive(true);
            state = NodeState.Success;
        }
        else
        {
            Debug.Log("No bullet found.");
            state = NodeState.Failure;
        }
    }
}
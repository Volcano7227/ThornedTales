using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
        elapsedTime += Time.deltaTime;
        bool isCharging = (bool)GetData("isCharging");

        if (!isCharging)
        {
            state = NodeState.Running;
            if (elapsedTime >= cooldown)
            {
                Debug.Log($"CooldownOver");
                state = NodeState.Success;
                elapsedTime = 0;
            }
        }
        else
        {
            state = NodeState.Success;
        }
        return state;
    }
}
public class Charge : Node
{
    Transform target;
    GameObject self;
    Rigidbody2D rb;
    bool isMovementDone = false;
    Vector3 leftStoppingPoint;
    Vector3 rightStoppingPoint;
    float speed;

    public Charge(Transform target, GameObject self, float speed)
    {
        this.target = target;
        this.self = self;
        this.speed = speed;
        rb = self.GetComponent<Rigidbody2D>();
        leftStoppingPoint = Camera.main.ScreenToWorldPoint(new Vector2(-137, 50));
        rightStoppingPoint = Camera.main.ScreenToWorldPoint(new Vector2(15, 0));
        Debug.Log("Left: " + leftStoppingPoint + " Right: " + rightStoppingPoint);
    }

    public override NodeState Evaluate()
    {
        parent.SetData("isCharging", true);
        if (Camera.main.transform.position.x - self.transform.position.x > 0)
        {
            Debug.Log("left");
            rb.velocity = (rightStoppingPoint - self.transform.position).normalized * speed;
            //rb.MovePosition(rightStoppingPoint);
        }
        else
        {
            Debug.Log("right");
            rb.velocity = (leftStoppingPoint - self.transform.position).normalized * speed;
            //rb.MovePosition(leftStoppingPoint);
        }
        state = NodeState.Running;
        Debug.Log($"Charge {target.gameObject.name} Running");
        Debug.Log(Vector3.Distance(self.transform.position, leftStoppingPoint));
        Debug.Log(Vector3.Distance(self.transform.position, rightStoppingPoint));
        if (Vector2.Distance(self.transform.position, leftStoppingPoint) < 1 || Vector2.Distance(self.transform.position, rightStoppingPoint) < 1)
        {
            rb.velocity = Vector3.zero;
            parent.SetData("isCharging", false);
            state = NodeState.Success;
            Debug.Log($"Charge {target.gameObject.name} Success");
        }
        /*while (isMovementDone)
        {
            
            elapsedTime += Time.deltaTime;
            if (elapsedTime >= cooldown)
            {
                
                isMovementDone = true;
            }
        }*/
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
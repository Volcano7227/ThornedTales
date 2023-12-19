using UnityEngine;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

public class WaitCooldown : Node
{
    float cooldown;
    float elapsedTime = 0;

    public WaitCooldown(float cooldown)
    {
        this.cooldown = cooldown;
    }

    public override NodeState Evaluate()
    {
        bool isCharging = (bool)GetData("isCharging");
        if (!isCharging)
        {
            elapsedTime += Time.deltaTime;
            state = NodeState.Running;
            if (elapsedTime >= cooldown)
            {
                //Debug.Log($"CooldownOver");
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
    int nbCharge;
    bool firstLap = true;
    bool isLastPass = false;
    bool isStartingRight;
    Vector3 leftStoppingPoint;
    Vector3 rightStoppingPoint;
    Vector3 targetPosition;
    Vector3 currentTarget;
    float speed;
    int i = 0;

    public Charge(Transform target, GameObject self, float speed, BossRoom bossRoom, int nbCharge)
    {
        this.target = target;
        this.self = self;
        this.speed = speed;
        this.nbCharge = nbCharge;
        rb = self.GetComponent<Rigidbody2D>();
        leftStoppingPoint = bossRoom.LeftSide.position;
        rightStoppingPoint = bossRoom.RightSide.position;
    }

    public override NodeState Evaluate()
    {
        if (firstLap)
        {
            parent.SetData("isCharging", true);
            state = NodeState.Running;
            Vector3 direction;
            targetPosition = target.position;
            isStartingRight = Camera.main.transform.position.x - self.transform.position.x < 0;
            if (isStartingRight)
            {
                currentTarget = rightStoppingPoint;
                direction = new Vector3(1, 0, 0);
            }
            else
            {
                currentTarget = leftStoppingPoint;
                direction = new Vector3(-1, 0, 0);
            }

            direction.y = 0;
            rb.velocity = direction.normalized * speed;
            firstLap = false;
            i = 0;
        }
        else if (isLastPass)
        {
            currentTarget = targetPosition;
        }

        if (hasArrived())
        {
            if (isLastPass)
            {
                rb.velocity = Vector3.zero;
                parent.SetData("isCharging", false);
                state = NodeState.Success;
                isLastPass = false;
                firstLap = true;
            }
            else
            {
                if (currentTarget == rightStoppingPoint)
                    self.transform.position = new Vector3(leftStoppingPoint.x, target.position.y, self.transform.position.z);
                else
                    self.transform.position = new Vector3(rightStoppingPoint.x, target.position.y, self.transform.position.z);
            }
            if (nbCharge - 1 == i)
            {
                isLastPass = true;
            }
            i++;
        }
        return state;
    }

    private bool hasArrived()
    {
        return Mathf.Abs(self.transform.position.x - currentTarget.x) < 1;
    }
}

public class Shoot : Node
{
    Transform self;
    GameObject objectToSpawn;
    ObjectPool objectPoolScript;

    public Shoot(Transform self, GameObject objectToSpawn, string poolName)
    {
        this.self = self;
        this.objectToSpawn = objectToSpawn;
        objectPoolScript = GameObject.FindGameObjectWithTag(poolName).GetComponent<ObjectPool>();
    }

    public override NodeState Evaluate()
    {
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
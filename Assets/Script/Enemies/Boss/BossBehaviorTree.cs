using UnityEngine;

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
    Animator animator;
    bool isFirstLap = true;
    bool isLastLap = false;
    bool isStartingRight;
    Vector3 leftStoppingPoint;
    Vector3 rightStoppingPoint;
    Vector3 targetPosition;
    Vector3 currentTarget;
    float speed;
    int i = 0;

    public Charge(Transform target, GameObject self, float speed, BossRoom bossRoom, int nbCharge, Animator animator)
    {
        this.target = target;
        this.self = self;
        this.speed = speed;
        this.nbCharge = nbCharge;
        this.animator = animator;
        rb = self.GetComponent<Rigidbody2D>();
        leftStoppingPoint = bossRoom.LeftSide.position;
        rightStoppingPoint = bossRoom.RightSide.position;
    }

    public override NodeState Evaluate()
    {
        //State is running while the boss is charging
        state = NodeState.Running;

        //Initialise the charge if this is the first lap
        if (isFirstLap)
        {
            //Pause the cooldown
            parent.SetData("isCharging", true);

            //Start the charge animation
            animator.SetBool("isCharging", true);

            Vector3 direction;
            targetPosition = target.position;

            //Determine the charge direction
            isStartingRight = Camera.main.transform.position.x - self.transform.position.x < 0;
            if (isStartingRight)
            {
                currentTarget = rightStoppingPoint;
                direction = new Vector3(1, 0, 0);
                rb.SetRotation(270);
            }
            else
            {
                currentTarget = leftStoppingPoint;
                direction = new Vector3(-1, 0, 0);
                rb.SetRotation(90);
            }
            isFirstLap = false;
            rb.velocity = direction * speed;

            //Initialise the lap counter
            i = 0;
        }
        else if (isLastLap) //Set the target to the player if this is the last lap
            currentTarget = targetPosition;

        //If the boss has arrived at his current target
        if (hasArrived())
        {
            if (isLastLap)
            {
                rb.velocity = Vector3.zero;

                //State is success when the charge is over
                state = NodeState.Success;

                //Restart the cooldown
                parent.SetData("isCharging", false);

                //Stop the charge animation
                animator.SetBool("isCharging", false);

                //Reset the boss rotation
                rb.SetRotation(0);

                //Reset the laps bools
                isLastLap = false;
                isFirstLap = true;
            }
            else
            {
                //If not the last lap, teleport the boss to the other side
                if (currentTarget == rightStoppingPoint)
                    self.transform.position = new Vector3(leftStoppingPoint.x, target.position.y, self.transform.position.z);
                else
                    self.transform.position = new Vector3(rightStoppingPoint.x, target.position.y, self.transform.position.z);
            }

            //Indicate that this is the last lap
            if (nbCharge - 1 == i)
                isLastLap = true;
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
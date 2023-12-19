using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    //Movement
    [SerializeField] float speed = 200f;
    Rigidbody2D rb;
    Vector3 direction;
    Transform target;

    [SerializeField] float animationDelay = 5f;
    float currentAnimDelay;
    Animator animator;
    bool hasHit = false;

    GameObject player;

    private void Start()
    {
        SetTrajectory();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        currentAnimDelay = animationDelay;
    }

    /// <summary>
    /// Reset the bullet when enabled
    /// </summary>
    private void OnEnable()
    {
        animator = GetComponent<Animator>();
        hasHit = false;
        currentAnimDelay = animationDelay;
        SetTrajectory();
    }

    /// <summary>
    /// Calculate the direction towards the player
    /// </summary>
    private void SetTrajectory()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        direction = (target.position - transform.position).normalized;
    }

    void FixedUpdate()
    {
        //Move the bullet towards the direction
        if (!hasHit)
        {
            rb.velocity = direction * Time.deltaTime * speed;
            float angulo = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angulo, Vector3.forward);
        }
        else
            TriggerHitBehavior();
    }

    /// <summary>
    /// Play a hit sequence before deactivating the bullet
    /// </summary>
    private void TriggerHitBehavior()
    {
        rb.velocity = Vector3.zero;

        //Play hit animation
        if (currentAnimDelay >= 0)
        {
            animator.SetTrigger("hasHit");
            currentAnimDelay -= Time.deltaTime;
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// Manage the bullet's collision
    /// </summary>
    /// <param name="collider"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && player != null)
        {
            print("Player hit");//player.inflictDamage(1);
        }
        if (collision.gameObject.tag != "Boss")
            hasHit = true;
    }
}
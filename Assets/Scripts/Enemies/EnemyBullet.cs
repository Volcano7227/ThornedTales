using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    //Movement
    [SerializeField] float speed = 200f;
    Rigidbody rb;
    Vector3 direction;
    Transform target;

    //Hit sequence
    /*bool hasPlayedSound = false;
    AudioSource hitSound;
    float hitPlayDelay = 0.1f;
    ParticleSystem hitEffect;*/
    bool hasHit = false;

    GameObject player;

    private void Start()
    {
        SetTrajectory();
        rb = GetComponent<Rigidbody>();
        /*hitSound = GetComponent<AudioSource>();
        hitEffect = GetComponentInChildren<ParticleSystem>();*/
    }

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    /// <summary>
    /// Reset the bullet when enabled
    /// </summary>
    private void OnEnable()
    {
        //toggleBulletTransparency(true);
        //hasPlayedSound = false;
        hasHit = false;
        //hitPlayDelay = 0.1f;
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
            rb.velocity = direction * Time.deltaTime * speed;
        else
            TriggerHitBehavior();
    }

    /// <summary>
    /// Play a hit sequence before deactivating the bullet
    /// </summary>
    private void TriggerHitBehavior()
    {
        //Play hit sound
        /*if (!hasPlayedSound)
        {
            hitSound.Play();
            toggleBulletTransparency(false);
            hasPlayedSound = true;
        }
        //Play hit animation
        if (hitPlayDelay >= 0)
        {
            hitEffect.Play();
            hitPlayDelay -= Time.deltaTime;
        }
        else*/
            gameObject.SetActive(false);
    }

    /// <summary>
    /// Toggle the bullet's visibility
    /// </summary>
    /// <param name="IsActive"></param>
    /*private void toggleBulletTransparency(bool IsActive)
    {
        CapsuleCollider vesselHitBox = GetComponent<CapsuleCollider>();
        vesselHitBox.enabled = IsActive;
        MeshRenderer[] vesselParts =- GetComponentsInChildren<MeshRenderer>();
        foreach (MeshRenderer mesh in vesselParts)
            mesh.enabled = IsActive;
    }*/

    /// <summary>
    /// Manage the bullet's collision
    /// </summary>
    /// <param name="collider"></param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && player != null)
        {
            print("Player hit");//player.inflictDamage(1);
        }
        hasHit = true;
    }
}
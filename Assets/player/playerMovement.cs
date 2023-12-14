using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]

public class PlayerMovement : MonoBehaviour
{
    /*
     * --Warning--
     * Faire Attention si on fait des powerups or shit like that augment speed
     */
    [SerializeField] float DefaultSpeed;

    float speed;
    Rigidbody2D rb;
    Vector2 movementInput;
    public Animator animator;

    public GameObject projectile;
    public float bulletSpeed;
    float lastFire;
    public float fireDelay;

    // Start is called before the first frame update
    void Awake()
    {
        speed = DefaultSpeed;
        rb = GetComponent<Rigidbody2D>();
    }
    // Update is called once per frame
    void Update()
    {
        // video: https://www.youtube.com/watch?v=whzomFgjT50
        animator.SetFloat("Horizontal", movementInput.x);
        animator.SetFloat("Vertical", movementInput.y);

        //video reference: https://www.youtube.com/watch?v=EWo3tAG-iAg
        float shootHor = Input.GetAxis("Shoot_Horizontal");
        float shootVer = Input.GetAxis("Shoot_Vertical");
        if (shootHor != 0 && Time.time > lastFire + fireDelay)
        {
            Shoot(shootHor, 0);
            lastFire = Time.time;
        }
        else if (shootVer != 0 && Time.time > lastFire + fireDelay)
        {
            Shoot(0, shootVer);
            lastFire = Time.time;
        }

        animator.SetFloat("Speed", movementInput.sqrMagnitude);
    }
    private void FixedUpdate()
    {
        rb.velocity = movementInput * speed;
    }

    public void FreezeMovement() => speed = 0;
    public void UnFreezeMovement() => speed = DefaultSpeed;
    private void OnMove(InputValue inputValue)
    {
        movementInput = inputValue.Get<Vector2>();
    }

    void Shoot(float x, float y)
    {
        Vector3 direction = new Vector3(
            (x < 0) ? Mathf.Floor(x) : Mathf.Ceil(x),
            (y < 0) ? Mathf.Floor(y) : Mathf.Ceil(y),
            0
        );
        GameObject bullet = Instantiate(projectile, transform.position, 
            Quaternion.Euler(transform.rotation.x, transform.rotation.y, transform.rotation.z + 90));
        bullet.AddComponent<Rigidbody2D>().gravityScale = 0;
        Rigidbody2D bulletRB = bullet.GetComponent<Rigidbody2D>();
        bulletRB.velocity = direction * bulletSpeed;
        float angulo = Mathf.Atan2(bulletRB.velocity.y, bulletRB.velocity.x) * Mathf.Rad2Deg;
        bullet.transform.rotation = Quaternion.AngleAxis(angulo, Vector3.forward);
    }
}

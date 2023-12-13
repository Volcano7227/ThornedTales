using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float lifeTime;
    public Animator animator;
    public Rigidbody2D rb;
    public BoxCollider2D coll;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DeathDelay());
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        rb.velocity = Vector3.zero;
        coll.enabled = false;
        animator.SetTrigger("Explode");
    }

    public void DestroyBullet()
    {
        Destroy(gameObject);
    }

    IEnumerator DeathDelay()
    {
        yield return new WaitForSeconds(lifeTime);
        Destroy(gameObject);
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
     [SerializeField]float lifeTime;
     public Animator animator;
     Rigidbody2D rb;

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
        animator.SetTrigger("Explode");
        GetComponent<Collider2D>().enabled = false;
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

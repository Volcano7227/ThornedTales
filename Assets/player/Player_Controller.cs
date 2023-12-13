using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class Player_Controller : MonoBehaviour
{
    [SerializeField] int HitPoints;

    public Animator animator;
    public BoxCollider2D coll;
    public Rigidbody2D rb;
    public PlayerMovement movement;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        movement = GetComponent<PlayerMovement>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        HitPoints -= 5;
        coll.enabled = false;
        if (HitPoints <= 0)
        {
            HitPoints = 0;
            animator.SetTrigger("Death");
            Debug.Log("Death");
        }
        else if (HitPoints > HitPoints - 5)
        {
            animator.SetTrigger("Hit");
            Debug.Log("Hit");
        }   
    }

    public void IFrames()
    {
        StartCoroutine(InvulnerabilityTime());
    }

    public void OnPlayerDeath()
    {
        rb.velocity = Vector3.zero;
        movement.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator InvulnerabilityTime()
    {
        yield return new WaitForSeconds(2);
        coll.enabled = true;
    }
}

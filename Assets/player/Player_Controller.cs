using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class Player_Controller : MonoBehaviour
{
    [SerializeField] public int HitCount;

    public Animator animator;
    public BoxCollider2D coll;
    public Rigidbody2D rb;
    public PlayerMovement movement;
    public Heart_Spawner hearts;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        movement = GetComponent<PlayerMovement>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        /*HitCount -= 1;
        hearts.PopHeart();
        coll.enabled = false;
        if (HitCount <= 0)
        {
            HitCount = 0;
            animator.SetTrigger("Death");
        }
        else if (HitCount > 0)
        {
            animator.SetTrigger("Hit");
        }*/ 
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

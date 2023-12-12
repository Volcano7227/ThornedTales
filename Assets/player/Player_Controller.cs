using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controller : MonoBehaviour
{
    public Animator animator;
    public BoxCollider2D coll;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        coll.enabled = false;
        animator.SetTrigger("Hit");
        Debug.Log("Hit");
    }

    public void IFrames ()
    {
        coll.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

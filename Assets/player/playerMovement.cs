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
}

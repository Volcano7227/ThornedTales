using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

[RequireComponent(typeof(Rigidbody2D))]

public class PlayerMovement : MonoBehaviour
{
    /*
     * --Warning--
     * Faire Attention si on fait des powerups or shit like that augment speed
     */
    [SerializeField] float defaultSpeed = 3f;
    [SerializeField]ContactFilter2D contactFilter;
    float speed;
    Rigidbody2D rb;
    Vector2 movementInput;
    List<RaycastHit2D> castCollision = new();
    public Animator animator;

    // Start is called before the first frame update
    void Awake()
    {
        speed = defaultSpeed;
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
        if (!MovePlayer(movementInput))
            if (!MovePlayer(new Vector2(movementInput.x, 0)))
                MovePlayer(new Vector2(0, movementInput.y)); 

    }

    public void FreezeMovement() => speed = 0;
    public void UnFreezeMovement() => speed = defaultSpeed;
    private void OnMove(InputValue inputValue)
    {
        movementInput = inputValue.Get<Vector2>();
    }
    bool MovePlayer(Vector2 inputMovement)
    {
        Vector2 Direction = inputMovement;
        int count = rb.Cast(Direction, contactFilter, castCollision, 0.1f);

        if (count == 0)
        {
            rb.velocity = inputMovement * speed;
            return true;
        }
        rb.velocity = Vector2.zero;
        return false;
    }
}
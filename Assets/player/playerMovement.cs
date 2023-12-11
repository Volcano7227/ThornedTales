using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] List<Sprite> sprites;

    Rigidbody2D rb;
    SpriteRenderer rd;
    Vector2 movementInput;

    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rd = GetComponent<SpriteRenderer>();
        rd.sprite = sprites[0]; //starts idle
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
            rd.sprite = sprites[1];                     // up
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
            rd.sprite = sprites[2];                     // left
        else if (Input.GetKeyDown(KeyCode.RightArrow))
            rd.sprite = sprites[3];                     // right
        else if (Input.GetKeyDown(KeyCode.DownArrow))
            rd.sprite = sprites[4];                     // down
        else 
            rd.sprite = sprites[0];                     // idle
    }
    private void FixedUpdate()
    {
        rb.velocity = movementInput;
    }
    private void OnMove(InputValue inputValue)
    {
        movementInput = inputValue.Get<Vector2>();
    }
}

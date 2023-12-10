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
    [SerializeField] List<Sprite> sprites;

    float speed;
    Rigidbody2D rb;
    SpriteRenderer rd;
    Vector2 movementInput;

    // Start is called before the first frame update
    void Awake()
    {
        speed = DefaultSpeed;
        rb = GetComponent<Rigidbody2D>();
        rd = GetComponent<SpriteRenderer>();
        rd.sprite = sprites[0]; //starts down
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
            rd.sprite = sprites[1]; 
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
            rd.sprite = sprites[2];
        else if (Input.GetKeyDown(KeyCode.RightArrow))
            rd.sprite = sprites[3];
        else if (Input.GetKeyDown(KeyCode.DownArrow))
            rd.sprite = sprites[0];
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

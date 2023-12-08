using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

[RequireComponent(typeof(Rigidbody2D))]

public class playerMovement : MonoBehaviour
{
    [SerializeField] public float speed;
    [SerializeField] public List<Material> mats;

    PlayerControls controls;
    Rigidbody2D rb;
    MeshRenderer rd;
    Vector2 moveInput;
    InputBinding[] inputs;

    // Start is called before the first frame update
    void Awake()
    {
        controls = new PlayerControls();
        rb = GetComponent<Rigidbody2D>();
        rd = GetComponent<MeshRenderer>();
        rd.material = mats[0]; //starts down
    }

    private void OnEnable()
    {
       controls.Player_map.Enable();
    }
    private void OnDisable()
    {
        controls.Player_map.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        moveInput = controls.Player_map.movement.ReadValue<Vector2>();
        rb.velocity = moveInput * speed;

        if (Keyboard.current.upArrowKey.IsPressed())
            rd.material = mats[1]; 
        else if (Keyboard.current.leftArrowKey.IsPressed())
            rd.material = mats[2];
        else if (Keyboard.current.rightArrowKey.IsPressed())
            rd.material = mats[3];
        else if (Keyboard.current.downArrowKey.IsPressed())
            rd.material = mats[0];
    }
}

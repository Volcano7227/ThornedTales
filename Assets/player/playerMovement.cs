using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] public float speed;
    [SerializeField] public List<Material> mats;

    Rigidbody2D rb;
    MeshRenderer rd;
    Vector2 moveInput;
    InputBinding[] inputs;

    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rd = GetComponent<MeshRenderer>();
        rd.material = mats[0]; //starts down
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
            rd.material = mats[1]; 
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
            rd.material = mats[2];
        else if (Input.GetKeyDown(KeyCode.RightArrow))
            rd.material = mats[3];
        else if (Input.GetKeyDown(KeyCode.DownArrow))
            rd.material = mats[0];
    }
    private void FixedUpdate()
    {
        rb.velocity = new Vector2(1,0.5f);
    }
    private void OnMove(InputValue inputValue)
    {
        Debug.Log("Moving");
    }
}

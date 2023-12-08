using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] List<Material> mats;

    Rigidbody2D rb;
    //MeshRenderer rd;
    Vector2 movementInput;

    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        //rd = GetComponent<MeshRenderer>();
        //rd.material = mats[0]; //starts down
    }
    // Update is called once per frame
    void Update()
    {
       /* if (Input.GetKeyDown(KeyCode.UpArrow))
            rd.material = mats[1]; 
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
            rd.material = mats[2];
        else if (Input.GetKeyDown(KeyCode.RightArrow))
            rd.material = mats[3];
        else if (Input.GetKeyDown(KeyCode.DownArrow))
            rd.material = mats[0];*/
    }
    private void FixedUpdate()
    {
        rb.velocity = movementInput;
    }
    public void OnMove()
    {
        //movementInput = inputValue.Get<Vector2>();
        Debug.Log("Moving");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JhonMovi : MonoBehaviour
{
    private Rigidbody2D Rigidbody2D;
    private float horizontal;

    public float speed;
    public float jumpForce;

    private bool grounded;

    void Start()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");

        Debug.DrawRay(transform.position, Vector3.down * 0.1f, Color.red);
        if (Physics2D.Raycast(transform.position, Vector3.down, 0.1f))
        {
            grounded = true;
        }
        else
        {
            grounded = false;
        }

        if (Input.GetKeyDown(KeyCode.W) && grounded)
        {
            Jump();
        }
    }
    
    private void Jump()
    {
        Rigidbody2D.AddForce(Vector2.up * jumpForce);
    }

    private void FixedUpdate()
    {
        Rigidbody2D.linearVelocity = new Vector2(horizontal, Rigidbody2D.linearVelocity.y);
    }
}

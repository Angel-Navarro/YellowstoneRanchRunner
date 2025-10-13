using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JhonMovi : MonoBehaviour
{
    private Rigidbody2D Rigidbody2D;
    private Animator animator; // ✅ Agregamos el Animator
    private float horizontal;  // ✅ Este será tu "move"
    private bool grounded;

    public float speed = 5f;
    public float jumpForce = 400f;

    void Start()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>(); // ✅ Asignamos el componente Animator
    }

    void Update()
    {
        // Capturamos movimiento horizontal (A/D o flechas)
        horizontal = Input.GetAxis("Horizontal");

        // ✅ Controlamos la animación
        animator.SetBool("SpeedBool", Mathf.Abs(horizontal) > 0.1f);

        //animator.SetFloat("SpeedBool", Mathf.Abs(horizontal));

        // ✅ Detectamos si está en el suelo
        Debug.DrawRay(transform.position, Vector3.down * 0.1f, Color.red);
        grounded = Physics2D.Raycast(transform.position, Vector3.down, 0.1f);

        // ✅ Salto
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
        // ✅ Movimiento horizontal con velocidad
        Rigidbody2D.linearVelocity = new Vector2(horizontal * speed, Rigidbody2D.linearVelocity.y);
    }
}









//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class JhonMovi : MonoBehaviour
//{
//    private Rigidbody2D Rigidbody2D;
//    private float horizontal;

//    public float speed;
//    public float jumpForce;

//    private bool grounded;

//    void Start()
//    {
//        Rigidbody2D = GetComponent<Rigidbody2D>();
//    }

//    void Update()
//    {
//        horizontal = Input.GetAxis("Horizontal");

//        //Para controlar la animacion de caminar
//        animator.SetFloat("SpeedBool", Mathf.Abs(move));

//        Debug.DrawRay(transform.position, Vector3.down * 0.1f, Color.red);
//        if (Physics2D.Raycast(transform.position, Vector3.down, 0.1f))
//        {
//            grounded = true;
//        }
//        else
//        {
//            grounded = false;
//        }

//        if (Input.GetKeyDown(KeyCode.W) && grounded)
//        {
//            Jump();
//        }
//    }

//    private void Jump()
//    {
//        Rigidbody2D.AddForce(Vector2.up * jumpForce);
//    }

//    private void FixedUpdate()
//    {
//        Rigidbody2D.linearVelocity = new Vector2(horizontal, Rigidbody2D.linearVelocity.y);
//    }
//}

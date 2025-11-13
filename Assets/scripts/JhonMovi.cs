using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JhonMovi : MonoBehaviour
{
    public GameObject BulletPrefab;
    public Transform firePoint;

    private Rigidbody2D Rigidbody2D;
    private Animator animator;
    private SpriteRenderer spriteRenderer; // ✅ NUEVO
    private float horizontal;
    private bool grounded;
    private int facingDirection = 1; // ✅ 1 = derecha, -1 = izquierda

    public float speed = 5f;
    public float jumpForce = 400f;
    private float LastShoot;

    void Start()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>(); // ✅ Obtener el sprite
    }

    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");

        // ✅ Voltear SOLO el sprite, NO el transform
        if (horizontal > 0)
        {
            spriteRenderer.flipX = false;
            facingDirection = 1;
        }
        else if (horizontal < 0)
        {
            spriteRenderer.flipX = true;
            facingDirection = -1;
        }

        animator.SetBool("SpeedBool", Mathf.Abs(horizontal) > 0.1f);

        Debug.DrawRay(transform.position, Vector3.down * 0.2f, Color.red);
        grounded = Physics2D.Raycast(transform.position, Vector3.down, 0.4f);

        if (Input.GetKeyDown(KeyCode.W) && grounded)
        {
            Jump();
        }

        if (Input.GetKeyDown(KeyCode.Space) && Time.time > LastShoot + 0.25f)
        {
            Shoot();
            LastShoot = Time.time;
        }
    }

    private void Jump()
    {
        Rigidbody2D.AddForce(Vector2.up * jumpForce);
    }

    private void FixedUpdate()
    {
        Rigidbody2D.linearVelocity = new Vector2(horizontal * speed, Rigidbody2D.linearVelocity.y);
    }

    private void Shoot()
    {
        if (BulletPrefab == null)
        {
            Debug.LogError("¡BulletPrefab no está asignado en el Inspector!");
            return;
        }

        // ✅ Usar facingDirection en vez de localScale
        Vector2 direction = facingDirection > 0 ? Vector2.right : Vector2.left;
        Vector3 spawnPosition = transform.position + (Vector3)direction * 0.5f;
        GameObject bullet = Instantiate(BulletPrefab, spawnPosition, Quaternion.identity);

        BulletScript bulletScript = bullet.GetComponent<BulletScript>();
        if (bulletScript != null)
        {
            bulletScript.SetDirection(direction);
        }
        else
        {
            Debug.LogError("¡El prefab de la bala no tiene el componente BulletScript!");
        }
    }
}







//2--------




//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class JhonMovi : MonoBehaviour
//{
//    public GameObject BulletPrefab;

//    private Rigidbody2D Rigidbody2D;
//    private Animator animator; // ✅ Agregamos el Animator
//    private float horizontal;  // ✅ Este será tu "move"
//    private bool grounded;

//    public float speed = 5f;
//    public float jumpForce = 400f;
//    private float LastShoot;

//    void Start()
//    {
//        Rigidbody2D = GetComponent<Rigidbody2D>();
//        animator = GetComponent<Animator>(); // ✅ Asignamos el componente Animator
//    }

//    void Update()
//    {
//        // Capturamos movimiento horizontal (A/D o flechas)
//        horizontal = Input.GetAxis("Horizontal");

//        // ✅ Controlamos la animación
//        animator.SetBool("SpeedBool", Mathf.Abs(horizontal) > 0.1f);

//        //animator.SetFloat("SpeedBool", Mathf.Abs(horizontal));

//        // ✅ Detectamos si está en el suelo
//        Debug.DrawRay(transform.position, Vector3.down * 0.2f, Color.red);
//        grounded = Physics2D.Raycast(transform.position, Vector3.down, 0.4f);

//        // ✅ Salto
//        if (Input.GetKeyDown(KeyCode.W) && grounded)
//        {
//            Jump();
//        }

//        // ✅ Test temporal
//        if (Input.GetKeyDown(KeyCode.W))
//        {
//            Debug.Log("Presionaste W - Grounded: " + grounded);
//        }

//        // ... resto de tu código ...

//        if (Input.GetKeyDown(KeyCode.Space) && Time.time > LastShoot + 0.25f)
//        {
//            Shoot();
//            LastShoot = Time.time;
//        }
//    }

//    private void Jump()
//    {
//        Rigidbody2D.AddForce(Vector2.up * jumpForce);
//    }

//    private void FixedUpdate()
//    {
//        // ✅ Movimiento horizontal con velocidad
//        Rigidbody2D.linearVelocity = new Vector2(horizontal * speed, Rigidbody2D.linearVelocity.y);
//    }

//    private void Shoot()
//    {
//        Vector3 direction;

//        if (transform.localScale.x > 0)
//        {
//            direction = Vector3.right;
//        }
//        else
//        {
//            direction = Vector3.left;
//        }

//        //Instantiate(BulletPrefab, transform.position, Quaternion.identity);
//        GameObject bullet = Instantiate(BulletPrefab, transform.position + direction * 0.1f, Quaternion.identity);
//        bullet.GetComponent<BulletScript>().SetDirection(Vector2.right);
//    }
//}










//1-------


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

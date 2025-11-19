using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public float speed = 2f;
    public float checkDistance = 0.1f;
    public LayerMask groundLayer;

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private int direction = 1;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(direction * speed, rb.linearVelocity.y);
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    void Update()
    {
        spriteRenderer.flipX = direction < 0;

        Vector2 frontCheck = new Vector2(
            transform.position.x + (direction * checkDistance),
            transform.position.y - 0.3f
        );

        RaycastHit2D groundCheck = Physics2D.Raycast(
            frontCheck,
            Vector2.down,
            0.6f,
            groundLayer
        );

        RaycastHit2D wallCheck = Physics2D.Raycast(
            transform.position,
            new Vector2(direction, 0),
            checkDistance,
            groundLayer
        );

        if (wallCheck.collider != null && !wallCheck.collider.CompareTag("Player"))
        {
            Flip();
        }
        else if (groundCheck.collider == null)
        {
            Flip();
        }

        Debug.DrawRay(frontCheck, Vector2.down * 0.6f, Color.yellow);
        Debug.DrawRay(transform.position, new Vector2(direction, 0) * checkDistance, Color.red);
    }

    void Flip()
    {
        direction *= -1;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("¡Oso golpeó al jugador!");

            PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.Die();
            }
            else
            {
                Debug.LogError("¡PlayerHealth no encontrado en el Player!");
            }
        }
    }
}




//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class EnemyPatrol : MonoBehaviour
//{
//    public float speed = 2f;
//    public float checkDistance = 0.1f; // Distancia para detectar obstáculos
//    public LayerMask groundLayer; // Capa del suelo

//    private Rigidbody2D rb;
//    private SpriteRenderer spriteRenderer;
//    private Animator animator;
//    private int direction = 1; // 1 = derecha, -1 = izquierda

//    void Start()
//    {
//        rb = GetComponent<Rigidbody2D>();
//        spriteRenderer = GetComponent<SpriteRenderer>();
//        animator = GetComponent<Animator>();
//    }

//    void FixedUpdate()
//    {
//        // Moverse en la dirección actual usando FixedUpdate para física
//        rb.linearVelocity = new Vector2(direction * speed, rb.linearVelocity.y);

//        // Mantener el oso derecho (evitar rotación)
//        transform.rotation = Quaternion.Euler(0, 0, 0);
//    }

//    void Update()
//    {
//        // Voltear el sprite según la dirección
//        spriteRenderer.flipX = direction < 0;

//        // Verificar si hay suelo adelante (para detectar precipicios)
//        Vector2 frontCheck = new Vector2(
//            transform.position.x + (direction * checkDistance),
//            transform.position.y - 0.3f // Bajamos un poco el punto de inicio
//        );

//        RaycastHit2D groundCheck = Physics2D.Raycast(
//            frontCheck,
//            Vector2.down,
//            0.6f,
//            groundLayer
//        );

//        // Verificar si hay pared/obstáculo adelante
//        RaycastHit2D wallCheck = Physics2D.Raycast(
//            transform.position,
//            new Vector2(direction, 0),
//            checkDistance,
//            groundLayer
//        );

//        // Si NO hay suelo adelante (precipicio) O hay una pared, voltear
//        //if (groundCheck.collider == null || wallCheck.collider != null)
//        //{
//        //    Flip();
//        //}

//        // Si hay un hit, verificar si no es el jugador
//        if (wallCheck.collider != null && !wallCheck.collider.CompareTag("Player"))
//        {
//            Flip();
//        }
//        else if (groundCheck.collider == null)
//        {
//            Flip();
//        }

//        // Debug visual (puedes verlo en Scene view)
//        Debug.DrawRay(frontCheck, Vector2.down * 0.6f, Color.yellow);
//        Debug.DrawRay(transform.position, new Vector2(direction, 0) * checkDistance, Color.red);
//    }

//    void Flip()
//    {
//        direction *= -1;
//    }

//    // Detectar colisión con el jugador
//    void OnCollisionEnter2D(Collision2D collision)
//    {
//        if (collision.gameObject.CompareTag("Player"))
//        {
//            PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
//            if (playerHealth != null)
//            {
//                playerHealth.Die();
//            }
//        }
//    }
//}















//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class EnemyPatrol : MonoBehaviour
//{
//    public float speed = 2f;
//    public float checkDistance = 0.5f; // Distancia para detectar obstáculos
//    public LayerMask groundLayer; // Capa del suelo

//    private Rigidbody2D rb;
//    private SpriteRenderer spriteRenderer;
//    private int direction = 1; // 1 = derecha, -1 = izquierda

//    void Start()
//    {
//        rb = GetComponent<Rigidbody2D>();
//        spriteRenderer = GetComponent<SpriteRenderer>();
//    }

//    void Update()
//    {
//        // Moverse en la dirección actual
//        rb.linearVelocity = new Vector2(direction * speed, rb.linearVelocity.y);

//        // Voltear el sprite según la dirección
//        spriteRenderer.flipX = direction < 0;

//        // Verificar si hay suelo adelante (para detectar precipicios)
//        Vector2 frontCheck = new Vector2(
//            transform.position.x + (direction * checkDistance),
//            transform.position.y
//        );

//        RaycastHit2D groundCheck = Physics2D.Raycast(
//            frontCheck,
//            Vector2.down,
//            0.6f,
//            groundLayer
//        );

//        // Verificar si hay pared/obstáculo adelante
//        RaycastHit2D wallCheck = Physics2D.Raycast(
//            transform.position,
//            new Vector2(direction, 0),
//            checkDistance,
//            groundLayer
//        );

//        // Si NO hay suelo adelante (precipicio) O hay una pared, voltear
//        if (groundCheck.collider == null || wallCheck.collider != null)
//        {
//            Flip();
//        }

//        // Debug visual (puedes verlo en Scene view)
//        Debug.DrawRay(frontCheck, Vector2.down * 0.6f, Color.yellow);
//        Debug.DrawRay(transform.position, new Vector2(direction, 0) * checkDistance, Color.red);
//    }

//    void Flip()
//    {
//        direction *= -1;
//    }

//    // Detectar colisión con el jugador
//    void OnCollisionEnter2D(Collision2D collision)
//    {
//        if (collision.gameObject.CompareTag("Player"))
//        {
//            PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
//            if (playerHealth != null)
//            {
//                playerHealth.Die();
//            }
//        }
//    }
//}

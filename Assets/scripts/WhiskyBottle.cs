using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhiskyBottle : MonoBehaviour
{
    [Header("Configuración")]
    public bool fullHeal = true;
    public int healAmount = 5;

    [Header("Efectos Visuales")]
    public GameObject pickupEffect;
    public float rotationSpeed = 50f;
    public float floatSpeed = 1f;
    public float floatAmount = 0.3f;

    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);

        float newY = startPosition.y + Mathf.Sin(Time.time * floatSpeed) * floatAmount;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();

            if (playerHealth != null)
            {
                // Restaurar vida
                if (fullHeal)
                {
                    playerHealth.HealFull();
                }
                else
                {
                    playerHealth.Heal(healAmount);
                }

                // Efecto de partículas
                if (pickupEffect != null)
                {
                    Instantiate(pickupEffect, transform.position, Quaternion.identity);
                }

                // Ocultar el sprite pero no destruir inmediatamente
                GetComponent<SpriteRenderer>().enabled = false;
                GetComponent<Collider2D>().enabled = false;

                // Destruir después de que el sonido termine
                Destroy(gameObject, 0.5f);
            }
        }
    }
}

//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class WhiskyBottle : MonoBehaviour
//{
//    [Header("Configuración")]
//    public bool fullHeal = true;
//    public int healAmount = 5;

//    [Header("Efectos Visuales")]
//    public GameObject pickupEffect;
//    public float rotationSpeed = 50f;
//    public float floatSpeed = 1f;
//    public float floatAmount = 0.3f;

//    private Vector3 startPosition;

//    void Start()
//    {
//        startPosition = transform.position;
//    }

//    void Update()
//    {
//        transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);

//        float newY = startPosition.y + Mathf.Sin(Time.time * floatSpeed) * floatAmount;
//        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
//    }

//    void OnTriggerEnter2D(Collider2D collision)
//    {
//        if (collision.CompareTag("Player"))
//        {
//            PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();

//            if (playerHealth != null)
//            {
//                // Restaurar vida
//                if (fullHeal)
//                {
//                    playerHealth.HealFull();
//                }
//                else
//                {
//                    playerHealth.Heal(healAmount);
//                }

//                // 🔊 El sonido ya se reproduce en PlayerHealth.Heal()

//                // Efecto de partículas
//                if (pickupEffect != null)
//                {
//                    Instantiate(pickupEffect, transform.position, Quaternion.identity);
//                }

//                Destroy(gameObject);
//            }
//        }
//    }
//}




//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class WhiskyBottle : MonoBehaviour
//{
//    [Header("Configuración")]
//    public bool fullHeal = true; // Si restaura todos los corazones
//    public int healAmount = 5; // Cuántos corazones restaura (si fullHeal es false)
//    public AudioClip healSound; // Sonido al recoger

//    [Header("Efectos Visuales")]
//    public GameObject pickupEffect; // Partículas opcionales
//    public float rotationSpeed = 50f; // Velocidad de rotación
//    public float floatSpeed = 1f; // Velocidad de flotación
//    public float floatAmount = 0.3f; // Amplitud de flotación

//    private Vector3 startPosition;
//    private AudioSource audioSource;

//    void Start()
//    {
//        startPosition = transform.position;
//        audioSource = GetComponent<AudioSource>();
//    }

//    void Update()
//    {
//        // Efecto de rotación
//        transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);

//        // Efecto de flotación
//        float newY = startPosition.y + Mathf.Sin(Time.time * floatSpeed) * floatAmount;
//        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
//    }

//    void OnTriggerEnter2D(Collider2D collision)
//    {
//        if (collision.CompareTag("Player"))
//        {
//            PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();

//            if (playerHealth != null)
//            {
//                // Restaurar vida
//                if (fullHeal)
//                {
//                    playerHealth.HealFull();
//                }
//                else
//                {
//                    playerHealth.Heal(healAmount);
//                }

//                // Reproducir sonido
//                if (healSound != null && audioSource != null)
//                {
//                    AudioSource.PlayClipAtPoint(healSound, transform.position);
//                }

//                // Efecto de partículas
//                if (pickupEffect != null)
//                {
//                    Instantiate(pickupEffect, transform.position, Quaternion.identity);
//                }

//                // Destruir la botella
//                Destroy(gameObject);
//            }
//        }
//    }
//}

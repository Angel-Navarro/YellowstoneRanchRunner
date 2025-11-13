using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonedAlfalfa : MonoBehaviour
{
    [Header("Configuración")]
    public int damageAmount = 1; // Cuántos corazones quita
    public bool destroyOnContact = true; // Si se destruye al tocarlo
    public AudioClip poisonSound; // Sonido opcional

    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();

            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damageAmount);

                // Reproducir sonido si existe
                if (poisonSound != null && audioSource != null)
                {
                    audioSource.PlayOneShot(poisonSound);
                }

                // Destruir la alfalfa si está configurado
                if (destroyOnContact)
                {
                    Destroy(gameObject);
                }
            }
        }
    }
}
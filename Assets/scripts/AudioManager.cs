using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance; // Singleton para acceder desde cualquier script

    [Header("Música de Fondo")]
    public AudioClip backgroundMusic;
    private AudioSource musicSource;

    [Header("Efectos de Sonido")]
    public AudioClip jumpSound;
    public AudioClip deathSound;
    public AudioClip damageSound;
    public AudioClip healSound;
    public AudioClip winSound;
    public AudioClip pickupSound; // Para monedas u otros items

    private AudioSource sfxSource; // Para efectos de sonido

    void Awake()
    {
        // Singleton: Solo puede existir uno
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // No se destruye al cambiar de escena
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        // Crear dos AudioSources: uno para música, otro para efectos
        musicSource = gameObject.AddComponent<AudioSource>();
        sfxSource = gameObject.AddComponent<AudioSource>();

        // Configurar música
        musicSource.loop = true;
        musicSource.volume = 0.5f;

        // Configurar efectos
        sfxSource.loop = false;
        sfxSource.volume = 0.7f;

        // Iniciar música
        if (backgroundMusic != null)
        {
            musicSource.clip = backgroundMusic;
            musicSource.Play();
        }
    }

    // Métodos públicos para reproducir sonidos
    public void PlayJump()
    {
        if (jumpSound != null)
            sfxSource.PlayOneShot(jumpSound);
    }

    public void PlayDeath()
    {
        if (deathSound != null)
            sfxSource.PlayOneShot(deathSound);
    }

    public void PlayDamage()
    {
        if (damageSound != null)
            sfxSource.PlayOneShot(damageSound);
    }

    public void PlayHeal()
    {
        if (healSound != null)
            sfxSource.PlayOneShot(healSound);
    }

    public void PlayWin()
    {
        if (winSound != null)
        {
            sfxSource.PlayOneShot(winSound);
            // Opcional: bajar el volumen de la música
            musicSource.volume = 0.2f;
        }
    }

    public void PlayPickup()
    {
        if (pickupSound != null)
            sfxSource.PlayOneShot(pickupSound);
    }

    // Control de volumen
    public void SetMusicVolume(float volume)
    {
        musicSource.volume = Mathf.Clamp01(volume);
    }

    public void SetSFXVolume(float volume)
    {
        sfxSource.volume = Mathf.Clamp01(volume);
    }

    public void StopMusic()
    {
        musicSource.Stop();
    }

    public void PlayMusic()
    {
        if (!musicSource.isPlaying && backgroundMusic != null)
        {
            musicSource.Play();
        }
    }
}
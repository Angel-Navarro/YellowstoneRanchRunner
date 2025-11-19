using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishLevel : MonoBehaviour
{
    [Header("Configuración")]
    public string nextSceneName = "Menu"; // Nombre de la siguiente escena
    public GameObject winUI; // Panel de Victoria (opcional)
    public float delayBeforeNextLevel = 3f; // Tiempo antes de cambiar de nivel (aumentado para el sonido)

    [Header("Efectos")]
    public GameObject winEffect; // Partículas de victoria

    private bool levelCompleted = false;

    void Start()
    {
        if (winUI != null)
        {
            winUI.SetActive(false);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !levelCompleted)
        {
            levelCompleted = true;

            // 🔊 Reproducir sonido de victoria
            if (AudioManager.instance != null)
            {
                AudioManager.instance.PlayWin();
            }

            // Efecto de partículas
            if (winEffect != null)
            {
                Instantiate(winEffect, transform.position, Quaternion.identity);
            }

            // Desactivar controles del jugador
            JhonMovi playerMovement = collision.GetComponent<JhonMovi>();
            if (playerMovement != null)
            {
                playerMovement.enabled = false;
            }

            // Mostrar UI de victoria
            if (winUI != null)
            {
                winUI.SetActive(true);
            }

            // Cargar siguiente nivel después de un delay
            StartCoroutine(LoadNextLevel());
        }
    }

    IEnumerator LoadNextLevel()
    {
        yield return new WaitForSeconds(delayBeforeNextLevel);

        Time.timeScale = 1f; // Asegurar que el tiempo esté normal
        SceneManager.LoadScene(nextSceneName);
    }

    // Método público para botón "Siguiente Nivel"
    public void GoToNextLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(nextSceneName);
    }
}
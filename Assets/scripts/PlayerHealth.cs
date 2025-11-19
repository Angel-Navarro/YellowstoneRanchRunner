using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    [Header("Sistema de Vidas")]
    public int maxHearts = 5;
    private int currentHearts;

    [Header("UI Corazones")]
    public GameObject heartPrefab; // Prefab de un corazón (Image)
    public Transform heartsContainer; // Panel donde irán los corazones
    private List<GameObject> heartsList = new List<GameObject>();

    [Header("Game Over")]
    public GameObject gameOverUI;
    private bool isDead = false;

    [Header("Invulnerabilidad")]
    public float invulnerabilityTime = 1.5f; // Tiempo invulnerable después de recibir daño
    private bool isInvulnerable = false;
    private SpriteRenderer spriteRenderer;

    [Header("Muerte por Caída")]
    public bool enableFallDeath = true; // Activar/desactivar muerte por caída
    public float deathHeight = -10f; // Altura en Y donde muere el jugador

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        currentHearts = maxHearts;

        // Crear los corazones en el UI
        CreateHearts();

        if (gameOverUI != null)
        {
            gameOverUI.SetActive(false);
        }
    }

    void Update()
    {
        // Verificar si el jugador cayó muy abajo
        if (enableFallDeath && transform.position.y < deathHeight)
        {
            Die();
        }
    }

    void CreateHearts()
    {
        // Limpiar corazones anteriores si existen
        foreach (GameObject heart in heartsList)
        {
            Destroy(heart);
        }
        heartsList.Clear();

        // Crear los corazones
        for (int i = 0; i < maxHearts; i++)
        {
            GameObject heart = Instantiate(heartPrefab, heartsContainer);
            heartsList.Add(heart);
        }
    }

    void UpdateHeartsUI()
    {
        for (int i = 0; i < heartsList.Count; i++)
        {
            // Activar corazón si el índice es menor que las vidas actuales
            heartsList[i].SetActive(i < currentHearts);
        }
    }

    public void TakeDamage(int damage = 1)
    {
        if (isDead || isInvulnerable) return;

        currentHearts -= damage;
        currentHearts = Mathf.Max(currentHearts, 0);

        UpdateHeartsUI();

        // 🔊 Reproducir sonido de daño
        if (AudioManager.instance != null)
        {
            AudioManager.instance.PlayDamage();
        }

        if (currentHearts <= 0)
        {
            Die();
        }
        else
        {
            StartCoroutine(InvulnerabilityCoroutine());
        }
    }

    public void Heal(int amount)
    {
        if (isDead) return;

        currentHearts += amount;
        currentHearts = Mathf.Min(currentHearts, maxHearts);

        UpdateHeartsUI();

        // 🔊 Reproducir sonido de curación
        if (AudioManager.instance != null)
        {
            AudioManager.instance.PlayHeal();
        }

        Debug.Log($"¡Curado! Vida actual: {currentHearts}/{maxHearts}");
    }

    public void HealFull()
    {
        if (isDead) return;

        currentHearts = maxHearts;
        UpdateHeartsUI();

        Debug.Log("HealFull() llamado - Curando completamente");

        // 🔊 Reproducir sonido de curación
        if (AudioManager.instance != null)
        {
            Debug.Log("Reproduciendo sonido de curación (heal)");
            AudioManager.instance.PlayHeal();
        }
        else
        {
            Debug.LogError("¡AudioManager no encontrado al intentar curar!");
        }

        Debug.Log("¡Vida completamente restaurada!");
    }

    IEnumerator InvulnerabilityCoroutine()
    {
        isInvulnerable = true;

        // Efecto de parpadeo
        float elapsed = 0f;
        while (elapsed < invulnerabilityTime)
        {
            spriteRenderer.color = new Color(1, 1, 1, 0.5f); // Semi-transparente
            yield return new WaitForSeconds(0.1f);
            spriteRenderer.color = Color.white; // Normal
            yield return new WaitForSeconds(0.1f);
            elapsed += 0.2f;
        }

        spriteRenderer.color = Color.white; // Asegurar que quede normal
        isInvulnerable = false;
    }

    public void Die()
    {
        if (isDead) return;
        isDead = true;

        // 🔊 Reproducir sonido de muerte
        if (AudioManager.instance != null)
        {
            AudioManager.instance.PlayDeath();
        }

        // Desactivar controles del jugador
        JhonMovi playerMovement = GetComponent<JhonMovi>();
        if (playerMovement != null)
        {
            playerMovement.enabled = false;
        }

        // Detener el movimiento
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
        }

        // Activar animación de muerte si existe
        Animator animator = GetComponent<Animator>();
        if (animator != null)
        {
            animator.SetTrigger("Death");
        }

        StartCoroutine(ShowGameOver());
    }

    IEnumerator ShowGameOver()
    {
        yield return new WaitForSeconds(0.5f);

        if (gameOverUI != null)
        {
            gameOverUI.SetActive(true);
            Time.timeScale = 0f;
        }
        else
        {
            LoadMainMenu();
        }
    }

    public void RestartLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }
}



//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;
//using UnityEngine.SceneManagement;

//public class PlayerHealth : MonoBehaviour
//{
//    [Header("Sistema de Vidas")]
//    public int maxHearts = 5;
//    private int currentHearts;

//    [Header("UI Corazones")]
//    public GameObject heartPrefab; // Prefab de un corazón (Image)
//    public Transform heartsContainer; // Panel donde irán los corazones
//    private List<GameObject> heartsList = new List<GameObject>();

//    [Header("Game Over")]
//    public GameObject gameOverUI;
//    private bool isDead = false;

//    [Header("Invulnerabilidad")]
//    public float invulnerabilityTime = 1.5f; // Tiempo invulnerable después de recibir daño
//    private bool isInvulnerable = false;
//    private SpriteRenderer spriteRenderer;

//    void Start()
//    {
//        spriteRenderer = GetComponent<SpriteRenderer>();
//        currentHearts = maxHearts;

//        // Crear los corazones en el UI
//        CreateHearts();

//        if (gameOverUI != null)
//        {
//            gameOverUI.SetActive(false);
//        }
//    }

//    void CreateHearts()
//    {
//        // Limpiar corazones anteriores si existen
//        foreach (GameObject heart in heartsList)
//        {
//            Destroy(heart);
//        }
//        heartsList.Clear();

//        // Crear los corazones
//        for (int i = 0; i < maxHearts; i++)
//        {
//            GameObject heart = Instantiate(heartPrefab, heartsContainer);
//            heartsList.Add(heart);
//        }
//    }

//    void UpdateHeartsUI()
//    {
//        for (int i = 0; i < heartsList.Count; i++)
//        {
//            // Activar corazón si el índice es menor que las vidas actuales
//            heartsList[i].SetActive(i < currentHearts);
//        }
//    }

//    public void TakeDamage(int damage = 1)
//    {
//        if (isDead || isInvulnerable) return;

//        currentHearts -= damage;
//        currentHearts = Mathf.Max(currentHearts, 0); // No bajar de 0

//        UpdateHeartsUI();

//        if (currentHearts <= 0)
//        {
//            Die();
//        }
//        else
//        {
//            // Activar invulnerabilidad temporal
//            StartCoroutine(InvulnerabilityCoroutine());
//        }
//    }

//    IEnumerator InvulnerabilityCoroutine()
//    {
//        isInvulnerable = true;

//        // Efecto de parpadeo
//        float elapsed = 0f;
//        while (elapsed < invulnerabilityTime)
//        {
//            spriteRenderer.color = new Color(1, 1, 1, 0.5f); // Semi-transparente
//            yield return new WaitForSeconds(0.1f);
//            spriteRenderer.color = Color.white; // Normal
//            yield return new WaitForSeconds(0.1f);
//            elapsed += 0.2f;
//        }

//        spriteRenderer.color = Color.white; // Asegurar que quede normal
//        isInvulnerable = false;
//    }

//    public void Die()
//    {
//        if (isDead) return;
//        isDead = true;

//        // Desactivar controles del jugador
//        JhonMovi playerMovement = GetComponent<JhonMovi>();
//        if (playerMovement != null)
//        {
//            playerMovement.enabled = false;
//        }

//        // Detener el movimiento
//        Rigidbody2D rb = GetComponent<Rigidbody2D>();
//        if (rb != null)
//        {
//            rb.linearVelocity = Vector2.zero;
//        }

//        // Activar animación de muerte si existe
//        Animator animator = GetComponent<Animator>();
//        if (animator != null)
//        {
//            animator.SetTrigger("Death");
//        }

//        StartCoroutine(ShowGameOver());
//    }

//    IEnumerator ShowGameOver()
//    {
//        yield return new WaitForSeconds(0.5f);

//        if (gameOverUI != null)
//        {
//            gameOverUI.SetActive(true);
//            Time.timeScale = 0f;
//        }
//        else
//        {
//            LoadMainMenu();
//        }
//    }

//    public void RestartLevel()
//    {
//        Time.timeScale = 1f;
//        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
//    }

//    public void LoadMainMenu()
//    {
//        Time.timeScale = 1f;
//        SceneManager.LoadScene("Menu");
//    }
//}





//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.SceneManagement;

//public class PlayerHealth : MonoBehaviour
//{
//    public GameObject gameOverUI; // Panel de Game Over en el Canvas
//    private bool isDead = false;

//    void Start()
//    {
//        // Asegurarse de que el panel esté oculto al inicio
//        if (gameOverUI != null)
//        {
//            gameOverUI.SetActive(false);
//        }
//    }

//    public void Die()
//    {
//        if (isDead) return; // Evitar múltiples muertes

//        isDead = true;

//        // Desactivar controles del jugador
//        JhonMovi playerMovement = GetComponent<JhonMovi>();
//        if (playerMovement != null)
//        {
//            playerMovement.enabled = false;
//        }

//        // Detener el movimiento
//        Rigidbody2D rb = GetComponent<Rigidbody2D>();
//        if (rb != null)
//        {
//            rb.linearVelocity = Vector2.zero;
//        }

//        // Activar animación de muerte si la tienes
//        Animator animator = GetComponent<Animator>();
//        if (animator != null && animator.GetBool("SpeedBool"))
//        {
//            animator.SetTrigger("Death"); // Opcional: crea este trigger si tienes animación
//        }

//        // Mostrar Game Over
//        StartCoroutine(ShowGameOver());
//    }

//    IEnumerator ShowGameOver()
//    {
//        // Esperar un poco antes de mostrar el Game Over
//        yield return new WaitForSeconds(0.5f);

//        if (gameOverUI != null)
//        {
//            gameOverUI.SetActive(true);
//            Time.timeScale = 0f; // Pausar el juego (opcional)
//        }
//        else
//        {
//            // Si no hay UI, ir directo al menú
//            LoadMainMenu();
//        }
//    }

//    // Método público para el botón de Restart
//    public void RestartLevel()
//    {
//        Time.timeScale = 1f; // Reanudar el tiempo
//        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
//    }

//    // Método público para el botón de Menu
//    public void LoadMainMenu()
//    {
//        Time.timeScale = 1f; // Reanudar el tiempo
//        SceneManager.LoadScene("Menu"); //Manda al menu principal
//    }
//}
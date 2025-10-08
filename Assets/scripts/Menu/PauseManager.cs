using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour
{
    public Button botonPause;
    public GameObject panelPausa; // Opcional: un panel que aparece al pausar

    void Start()
    {
        // Guardar que el jugador está en este nivel
        PlayerPrefs.SetString("UltimaEscena", SceneManager.GetActiveScene().name);
        PlayerPrefs.Save();

        botonPause.onClick.AddListener(IrAlMenu);
    }

    void IrAlMenu()
    {
        Time.timeScale = 1; // Restaurar el tiempo antes de cambiar de escena
        SceneManager.LoadScene("Menu");
    }

    // Opcional: Pausar el juego
    public void PausarJuego()
    {
        Time.timeScale = 0;
        if (panelPausa != null)
            panelPausa.SetActive(true);
    }

    public void ReanudarJuego()
    {
        Time.timeScale = 1;
        if (panelPausa != null)
            panelPausa.SetActive(false);
    }
}

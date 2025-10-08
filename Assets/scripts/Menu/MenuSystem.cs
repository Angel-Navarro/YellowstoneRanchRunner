using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuSystem : MonoBehaviour
{
    public Button botonNivel1;
    public Button botonNivel2;
    public Button botonNivel3;
    public Button botonSeguir;
    public Button botonSalir;

    void Start()
    {
        // Verificar si hay una partida guardada
        if (PlayerPrefs.HasKey("UltimaEscena"))
        {
            botonSeguir.interactable = true;
        }
        else
        {
            botonSeguir.interactable = false;
        }

        // Asignar funciones a los botones
        botonNivel1.onClick.AddListener(() => CargarNivel("Nivel1"));
        botonNivel2.onClick.AddListener(() => CargarNivel("Nivel2"));
        botonNivel3.onClick.AddListener(() => CargarNivel("Nivel3"));
        botonSeguir.onClick.AddListener(Seguir);
        botonSalir.onClick.AddListener(Salir);
    }

    void CargarNivel(string nombreEscena)
    {
        // Guardar la escena actual
        PlayerPrefs.SetString("UltimaEscena", nombreEscena);
        PlayerPrefs.Save();
        
        SceneManager.LoadScene(nombreEscena);
    }

    void Seguir()
    {
        string ultimaEscena = PlayerPrefs.GetString("UltimaEscena");
        SceneManager.LoadScene(ultimaEscena);
    }

    void Salir()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}

using UnityEngine;
using UnityEngine.SceneManagement;

public class MetaController : MonoBehaviour
{
    public string nombreSiguienteEscena;
    public Animator animacionObjetoExtra; // Animator del objeto que quieres animar
    public float tiempoEspera = 2f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // 🔊 Reproducir sonido de victoria
            if (AudioManager.instance != null)
            {
                Debug.Log("¡Victoria! Reproduciendo sonido de win");
                AudioManager.instance.PlayWin();
            }
            else
            {
                Debug.LogWarning("AudioManager no encontrado - No se puede reproducir sonido de victoria");
            }

            // Reproducir animación del objeto extra
            if (animacionObjetoExtra != null)
                animacionObjetoExtra.SetTrigger("ActivarASalvo");

            // Desactiva el movimiento del jugador (opcional)
            JhonMovi playerMovement = collision.GetComponent<JhonMovi>();
            if (playerMovement != null)
                playerMovement.enabled = false;

            // Inicia la espera y luego cambia de escena
            StartCoroutine(CambiarEscena());
        }
    }

    private System.Collections.IEnumerator CambiarEscena()
    {
        // Espera el tiempo definido para que se vea la animación y se escuche el sonido
        yield return new WaitForSeconds(tiempoEspera);

        // Cambia a la siguiente escena
        SceneManager.LoadScene(nombreSiguienteEscena);
    }
}


//using UnityEngine;
//using UnityEngine.SceneManagement;

//public class MetaController : MonoBehaviour
//{
//    public string nombreSiguienteEscena;
//    public Animator animacionObjetoExtra; // Animator del objeto que quieres animar
//    public float tiempoEspera = 2f;

//    private void OnTriggerEnter2D(Collider2D collision)
//    {
//        if (collision.CompareTag("Player"))
//        {
//            // Reproducir animación del objeto extra
//            if (animacionObjetoExtra != null)
//                animacionObjetoExtra.SetTrigger("ActivarASalvo");

//            // Desactiva el movimiento del jugador (opcional)
//            collision.GetComponent<JhonMovi>().enabled = false;

//            // Inicia la espera y luego cambia de escena
//            StartCoroutine(CambiarEscena());
//        }
//    }

//    private System.Collections.IEnumerator CambiarEscena()
//    {
//        // Espera el tiempo definido para que se vea la animación
//        yield return new WaitForSeconds(tiempoEspera);

//        // Cambia a la siguiente escena
//        SceneManager.LoadScene(nombreSiguienteEscena);
//    }
//}






//using UnityEngine;
//using UnityEngine.SceneManagement;

//public class MetaController : MonoBehaviour
//{
//    public string nombreSiguienteEscena;  // Nombre de la escena a cargar
//    public Animator animacionFinal;       // Asigna el Animator del personaje (o de la cámara si prefieres)
//    public Animator animacionObjetoExtra;    // 👈 animación del otro objeto
//    public float tiempoEspera = 2f;       // Tiempo antes de cambiar de escena

//    private void OnTriggerEnter2D(Collider2D collision)
//    {
//        // Verifica si el jugador tocó la meta
//        if (collision.CompareTag("Player"))
//        {
//            //// Inicia la animación si la tienes configurada
//            //if (animacionFinal != null)
//            //{
//            //    animacionFinal.SetTrigger("Ganar"); // Asegúrate que el Animator tenga este trigger
//            //}

//            // Reproducir animación del otro objeto
//            if (animacionObjetoExtra != null)
//                animacionObjetoExtra.SetTrigger("ActivarASalvo"); // 👈 este Trigger debe existir en su Animator

//            // Desactiva movimiento del jugador (opcional)
//            collision.GetComponent<JhonMovi>().enabled = false;

//            // Cambia de escena después de un tiempo
//            StartCoroutine(CambiarEscena());
//        }
//    }

//    private System.Collections.IEnumerator CambiarEscena()
//    {
//        yield return new WaitForSeconds(tiempoEspera);
//        SceneManager.LoadScene(nombreSiguienteEscena);
//    }
//}

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
            // Reproducir animación del objeto extra
            if (animacionObjetoExtra != null)
                animacionObjetoExtra.SetTrigger("ActivarASalvo");

            // Desactiva el movimiento del jugador (opcional)
            collision.GetComponent<JhonMovi>().enabled = false;

            // Inicia la espera y luego cambia de escena
            StartCoroutine(CambiarEscena());
        }
    }

    private System.Collections.IEnumerator CambiarEscena()
    {
        // Espera el tiempo definido para que se vea la animación
        yield return new WaitForSeconds(tiempoEspera);

        // Cambia a la siguiente escena
        SceneManager.LoadScene(nombreSiguienteEscena);
    }
}






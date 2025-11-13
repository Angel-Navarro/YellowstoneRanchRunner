using UnityEngine;

public class Parralax : MonoBehaviour
{
    public float parralaxMultiplier = 0.3f;
    private Material parralaxMaterial;
    private Transform mainCamera;
    private float startCameraX; // ✅ Guardar posición inicial en vez de última

    void Start()
    {
        parralaxMaterial = GetComponent<Renderer>().material;
        parralaxMaterial.mainTextureOffset = Vector2.zero;
        
        mainCamera = Camera.main.transform;
        startCameraX = mainCamera.position.x; // ✅ Posición inicial de referencia
        
        Debug.Log("Parallax inicializado en: " + gameObject.name);
    }

    void Update()
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main.transform;
            return;
        }

        // ✅ Calcular desde la posición inicial, no desde la última
        float distance = mainCamera.position.x - startCameraX;
        
        // ✅ Setear directamente el offset en vez de acumularlo
        parralaxMaterial.mainTextureOffset = new Vector2(distance * parralaxMultiplier, 0);
        
        Debug.Log("Parallax | Distance: " + distance + " | Offset: " + (distance * parralaxMultiplier));
    }
}



//using UnityEngine;

//public class Parralax : MonoBehaviour
//{
//    public float parralaxMultiflier = 0.3f;
//    private Material parralaxMaterial;
//    private Transform mainCamera;
//    private float lastCameraX;

//    void Start()
//    {
//        parralaxMaterial = GetComponent<Renderer>().material;
//        parralaxMaterial.mainTextureOffset = Vector2.zero;

//        // Usar la cámara en lugar del player
//        mainCamera = Camera.main.transform;
//        lastCameraX = mainCamera.position.x;

//        Debug.Log("Parallax inicializado en: " + gameObject.name);
//    }

//    void Update()
//    {
//        if (mainCamera == null)
//        {
//            mainCamera = Camera.main.transform;
//            return;
//        }

//        float deltaX = mainCamera.position.x - lastCameraX;

//        if (Mathf.Abs(deltaX) > 0.001f)
//        {
//            parralaxMaterial.mainTextureOffset += new Vector2(deltaX * parralaxMultiflier, 0);
//            Debug.Log("Parallax moviendo | DeltaX: " + deltaX);
//        }

//        lastCameraX = mainCamera.position.x;
//    }
//}
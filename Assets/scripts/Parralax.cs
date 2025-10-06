using UnityEngine;

public class Parralax : MonoBehaviour
{
    public float parralaxMultiflier = 0.3f;
    private Material parralaxMaterial;
    private Transform mainCamera;
    private float lastCameraX;

    void Start()
    {
        parralaxMaterial = GetComponent<Renderer>().material;
        parralaxMaterial.mainTextureOffset = Vector2.zero;

        // Usar la cámara en lugar del player
        mainCamera = Camera.main.transform;
        lastCameraX = mainCamera.position.x;

        Debug.Log("Parallax inicializado en: " + gameObject.name);
    }

    void Update()
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main.transform;
            return;
        }

        float deltaX = mainCamera.position.x - lastCameraX;

        if (Mathf.Abs(deltaX) > 0.001f)
        {
            parralaxMaterial.mainTextureOffset += new Vector2(deltaX * parralaxMultiflier, 0);
            Debug.Log("Parallax moviendo | DeltaX: " + deltaX);
        }

        lastCameraX = mainCamera.position.x;
    }
}
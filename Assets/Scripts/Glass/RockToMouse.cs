using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockToMouse : MonoBehaviour
{
    private Camera cam;  // Referencia a la cámara principal
    private Vector2 spriteSize; // Tamaño del sprite en unidades del mundo

    void Start()
    {
        cam = Camera.main;
        if (TryGetComponent<SpriteRenderer>(out SpriteRenderer spriteRenderer))
        {
            spriteSize = spriteRenderer.bounds.size; // Obtener el tamaño del sprite
        }
        else
        {
            spriteSize = Vector2.zero;
            Debug.LogWarning("⚠️ No se encontró SpriteRenderer en el objeto.");
        }
    }

    void Update()
    {
        FollowMouse();
    }

    void FollowMouse()
    {
        if (cam == null) return;

        Vector3 mousePos = Input.mousePosition; // Posición del ratón en pantalla
        mousePos.z = Mathf.Abs(cam.transform.position.z); // Ajustar la profundidad (Z)
        Vector3 worldPos = cam.ScreenToWorldPoint(mousePos); // Convertir a coordenadas del mundo

        // Ajustar la posición para que la esquina superior izquierda siga el ratón
        float newX = worldPos.x + spriteSize.x / 2;
        float newY = worldPos.y - spriteSize.y / 2;

        transform.position = new Vector3(newX, newY, transform.position.z);
    }
}

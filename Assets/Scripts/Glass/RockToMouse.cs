using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockToMouse : MonoBehaviour
{
    private Camera cam;  // Referencia a la cámara principal
    private Vector2 spriteSize; // Tamaño del sprite en unidades del mundo
    private int golpes = -1; // Contador de golpes
    private HashSet<GameObject> contactos = new HashSet<GameObject>(); // Almacena triggers en contacto

    [SerializeField] private List<GameObject> puntosValidos; // Lista de objetos válidos (asignar en Inspector)

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
    public int getGolpes()
    {
        return golpes;
    }

    void Update()
    {
        FollowMouse();

        // Solo sumar golpe si se hace clic y está sobre un objeto en la lista de puntos válidos
        if (Input.GetMouseButtonDown(0))
        {
            foreach (GameObject obj in contactos)
            {
                if (puntosValidos.Contains(obj))
                {
                    golpes++;
                    Debug.Log($"Golpes: {golpes}");
                    break; // Evita sumar múltiples veces si hay más de un objeto en contacto
                }
            }
        }
    }

    void FollowMouse()
    {
        if (cam == null) return;

        Vector3 mousePos = Input.mousePosition; // Posición del ratón en pantalla
        mousePos.z = Mathf.Abs(cam.transform.position.z); // Ajustar la profundidad (Z)
        Vector3 worldPos = cam.ScreenToWorldPoint(mousePos); // Convertir a coordenadas del mundo

        // Ajustar la posición para que la esquina inferior izquierda siga el ratón
        float newX = worldPos.x + spriteSize.x / 2; // Desplazar hacia la derecha
        float newY = worldPos.y + spriteSize.y / 2; // Subir para alinear la esquina inferior izquierda

        transform.position = new Vector3(newX, newY, transform.position.z);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (puntosValidos.Contains(other.gameObject)) // Solo agregar si está en la lista
        {
            contactos.Add(other.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (contactos.Contains(other.gameObject))
        {
            contactos.Remove(other.gameObject);
        }
    }
}

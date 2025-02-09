using UnityEngine;

public class ParallaxCursor : MonoBehaviour
{
    public float verticalOffset = 0f;    // Ajuste vertical del fondo
    public float movementFactor = 0.1f;   // Controla cuánto se mueve el fondo
    public float rotationSpeed = 5f;      // Velocidad de rotación

    private Vector3 startPosition;
    private Quaternion startRotation;

    void Start()
    {
        startPosition = transform.position;
        startRotation = transform.rotation; // Almacena la rotación inicial
    }

    void Update()
    {
        Vector2 mousePos = Input.mousePosition;
        float moveX = (mousePos.x / Screen.width - 0.5f) * movementFactor;
        float moveY = (mousePos.y / Screen.height - 0.5f) * movementFactor;

        transform.position = startPosition + new Vector3(moveX, moveY + verticalOffset, 0);
        
        // Rotación del fondo basada en la rotación inicial
        float rotationZ = (mousePos.x / Screen.width - 0.5f) * rotationSpeed;
        transform.rotation = startRotation * Quaternion.Euler(0, 0, rotationZ); // Mantiene la rotación inicial
    }
}
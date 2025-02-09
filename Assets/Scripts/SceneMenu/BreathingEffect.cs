using UnityEngine;

public class BreathingEffect : MonoBehaviour
{
    [Header("Escala de Respiración")]
    public float minScale = 0.9f; // Tamaño mínimo
    public float maxScale = 1.1f; // Tamaño máximo
    public float speed = 1f; // Velocidad de la animación

    private Vector3 initialScale;
    private float time;

    void Start()
    {
        // Guardar la escala inicial del objeto
        initialScale = transform.localScale;
    }

    void Update()
    {
        // Aplicar un efecto de "respiración" usando Mathf.Sin
        float scale = Mathf.Lerp(minScale, maxScale, (Mathf.Sin(time * speed) + 1) / 2);
        transform.localScale = initialScale * scale;
        
        // Incrementar el tiempo
        time += Time.deltaTime;
    }
}
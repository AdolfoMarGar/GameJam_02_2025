using UnityEngine;

public class DestruirAlEntrar : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Destruir el objeto con el que colisiona
        Destroy(other.gameObject);
    }
}
using System.Collections;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public Vector3 OriginalCameraPosition;

    void Start()
    {
        OriginalCameraPosition = transform.localPosition;
    }
    public IEnumerator Shake(float duration, float magnitude)
    {
        Debug.Log("CameraShake");
        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            transform.localPosition = new Vector3(OriginalCameraPosition.x + x, OriginalCameraPosition.y + y, OriginalCameraPosition.z);

            elapsed += Time.deltaTime;
            yield return null; // Espera un frame
        }   

        transform.localPosition = OriginalCameraPosition; // Restaurar posición original
    }
}
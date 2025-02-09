using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;  // Necesario para cambiar de escena

public class PrefabActivator : MonoBehaviour
{
    public GameObject prefabToActivate;  // Prefab que se activará
    public Transform spawnPoint;         // El punto donde se activará el prefab
    public float activationDelay = 3f;   // Retraso para activar el prefab (en segundos)
    public float animationDelay = 1f;    // Retraso para comenzar la animación (en segundos)

    private GameObject spawnedPrefab;    // Instancia del prefab activado

    void Update()
    {
        // Espera hasta que se presione la tecla espacio
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Comienza la corutina para activar el prefab y forzar su animación
            StartCoroutine(ActivatePrefabAndAnimate());
        }
    }

    IEnumerator ActivatePrefabAndAnimate()
    {
        // Espera el tiempo de activación configurado
        yield return new WaitForSeconds(activationDelay);

        // Instancia el prefab en el spawn point
        if (prefabToActivate != null && spawnPoint != null)
        {
            spawnedPrefab = Instantiate(prefabToActivate, spawnPoint.position, Quaternion.identity);
            Debug.Log("Prefab activado");
        }

        // Espera el tiempo de retraso de la animación
        yield return new WaitForSeconds(animationDelay);

        // Forzar la animación si el prefab tiene un Animator
        if (spawnedPrefab != null)
        {
            Animator prefabAnimator = spawnedPrefab.GetComponent<Animator>();
            if (prefabAnimator != null)
            {
                prefabAnimator.Play(0, 0, 0f); // Forza la animación desde el inicio
                Debug.Log("Animación forzada");

                // Espera a que termine la animación
                float animationDuration = prefabAnimator.GetCurrentAnimatorStateInfo(0).length;
                yield return new WaitForSeconds(animationDuration);  // Espera que termine la animación

                // Espera 2 segundos después de la animación
                yield return new WaitForSeconds(2f);

                // Cambiar a la siguiente escena (escena con índice 3)
                SceneManager.LoadScene(3);
                Debug.Log("Escena cambiada a la 3");
            }
            else
            {
                Debug.LogWarning("El prefab no tiene un Animator.");
            }
        }
    }
}

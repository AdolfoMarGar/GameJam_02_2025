using UnityEngine;
using System.Collections;

public class GameStarter : MonoBehaviour
{
    public GameObject introPrefab; // Prefab que se spawnea con la animación
    public Transform spawnPoint; // Punto donde aparece el prefab (arrástralo en el editor)
    public float startDelay = 3f; // Tiempo antes de iniciar el juego
    public NoteSpawner noteSpawnerScript; // Referencia directa al script NoteSpawner
    public AudioSource musicSource; // AudioSource de la música

    private GameObject spawnedIntro; // Referencia al prefab instanciado

    void Start()
    {
        // Spawnea el prefab de introducción si está asignado
        if (introPrefab != null && spawnPoint != null)
        {
            spawnedIntro = Instantiate(introPrefab, spawnPoint.position, Quaternion.identity);
        }

        // Desactiva el NoteSpawner (desactiva el script en vez del GameObject) y el AudioSource al inicio
        if (noteSpawnerScript != null) noteSpawnerScript.enabled = false;
        if (musicSource != null) musicSource.enabled = false;

        // Espera X segundos y luego inicia el juego
        StartCoroutine(StartGameAfterDelay());
    }

    IEnumerator StartGameAfterDelay()
    {
        yield return new WaitForSeconds(startDelay);

        // Destruye el prefab de introducción si fue creado
        if (spawnedIntro != null)
        {
            Destroy(spawnedIntro);
        }

        // Activa el NoteSpawner y el AudioSource
        if (noteSpawnerScript != null) noteSpawnerScript.enabled = true;
        if (musicSource != null)
        {
            musicSource.enabled = true;
            musicSource.Play();
        }
    }
}

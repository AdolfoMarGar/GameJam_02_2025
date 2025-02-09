using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement; // Necesario para cambiar de escena

public class GameStarter : MonoBehaviour
{
    public GameObject introPrefab; // Prefab que se spawnea con la animación
    public Transform spawnPoint; // Punto donde aparece el prefab (arrástralo en el editor)
    public float startDelay = 3f; // Tiempo antes de iniciar el juego
    public NoteSpawner noteSpawnerScript; // Referencia directa al script NoteSpawner
    public AudioSource musicSource; // AudioSource de la música
    public GameObject notebookObject; // Objeto de la libreta que aparece al inicio
    public GameObject initialImageObject; // Objeto de la imagen inicial que aparece al inicio
    public MonoBehaviour scriptToDisable; // Script que será desactivado antes de iniciar la intro

    public GameObject secondPrefab; // Segundo prefab a activar
    public Transform secondSpawnPoint; // Punto donde aparece el segundo prefab
    public float secondPrefabDelay = 5f; // Tiempo de espera antes de activar el segundo prefab
    public float sceneChangeDelay = 3f; // Tiempo de espera antes de cambiar de escena

    private GameObject spawnedIntro; // Referencia al prefab instanciado
    private bool introStarted = false; // Control para evitar múltiples inicios

    void Start()
    {
        // Mostrar los objetos iniciales si están asignados
        if (notebookObject != null)
        {
            notebookObject.SetActive(true);
        }
        if (initialImageObject != null)
        {
            initialImageObject.SetActive(true);
        }

        // Desactiva el NoteSpawner y el AudioSource al inicio
        if (noteSpawnerScript != null) noteSpawnerScript.enabled = false;
        if (musicSource != null) musicSource.enabled = false;

        // Desactivar el script indicado en el editor
        if (scriptToDisable != null)
        {
            scriptToDisable.enabled = false;
        }
    }

    void Update()
    {
        // Al presionar espacio, oculta los objetos iniciales y comienza la intro
        if (Input.GetKeyDown(KeyCode.Space) && !introStarted)
        {
            if (notebookObject != null)
            {
                notebookObject.SetActive(false);
            }
            if (initialImageObject != null)
            {
                initialImageObject.SetActive(false);
            }
            introStarted = true;
            StartCoroutine(SpawnIntroAndStartGame());
        }
    }

    IEnumerator SpawnIntroAndStartGame()
    {
        // Spawnea el prefab de introducción
        if (introPrefab != null && spawnPoint != null)
        {
            spawnedIntro = Instantiate(introPrefab, spawnPoint.position, Quaternion.identity);
        }

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

        // Espera el tiempo determinado antes de activar el segundo prefab
        yield return new WaitForSeconds(secondPrefabDelay);
        
        if (secondPrefab != null && secondSpawnPoint != null)
        {
            GameObject spawnedSecond = Instantiate(secondPrefab, secondSpawnPoint.position, Quaternion.identity);
            Animator anim = spawnedSecond.GetComponent<Animator>();
            if (anim != null)
            {
                anim.Play(0, 0, 0f); // Fuerza la animación del prefab
            }
        }
        
        // Espera antes de cambiar de escena
        yield return new WaitForSeconds(sceneChangeDelay);
        
        // Cambia a la escena con índice 3
        SceneManager.LoadScene(3);
    }
}

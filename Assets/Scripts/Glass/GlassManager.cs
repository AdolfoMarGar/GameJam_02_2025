using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GlassManager : MonoBehaviour
{
    public List<Sprite> listaDeSprites;  // Lista de sprites que cambiarán en bucle
    public List<Sprite> imagenesFinales;  // Lista de sprites que cambiarán en bucle
    public Sprite jumpscareSprite;  // Lista de sprites que cambiarán en bucle
    private bool tutorialDone = false;
    public GameObject tutorial;
    private SpriteRenderer imgRenderer;  // Referencia al SpriteRenderer de la piedra
    public GameObject imagen;
    public GameObject piedra;
    private GameObject spawnedIntro; // Referencia al prefab instanciado
    public GameObject introPrefab; // Prefab que se spawnea con la animación
    public GameObject outroPrefab; // Prefab que se spawnea con la animación
    public Transform spawnPoint; // Punto donde aparece el prefab (arrástralo en el editor)
    private RockToMouse piedraController; // Referencia al controlador de la piedra
    private int golpes = 0;
    private bool jumpsCareOn = false;
    [SerializeField] private List<GameObject> puntosValidos; // Lista de objetos válidos (asignar en Inspector)
    public GameObject sonido; // Prefab que se spawnea con la animación
    public GameObject sonidoFinal; // Prefab que se spawnea con la animación
    public GameObject sonidoZusto; // Prefab que se spawnea con la animación
    private AudioManagerGlass sonidoFinalController; // Referencia al controlador de la piedra
    private AudioManagerGlass sonidoZustoController; // Referencia al controlador de la piedra

    private bool countImageExecuted = false;  // Bandera para asegurar que CountImage solo se ejecute una vez

    void Start()
    {
        sonidoFinalController = sonidoFinal.GetComponent<AudioManagerGlass>();
        sonidoZustoController = sonidoZusto.GetComponent<AudioManagerGlass>();

        StartCoroutine(StartScene());
        imgRenderer = imagen.GetComponent<SpriteRenderer>();

        // Obtener el componente RockToMouse del objeto piedra
        if (piedra != null)
        {
            piedraController = piedra.GetComponent<RockToMouse>();

            if (piedraController == null)
            {
                Debug.LogError("⚠️ No se encontró el componente RockToMouse en el objeto piedra.");
            }
        }
        else
        {
            Debug.LogError("⚠️ La referencia a piedra no está asignada en el Inspector.");
        }
    }

    IEnumerator StartScene()
    {
        // Espera hasta que el jugador presione Espacio
        while (!Input.GetKeyDown(KeyCode.Space))
        {
            yield return null;  // Espera un frame antes de volver a verificar
        }

        // Se ejecuta una vez cuando se rompe el bucle (cuando se presiona Espacio)
        if (!tutorialDone)
        {
            tutorial.SetActive(false);
            startAnimDoor();
            tutorialDone = true;

            yield return new WaitForSeconds(3.5f);  // Espera un tiempo antes de activar la piedra
            piedra.SetActive(true);
            sonido.SetActive(true);
            StartCoroutine(ManagePoints());
        }
    }

    IEnumerator ManagePoints()
    {
        // Espera hasta que el jugador presione Espacio o el número de golpes llegue a 7
        while (golpes < 6)
        {
            // Desactivar el punto anterior (si no es el primer golpe)
            if (golpes > -1)
            {
                puntosValidos[golpes].SetActive(false);
            }

            // Activar el siguiente punto en la lista
            if (golpes < puntosValidos.Count - 1)
            {
                puntosValidos[golpes + 1].SetActive(true);
            }

            yield return null;  // Espera un frame antes de volver a verificar
        }
        puntosValidos[6].SetActive(false);
        sonidoFinalController.setPlaySound(true);
        StartCoroutine(JumpScare());
    }

    IEnumerator JumpScare()
    {
        yield return new WaitForSeconds(2f);  // Espera un tiempo antes de activar la piedra
        sonidoZustoController.setPlaySound(true);
        jumpsCareOn = true;
        yield return new WaitForSeconds(2f);  // Espera un tiempo antes de activar la piedra
    }


    IEnumerator Waittiririririririr()
    {
        yield return new WaitForSeconds(2f);  // Espera un tiempo antes de activar la piedra
        SceneManager.LoadScene(5);

    }

    void Update()
    {
        if (piedraController != null)
        {
            golpes = piedraController.getGolpes();
            Debug.Log($"Golpes actuales: {golpes}");

            // Cambiar el sprite según el número de golpes
            if (jumpsCareOn)
            {
                imgRenderer.sprite = jumpscareSprite;
                StartCoroutine(Waittiririririririr());
            }
            else
            {
                if (golpes >= 0 && golpes < listaDeSprites.Count)
                {
                    imgRenderer.sprite = listaDeSprites[golpes];
                }
            }
        }
    }

    private void startAnimDoor()
    {
        spawnedIntro = Instantiate(introPrefab, spawnPoint.position, Quaternion.identity);
    }
}

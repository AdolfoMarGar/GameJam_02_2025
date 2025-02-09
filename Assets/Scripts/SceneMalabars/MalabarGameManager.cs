using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MalabarGameManager : MonoBehaviour
{
    public SpriteSpawner SpriteSpawner;
    public CameraShake CameraShake;
    public DecreasingBar DecreasingBar;
    public AudioManager AudioManager;
    public Sprite monoSprite;
    public GameObject monoObject;
    private List<char> actualCombination = new List<char>();
    private float solvingTime = 2.9f;
    private bool isListening = false;
    public int totalCorrectGuesses = 0;
    public int totalWrongGuesses = 0;
    public int actualCorrectGuesses = 0;
    public int actualWrongGuesses = 0;
    public bool correctOrWrongAnswer = false;
    public bool end = false;
    private GameObject spawnedIntro; // Referencia al prefab instanciado
    public GameObject introPrefab; // Prefab que se spawnea con la animación
    public GameObject outroPrefab; // Prefab que se spawnea con la animación
    public Transform spawnPoint;
    private bool tutorialDone = false;
    public GameObject tutorial;

    void Start()
    {

        StartCoroutine(StartScene());


    }
    IEnumerator StartScene()//Se llama desde Start() con StartCoroutine(StartScene())
    {
        // Espera hasta que el jugador presione Espacio
        while (!Input.GetKeyDown(KeyCode.Space))
        {
            yield return null;
        }

        // Se ejecuta una vez cuando se rompe el bucle (cuando se presiona Espacio)
        if (!tutorialDone)
        {
            tutorial.SetActive(false);
            startAnimDoor();

            yield return new WaitForSeconds(4.5f);  // Espera un tiempo antes de activar la piedra, no cambiar el tiempo
                                                    //aqui ahora activas todos los objetos que hacen que tu juego funcione.
            charGenerator();
            tutorialDone = true;
            AudioManager.PlayMusic();


        }
    }


    void Update()
    {

        // Si es presiona una tecla i no s'esta fent la comprovació entra en la comprovació
        if (!isListening && !end && tutorialDone)
        {
            StartCoroutine(CollectKeys());
        }
    }

    // Analitza totes les tecles que introdueix el usuari, bucle principal de comprovacio
    private IEnumerator CollectKeys()
    {

        bool wrongKey = false;
        isListening = true;
        yield return new WaitForEndOfFrame();
        correctOrWrongAnswer = false;
        int counter = -1;
        float startTime = Time.time;
        StartCoroutine(DecreasingBar.DecreaseBar(solvingTime));

        while (Time.time - startTime < solvingTime)
        {

            if (Input.anyKeyDown)
            {

                string inputStr = Input.inputString;

                if (!string.IsNullOrEmpty(inputStr))
                {

                    char pressedChar = char.ToUpper(inputStr[0]);
                    //Debug.Log("Tecla presionada: " + pressedChar);
                    counter++;

                    if (actualCombination[counter] == pressedChar)
                    {

                        CorrectKey(counter);

                    }
                    else
                    {
                        wrongKey = true;
                        WrongKey(counter);
                        break;
                    }

                    if (counter >= actualCombination.Count - 1)
                    {

                        CorrectAnswer();
                        isListening = false;
                        yield break;
                    }
                }
            }
            yield return null;
        }

        if (!wrongKey)
        {
            wrongKey = false;
            WrongAnswer();
        }
        yield return new WaitForSeconds(0.6f);
        isListening = false;
    }

    private void charGenerator()
    {

        actualCombination.Clear();

        int leters;

        if (actualCorrectGuesses < 3) { leters = 2; }
        else if (actualCorrectGuesses < 5) { leters = 3; }
        else { leters = 4; }

        for (int i = 0; i < leters; i++)
        {

            int asciiInt = Random.Range(65, 90 + 1);

            while (actualCombination.Contains((char)asciiInt))
            {
                asciiInt = Random.Range(65, 90 + 1);
            }

            char ascii = (char)asciiInt;
            actualCombination.Add(ascii);

        }

        SpriteSpawner.LoadSprites(actualCombination);

        return;

    }

    private void CorrectAnswer()
    {
        correctOrWrongAnswer = true;
        totalCorrectGuesses++;
        actualCorrectGuesses++;
        actualWrongGuesses = 0;


        //SpriteSpawner.ClearAllSprites(); // Elimina todos los sprites
        charGenerator();
        //Debug.Log("¡Correcto! Has presionado la combinación correcta.");
        if (actualCorrectGuesses >= 7)
        {
            end = true;
            StartCoroutine(Victory());
        }
    }

    private void WrongAnswer()
    {
        correctOrWrongAnswer = true;
        actualWrongGuesses++;
        totalWrongGuesses++;
        actualCorrectGuesses = 0;
        AudioManager.PlayTeclaIncorrecta();
        StartCoroutine(Animation());
        StartCoroutine(CameraShake.Shake(0.5f, 0.1f));
        /*
        if(actualWrongGuesses >= 3){
            end = true;
            SpriteSpawner.ClearSprites(actualCombination);
            Defeat();
            return;
        }
        */
        charGenerator();
        Debug.Log("¡Incorrecto! Se ha presionado una tecla errónea.");
    }

    private void CorrectKey(int index)
    {
        //Debug.Log("¡Correcto!");
        AudioManager.PlayTeclaCorrecta();
        if (index >= 0 && index < actualCombination.Count)
        {
            char letter = actualCombination[index];
            SpriteSpawner.ReplaceSpriteOnCorrect(letter); // Borrar usando la letra correcta
        }
    }

    private void WrongKey(int index)
    {

        WrongAnswer();
    }

    private IEnumerator Animation()
    {
        // Obtenemos el Animator y el SpriteRenderer del objeto
        Animator animator = monoObject.GetComponent<Animator>();
        SpriteRenderer renderer = monoObject.GetComponent<SpriteRenderer>();

        // 1) Detenemos la animación actual
        animator.enabled = false;

        // 2) Asignamos el sprite "estático"
        renderer.sprite = monoSprite;

        // 3) Esperamos un tiempo
        yield return new WaitForSeconds(0.6f);

        // 4) Reactivamos el Animator para reanudar la animación
        if (!end)
        {
            animator.enabled = true;
        }
    }

    IEnumerator Victory()
    {
        Debug.Log("¡Has ganado!");
        if (outroPrefab != null && spawnPoint != null)
        {
            spawnedIntro = Instantiate(outroPrefab, spawnPoint.position, Quaternion.identity);
        }
        yield return new WaitForSeconds(1.3f);  // Esperar 0.3 segundos antes de cambiar de nuevo
        SceneManager.LoadScene(2);
    }

    public void Defeat()
    {
        Debug.Log("¡Has perdido!");
    }

    public bool getCorrectOrWrongAnswer()
    {
        return correctOrWrongAnswer;
    }
    private void startAnimDoor()
    {
        spawnedIntro = Instantiate(introPrefab, spawnPoint.position, Quaternion.identity);
    }
}
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class MalabarGameManager : MonoBehaviour{
    public SpriteSpawner SpriteSpawner; // Asigna este en el Inspector

    public List<char> actualCombination = new List<char>();
    private bool isListening = false;
    public int totalCorrectGuesses = 0;
    public int totalWrongGuesses = 0;
    public int actualCorrectGuesses = 0;
    public int actualWrongGuesses = 0;
    public float time;
    public float stime;
    public int aux = 0;

    void Start(){

    stageBasedCombination();

    }

    void Update(){

        // Si es presiona una tecla i no s'esta fent la comprovació entra en la comprovació
        if (!isListening && Input.anyKeyDown && !Input.GetKeyDown(KeyCode.Escape)) {
        StartCoroutine(CollectKeys());
    }
    }

    // Analitza totes les tecles que introdueix el usuari, bucle principal de comprovacio
    private IEnumerator CollectKeys(){

    
    isListening = true;
    int counter = -1;
    float startTime = Time.time;

    while (Time.time - startTime < 1.5f){

        if (Input.anyKeyDown){

            string inputStr = Input.inputString;

            if (!string.IsNullOrEmpty(inputStr)){
                
                char pressedChar = char.ToUpper(inputStr[0]);
                Debug.Log("Tecla presionada: " + pressedChar);
                counter++;

                if(actualCombination[counter] == pressedChar){

                    CorrectKey(counter);
                    Debug.Log("¡Correcto!");

                }
                else{
                    WrongKey(counter);
                    break;
                }

                if(counter >= actualCombination.Count - 1){

                    CorrectAnswer();
                    isListening = false;
                    yield break;
                }
            }
        }
        yield return null;
    }
    WrongAnswer();
    isListening = false;
}

    private void stageBasedCombination(){
        
        actualCombination.Clear();

        int leters;

        if(actualCorrectGuesses < 3){leters = 2;}
        else if(actualCorrectGuesses < 5){leters = 3;}
        else{leters = 4;}

        for (int i = 0; i < leters; i++){

            int asciiInt = Random.Range(65, 90 + 1);
            char ascii = (char)asciiInt;
            actualCombination.Add(ascii);

        }

        SpriteSpawner.LoadSprite(actualCombination);

        return;

    }

    private void CorrectAnswer()
{
    totalCorrectGuesses++;
    actualCorrectGuesses++;
    actualWrongGuesses = 0;

    SpriteSpawner.ClearAllSprites(); // Elimina todos los sprites
    stageBasedCombination();
    Debug.Log("¡Correcto! Has presionado la combinación correcta.");
}

private void WrongAnswer()
{
    actualWrongGuesses++;
    totalWrongGuesses++;
    actualCorrectGuesses = 0;

    SpriteSpawner.ClearAllSprites(); // Elimina todos los sprites
    stageBasedCombination();
    Debug.Log("¡Incorrecto! Se ha presionado una tecla errónea.");
}

private void CorrectKey(int index)
{
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

}

    
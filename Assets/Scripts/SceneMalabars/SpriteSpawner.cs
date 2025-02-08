using System.Collections.Generic;
using UnityEngine;

public class SpriteSpawner : MonoBehaviour
{
    public Sprite[] letterSprites = new Sprite[26]; // Sprites originales de A-Z
    public Sprite[] correctLetterSprites = new Sprite[26]; // Sprites al acertar
    public RuntimeAnimatorController keyPressAnimator; // 🎥 Controlador de animación

    private Dictionary<char, Sprite> spriteDictionary;
    private Dictionary<char, Sprite> correctSpriteDictionary;
    private Dictionary<char, GameObject> spawnedObjects = new Dictionary<char, GameObject>();

    private void Awake()
    {
        spriteDictionary = new Dictionary<char, Sprite>();
        correctSpriteDictionary = new Dictionary<char, Sprite>();

        for (int i = 0; i < 26; i++)
        {
            char letter = (char)('A' + i);
            spriteDictionary[letter] = letterSprites[i];
            correctSpriteDictionary[letter] = correctLetterSprites[i];
        }
    }

    public void LoadSprite(List<char> combination)
    {
        ClearAllSprites();

        for (int i = 0; i < combination.Count; i++)
        {
            char letter = combination[i];
            GameObject newObject = new GameObject("Sprite_" + letter);
            SpriteRenderer spriteRenderer = newObject.AddComponent<SpriteRenderer>();

            if (spriteDictionary.TryGetValue(letter, out Sprite assignedSprite))
            {
                spriteRenderer.sprite = assignedSprite;
            }
            else
            {
                Debug.LogWarning("Letra sin sprite asignado: " + letter);
            }

            // Agregar Animator si hay animación
            Animator animator = newObject.AddComponent<Animator>();
            animator.runtimeAnimatorController = keyPressAnimator; // 🎥 Asigna el Animator

            newObject.transform.position = new Vector3(2.0f, 3.0f - (i), 0f);
            newObject.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);

            spawnedObjects[letter] = newObject;
        }
    }

    // Método para activar la animación al presionar una tecla
    public void PlayKeyPressAnimation(char letter)
{
    if (spawnedObjects.TryGetValue(letter, out GameObject obj))
    {
        Animator animator = obj.GetComponent<Animator>();
        if (animator != null)
        {
            animator.ResetTrigger("Pressed"); // Resetea cualquier activación anterior
            animator.SetTrigger("Pressed");   // Activa la animación correctamente
        }
    }
}


    public void ClearAllSprites()
    {
        foreach (var obj in spawnedObjects.Values)
        {
            Destroy(obj);
        }
        spawnedObjects.Clear();
    }

    public void ReplaceSpriteOnCorrect(char letter)
    {
        if (spawnedObjects.TryGetValue(letter, out GameObject obj))
        {
            SpriteRenderer spriteRenderer = obj.GetComponent<SpriteRenderer>();

            if (correctSpriteDictionary.TryGetValue(letter, out Sprite newSprite))
            {
                spriteRenderer.sprite = newSprite;
            }
            else
            {
                Debug.LogWarning("No hay sprite de reemplazo para la letra: " + letter);
            }
        }
    }
}
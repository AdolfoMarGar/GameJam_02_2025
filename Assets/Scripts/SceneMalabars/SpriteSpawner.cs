using System.Collections.Generic;
using UnityEngine;

public class SpriteSpawner : MonoBehaviour
{
    public Sprite[] letterSprites = new Sprite[26];
    public Sprite[] correctLetterSprites = new Sprite[26];

    private Dictionary<char, Sprite> spriteDictionary;
    private Dictionary<char, Sprite> correctSpriteDictionary;
    private Dictionary<char, GameObject> spawnedObjects;
    private List<char> lastLetters = new List<char>();

    private void Awake()
    {
        InitializeDictionaries();
    }

    private void InitializeDictionaries()
    {
        spriteDictionary = new Dictionary<char, Sprite>();
        correctSpriteDictionary = new Dictionary<char, Sprite>();
        spawnedObjects = new Dictionary<char, GameObject>();

        for (int i = 0; i < 26; i++)
        {
            char letter = (char)('A' + i);
            spriteDictionary[letter] = letterSprites[i];
            correctSpriteDictionary[letter] = correctLetterSprites[i];
        }
    }

    public void LoadSprites(List<char> letters){

        //Debug.Log("LoadSpritesInici -> " + string.Join(", ", lastLetters));

        ClearSprites(lastLetters);
        
        lastLetters = lastLetters = new List<char>(letters);

        foreach (char letter in letters)
        {
            GameObject letterSprite = new GameObject("Letter" + letter);

            SpriteRenderer renderer = letterSprite.AddComponent<SpriteRenderer>();

            renderer.sprite = spriteDictionary[letter];
            
            letterSprite.transform.position = new Vector3(-3.37f, 1.28f - (1.1f * (float)spawnedObjects.Count), 10f);

            letterSprite.transform.localScale = new Vector3(0.3f, 0.3f, 0f);

            renderer.sortingOrder = 5;

            spawnedObjects[letter] = letterSprite;
        }
        
        //Debug.Log("LoadSpritesFinal -> " + string.Join(", ", lastLetters));
    }

    public void ClearSprites(List<char> letters)
    {

        //Debug.Log("ClearSprites -> " + string.Join(", ", letters));
        foreach (char letter in letters)
        {
            if (spawnedObjects.TryGetValue(letter, out GameObject obj) && obj != null)
            {
                
                //Debug.Log("Destroying letter: " + spawnedObjects[letter].name);
                Destroy(obj);
            }
        }
        spawnedObjects.Clear();

    }

    
    public void ReplaceSpriteOnCorrect(char letter)
    {
        if (spawnedObjects.TryGetValue(letter, out GameObject obj) && obj != null)
        {
            SpriteRenderer spriteRenderer = obj.GetComponent<SpriteRenderer>();
            if (correctSpriteDictionary.TryGetValue(letter, out Sprite newSprite))
            {
                spriteRenderer.sprite = newSprite;
            }
            else
            {
                //.LogWarning("No hay sprite de reemplazo para la letra: " + letter);
            }
        }
    }
    
}
 
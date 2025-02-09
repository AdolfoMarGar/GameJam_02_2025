using UnityEngine;
using System.Collections;

public class AudioSequence : MonoBehaviour
{
    public AudioClip firstClip;  // Primer audio
    public AudioClip secondClip; // Segundo audio que se repetirá en loop

    private AudioSource audioSource;

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        StartCoroutine(PlayAudioSequence());
    }

    IEnumerator PlayAudioSequence()
    {
        // Reproducir el primer audio
        audioSource.clip = firstClip;
        audioSource.loop = false;
        audioSource.Play();

        // Esperar hasta que falten unos pocos milisegundos para que termine el primer audio
        yield return new WaitForSeconds(firstClip.length - 0.00f);

        // Iniciar el segundo audio sin interrupciones
        audioSource.clip = secondClip;
        audioSource.loop = true;
        audioSource.Play();
    }
}
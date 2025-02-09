using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip teclaCorrecta;
    public AudioClip teclaIncorrecta;
    public AudioClip music;

    public void PlayTeclaCorrecta()
    {
        AudioSource rightKeySource = GetComponent<AudioSource>();
        rightKeySource.clip = teclaCorrecta;
        rightKeySource.PlayOneShot(teclaCorrecta);
    }

    public void PlayTeclaIncorrecta()
    {
        AudioSource wrongKeySource = GetComponent<AudioSource>();
        wrongKeySource.clip = teclaIncorrecta;
        wrongKeySource.PlayOneShot(teclaIncorrecta);
    }

    public void PlayMusic()
    {
        AudioSource musicSource = GetComponent<AudioSource>();
        musicSource.clip = music;
        // Asegúrate de que la música tenga loop activado
        musicSource.loop = true;
        
        // Iniciar la música si no está sonando
        if (!musicSource.isPlaying)
        {
            musicSource.Play();
        }
    }
}

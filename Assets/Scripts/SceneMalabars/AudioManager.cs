using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip teclaCorrecta;

    public void PlayTeclaCorrecta()
    {
        AudioSource audio = GetComponent<AudioSource>();
        audio.clip = teclaCorrecta;
        audio.Play();
    }
}

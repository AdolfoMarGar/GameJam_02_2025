using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource teclaCorrecta;

    public void PlayTeclaCorrecta()
    {
        teclaCorrecta.Play();
    }
}

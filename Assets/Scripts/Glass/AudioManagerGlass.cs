using UnityEngine;

public class AudioManagerGlass : MonoBehaviour
{
    private AudioSource audioSource; // El componente AudioSource del objeto
    public AudioClip audioClip; // El clip de audio a reproducir
    private bool playSound = false;

    void Start()
    {
        // Obtener el componente AudioSource en el mismo objeto
        audioSource = GetComponent<AudioSource>();

        if (audioSource == null)
        {
            Debug.LogError("⚠️ No se encontró el componente AudioSource en este objeto.");
        }

        if (audioClip == null)
        {
            Debug.LogError("⚠️ No se ha asignado ningún AudioClip.");
        }
    }

    void Update()
    {
        // Verificar si se presiona la tecla espacio
        if (playSound)
        {
            // Reproducir el clip de audio cuando se presiona espacio
            PlayAudioClip();
            playSound = false;
        }
    }
    public void setPlaySound(bool sonar)
    {
        playSound = sonar;
    }

    void PlayAudioClip()
    {
        // Reproducir el audio
        if (audioSource != null && audioClip != null)
        {
            audioSource.PlayOneShot(audioClip);
        }
    }
}

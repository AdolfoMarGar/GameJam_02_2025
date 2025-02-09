using UnityEngine;

public class AudioManagerFire : MonoBehaviour
{
    private AudioSource audioSource; // El componente AudioSource del objeto
    public AudioClip audioClip; // El clip de audio a reproducir

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
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Reproducir el clip de audio cuando se presiona espacio
            PlayAudioClip();
        }
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

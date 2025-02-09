using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    [Header("Prefab con Animación")]
    public GameObject animationPrefab; // Prefab que se activará
    private GameObject spawnedObject; // Instancia del prefab

    [Header("Animación")]
    public string animationName; // Nombre de la animación a reproducir

    public void LoadScene()
    {
        if (animationPrefab != null)
        {
            // Instanciar el prefab
            spawnedObject = Instantiate(animationPrefab, Vector3.zero, Quaternion.identity);

            // Obtener el Animator del prefab
            Animator animator = spawnedObject.GetComponent<Animator>();

            if (animator != null)
            {
                // Reproducir la animación seleccionada
                animator.Play(animationName);

                // Obtener la duración de la animación y cambiar de escena al finalizar
                AnimationClip clip = GetAnimationClip(animator, animationName);
                if (clip != null)
                {
                    Destroy(spawnedObject, clip.length); // Opcional: destruir el objeto al terminar
                    Invoke("ChangeToNextScene", clip.length);
                }
                else
                {
                    Debug.LogWarning("No se encontró la animación en el Animator.");
                }
            }
            else
            {
                Debug.LogWarning("El prefab no tiene un Animator.");
            }
        }
        else
        {
            Debug.LogWarning("Prefab de animación no asignado.");
        }
    }

    void ChangeToNextScene()
    {
        SceneManager.LoadScene(1); // Cargar la siguiente escena
    }

    private AnimationClip GetAnimationClip(Animator animator, string clipName)
    {
        foreach (AnimationClip clip in animator.runtimeAnimatorController.animationClips)
        {
            if (clip.name == clipName)
            {
                return clip;
            }
        }
        return null;
    }
}
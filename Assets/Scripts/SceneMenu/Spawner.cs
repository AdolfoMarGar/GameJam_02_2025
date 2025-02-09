using UnityEngine;
using UnityEngine.SceneManagement;

public class Spawner : MonoBehaviour
{
    [Header("Prefab a Instanciar")]
    public GameObject prefab; // Prefab que se instanciará

    [Header("Posición del Spawn")]
    public Transform spawnPoint; // Punto donde aparecerá el objeto

    [Header("Animación")]
    public string animationName; // Nombre de la animación a reproducir (seleccionable en el inspector)

    [Header("Tiempo antes de cambiar de escena")]
    public float timeToChangeScene = 3f; // Tiempo antes de cambiar de escena

    [Header("Índice de la Siguiente Escena")]
    public int nextSceneIndex = 1; // Índice de la siguiente escena

    public void SpawnObject()
    {
        if (prefab == null)
        {
            Debug.LogError("❌ Error: No has asignado un Prefab en el Inspector.");
            return;
        }
        if (spawnPoint == null)
        {
            Debug.LogError("❌ Error: No has asignado un Spawn Point en el Inspector.");
            return;
        }

        // Instanciar el prefab en la posición definida
        GameObject newObject = Instantiate(prefab, spawnPoint.position, Quaternion.identity);

        // Obtener el Animator del objeto instanciado
        Animator animator = newObject.GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogError("❌ Error: El prefab no tiene un componente Animator.");
            return;
        }

        // Verificar si el Animator tiene un controlador asignado
        if (animator.runtimeAnimatorController == null)
        {
            Debug.LogError("❌ Error: El Animator no tiene asignado un Animator Controller.");
            return;
        }

        // Verificar si la animación ingresada en el Inspector existe en el Animator Controller
        AnimationClip clip = GetAnimationClip(animator, animationName);
        if (clip != null)
        {
            animator.enabled = true; // Asegurar que el Animator está activado
            animator.Play(animationName); // Reproducir la animación seleccionada
            Debug.Log($"✅ Reproduciendo animación: {animationName}");
        }
        else
        {
            Debug.LogError($"❌ Error: La animación '{animationName}' no fue encontrada en el Animator Controller.");
            return;
        }

        // Reproducir sonido si el prefab tiene un AudioSource
        AudioSource audio = newObject.GetComponent<AudioSource>();
        if (audio != null)
        {
            audio.Play();
        }
        else
        {
            Debug.LogWarning("⚠️ Advertencia: No se encontró AudioSource en el prefab.");
        }

        // Invocar el cambio de escena después de la duración de la animación
        Invoke("ChangeToNextScene", clip.length);
    }

    void ChangeToNextScene()
    {
        SceneManager.LoadScene(nextSceneIndex); // Cambiar a la escena indicada
    }

    // Método para obtener la animación desde el Animator Controller
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

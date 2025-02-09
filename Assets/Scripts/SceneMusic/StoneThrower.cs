using System.Collections;
using UnityEngine;

public class StoneThrower : MonoBehaviour
{
    public GameObject animationPrefab; // Prefab con la animación
    public string animationName; // Nombre de la animación a ejecutar
    public float animationLifetime = 3f; // Tiempo antes de eliminar el prefab de animación
    public Transform animationStartPoint; // Punto de inicio del prefab de animación
    public GameObject stonePrefab; // Prefab de la piedra a lanzar
    public Transform startPoint; // Punto de inicio del lanzamiento de la piedra
    public Transform[] targetPoints = new Transform[4]; // 4 puntos de llegada
    public float speed = 5f; // Velocidad de la piedra
    private GameObject currentAnimationInstance;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            TriggerAnimationAndThrow(0);
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            TriggerAnimationAndThrow(1);
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            TriggerAnimationAndThrow(2);
        }
        else if (Input.GetKeyDown(KeyCode.F))
        {
            TriggerAnimationAndThrow(3);
        }
    }

    void TriggerAnimationAndThrow(int targetIndex)
    {
        if (animationPrefab != null && animationStartPoint != null)
        {
            if (currentAnimationInstance != null)
            {
                Destroy(currentAnimationInstance); // Elimina la animación anterior si existe
            }
            
            currentAnimationInstance = Instantiate(animationPrefab, animationStartPoint.position, Quaternion.identity);
            Animator anim = currentAnimationInstance.GetComponent<Animator>();
            if (anim != null && !string.IsNullOrEmpty(animationName))
            {
                anim.Play(animationName, 0, 0f); // Fuerza la animación
            }
            StartCoroutine(DestroyAnimationAfterTime());
        }
        
        if (targetPoints[targetIndex] != null)
        {
            StartCoroutine(ThrowStone(targetPoints[targetIndex]));
        }
    }

    IEnumerator DestroyAnimationAfterTime()
    {
        yield return new WaitForSeconds(animationLifetime);
        if (currentAnimationInstance != null)
        {
            Destroy(currentAnimationInstance);
        }
    }

    IEnumerator ThrowStone(Transform target)
    {
        GameObject stone = Instantiate(stonePrefab, startPoint.position, Quaternion.identity);
        float elapsedTime = 0f;
        float randomRotationSpeed = Random.Range(50f, 150f); // Rotación aleatoria
        Vector3 randomRotationAxis = Random.insideUnitSphere; // Eje de rotación aleatorio
        
        while (Vector3.Distance(stone.transform.position, target.position) > 0.1f)
        {
            stone.transform.position = Vector3.MoveTowards(stone.transform.position, target.position, speed * Time.deltaTime);
            stone.transform.Rotate(randomRotationAxis, randomRotationSpeed * Time.deltaTime);
            elapsedTime += Time.deltaTime;
            
            if (elapsedTime >= animationLifetime)
            {
                Destroy(stone);
                yield break;
            }
            
            yield return null;
        }
        Destroy(stone);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DecreasingBar : MonoBehaviour
{
    public GameObject timeBarObject;
    public float time;
    public MalabarGameManager malabarGameManager;
    public bool answerState = false;
    public IEnumerator DecreaseBar(float animationTime){

        Vector3 originalScale = timeBarObject.transform.localScale;
        float startTime = Time.time;
        float initialScaleY = timeBarObject.transform.localScale.y;

        while (Time.time - startTime < animationTime)
        {
            answerState = malabarGameManager.getCorrectOrWrongAnswer();
            if (answerState)
            {
                // Restauramos la barra a su escala original
                Debug.Log("Restauramos la barra a su escala original");
                timeBarObject.transform.localScale = originalScale;
                yield break; // Salimos inmediatamente de la corrutina
            }

            float elapsed = Time.time - startTime;
            float fraction = elapsed / animationTime;

            // Disminuir desde el tamaño original hasta 0 en "animationTime" segundos
            float newScaleY = Mathf.Lerp(initialScaleY, 0f, fraction);

            // Ajustar escala sin variar X ni Z
            timeBarObject.transform.localScale = new Vector3(
                timeBarObject.transform.localScale.x,
                newScaleY,
                timeBarObject.transform.localScale.z
            );

            // Esperar al siguiente frame
            yield return null;
        }

        // Al acabar, te aseguras de dejarla exactamente en 0
        timeBarObject.transform.localScale = originalScale;
    }

}

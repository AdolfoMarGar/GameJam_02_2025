using UnityEngine;
using System.Collections;


public class ProgressBarController : MonoBehaviour
{
    private bool disminuirEscala = true;

    void Start()
    {
        StartCoroutine(DisminuirEscalaCadaIntervalo());
    }

    void Update()
    {
        if (transform.localScale.y >= 2.55f)
        {
            disminuirEscala = false;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            IncrementarEscala();
        }
    }

    public bool GetDisminuirEscala()
    {
        return disminuirEscala;
    }
    void IncrementarEscala()
    {
        Vector3 nuevaEscala = transform.localScale;

        nuevaEscala.y += 0.055f;

        transform.localScale = nuevaEscala;
    }

    IEnumerator DisminuirEscalaCadaIntervalo()
    {
        while (true)
        {
            if (!disminuirEscala)
                break;
            yield return new WaitForSeconds(0.1f);

            if (transform.localScale.y > 0)

            {
                Vector3 nuevaEscala = transform.localScale;
                nuevaEscala.y -= 0.01f;
                transform.localScale = nuevaEscala;
            }
        }
    }
}

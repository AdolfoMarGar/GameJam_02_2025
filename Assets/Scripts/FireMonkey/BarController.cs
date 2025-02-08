using UnityEngine;
using System.Collections;


public class BarController : MonoBehaviour
{
    private bool disminuirEscala = true; // Controla cuándo empezar a disminuir la escala

    void Start()
    {
        // Iniciar la corrutina
        StartCoroutine(DisminuirEscalaCadaIntervalo());
    }

    void Update()
    {
        if (transform.localScale.y >= 2.55f)
        {
            disminuirEscala = false;
        }
        // Si se presiona la barra espaciadora, aumentar la escala
        if (Input.GetKeyDown(KeyCode.Space))
        {
            IncrementarEscala();
        }
    }

    void IncrementarEscala()
    {
        Vector3 nuevaEscala = transform.localScale;

        nuevaEscala.y += 0.04f; // Incrementar la escala en Y

        transform.localScale = nuevaEscala; // Aplicar la nueva escala al objeto
    }

    // Corrutina que disminuye la escala cada 0.5 segundos
    IEnumerator DisminuirEscalaCadaIntervalo()
    {
        while (true) // Esto hará que la corrutina se repita constantemente
        {
            if (!disminuirEscala)
                break;
            // Esperar 0.5 segundos
            yield return new WaitForSeconds(0.1f);

            // Disminuir la escala solo si no es menor que 0
            if (transform.localScale.y > 0)

            {
                Vector3 nuevaEscala = transform.localScale;
                nuevaEscala.y -= 0.01f; // Reducir la escala en Y
                transform.localScale = nuevaEscala; // Aplicar la nueva escala
            }
        }
    }
}

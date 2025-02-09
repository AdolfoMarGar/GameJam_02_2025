using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ProgressBarController : MonoBehaviour
{
    private bool disminuirEscala = true;
    public List<Sprite> listaDeSprites;  // Lista de sprites que cambiarán en bucle
    private int indiceSprite = 0;  // Para hacer el seguimiento del sprite actual
    private SpriteRenderer spriteRenderer;

    public GameObject bg; // Objeto al cual se le cambiará el sprite (así lo asignas en el Inspector)

    void Start()
    {
        spriteRenderer = bg.GetComponent<SpriteRenderer>(); // Obtener el SpriteRenderer del otro objeto
        StartCoroutine(DisminuirEscalaCadaIntervalo());
    }

    public void Reset()
    {
        disminuirEscala = true;
        Vector3 resetEscala = transform.localScale;
        resetEscala.y = 0.1f;
        transform.localScale = resetEscala;
    }
    void Update()
    {
        if (transform.localScale.y >= 2.55f)
        {
            disminuirEscala = false;
        }

        // Si se presiona la barra espaciadora, cambiar al siguiente sprite
        if (Input.GetKeyDown(KeyCode.Space))
        {
            IncrementarEscala();
            CambiarSprite();  // Cambiar el sprite en el otro objeto
        }
    }

    public bool GetDisminuirEscala()
    {
        return disminuirEscala;
    }

    void IncrementarEscala()
    {
        Vector3 nuevaEscala = transform.localScale;
        nuevaEscala.y += 0.1f;
        transform.localScale = nuevaEscala;
    }

    void CambiarSprite()
    {
        if (spriteRenderer != null && listaDeSprites.Count > 0)
        {
            indiceSprite = (indiceSprite + 1) % listaDeSprites.Count;
            spriteRenderer.sprite = listaDeSprites[indiceSprite];
        }
    }

    IEnumerator DisminuirEscalaCadaIntervalo()
    {
        while (true)
        {
            if (!disminuirEscala)
                break;
            yield return new WaitForSeconds(0.1f);

            if (transform.localScale.y > 0.55f)
            {
                Vector3 nuevaEscala = transform.localScale;
                nuevaEscala.y -= 0.01f;
                transform.localScale = nuevaEscala;
            }
        }
    }
}

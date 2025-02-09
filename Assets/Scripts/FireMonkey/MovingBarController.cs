using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingBarController : MonoBehaviour
{
    public float velocidad = 2.5f;
    private Rigidbody2D rb;
    private bool dentroDeScope = false; // Nueva variable para saber si está dentro del trigger
    public int aciertos = 0;
    private bool errores = false;
    public List<Sprite> listaDeSprites;  // Lista de sprites que cambiarán en bucle
    private int indiceSprite = 0;  // Para hacer el seguimiento del sprite actual
    private SpriteRenderer spriteRenderer;
    public GameObject bg; // Objeto al cual se le cambiará el sprite (así lo asignas en el Inspector)


    void Start()
    {
        spriteRenderer = bg.GetComponent<SpriteRenderer>(); // Obtener el SpriteRenderer del otro objeto
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        rb.velocity = new Vector2(rb.velocity.x, velocidad);
        // Verificar si está dentro del Scope y si se presiona la barra espaciadora
        if (dentroDeScope && Input.GetKeyDown(KeyCode.Space))
        {
            aciertos += 1;
            //Debug.Log("¡Presioné espacio dentro de Scope!");
            CambiarSprite();
            CambiarEscalaSegunAciertos();
        }
        else if (!dentroDeScope && Input.GetKeyDown(KeyCode.Space))
        {
            ProcesoFallo();

        }
    }

    private void ProcesoFallo()
    {
        SetAciertos(0);
        CambiarEscalaSegunAciertos();
        SetErrores(true);
    }

    void CambiarSprite()
    {
        StartCoroutine(CambiarSpriteEnBucle());
    }

    IEnumerator CambiarSpriteEnBucle()
    {
        for (int i = 0; i < 3; i++)  // Cambiar tres veces
        {
            indiceSprite = (indiceSprite + 1) % listaDeSprites.Count;  // Mover al siguiente sprite en la lista de manera cíclica
            spriteRenderer.sprite = listaDeSprites[indiceSprite];  // Asignar el nuevo sprite
            yield return new WaitForSeconds(0.1f);  // Esperar 0.3 segundos antes de cambiar de nuevo
        }
    }


    public void SetAciertos(int num)
    {
        aciertos = num;
    }
    public int GetAciertos()
    {
        return aciertos;
    }

    public bool GetErrores()
    {
        return errores;
    }

    public void SetErrores(bool error)
    {
        errores = error;
    }

    void CambiarEscalaSegunAciertos()
    {
        Vector3 nuevaEscala = transform.localScale;

        // Cambiar la escala según los aciertos
        if (aciertos == 0)
        {
            nuevaEscala.y = 0.8f;  // Cambiar a escala Y 1.5x
            velocidad = -2.5f;
        }
        else if (aciertos == 1)
        {
            nuevaEscala.y = 0.6f;  // Cambiar a escala Y 1.5x
            velocidad = -3f;
        }
        else if (aciertos == 2)
        {
            nuevaEscala.y = 0.35f;  // Cambiar a escala Y 2.0x
            velocidad = -3.5f;
        }
        else if (aciertos == 3)
        {
            nuevaEscala.y = 0.20f;  // Cambiar a escala Y 2.0x
            velocidad = -4f;
        }

        transform.localScale = nuevaEscala; // Aplicar la nueva escala al objeto
        transform.position = new Vector3(transform.position.x, 0.8f, transform.position.z); // Establecer la posición Y
    }

    void OnTriggerEnter2D(Collider2D otro)
    {
        if (otro.gameObject.name == "TopSide" || otro.gameObject.name == "BottomSide")
        {
            velocidad = velocidad * -1; // Cambiar dirección al tocar Top o Bottom
        }

        if (otro.gameObject.name == "Scope")
        {
            dentroDeScope = true; // Marcar que estamos dentro de Scope
        }
    }

    void OnTriggerExit2D(Collider2D otro)
    {
        if (otro.gameObject.name == "Scope")
        {
            dentroDeScope = false; // Marcar que salimos de Scope
        }
    }
}

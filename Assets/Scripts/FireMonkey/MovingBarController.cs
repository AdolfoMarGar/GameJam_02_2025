using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingBarController : MonoBehaviour
{
    public float velocidad = 2.5f;
    private Rigidbody2D rb;
    private bool dentroDeScope = false; // Nueva variable para saber si está dentro del trigger
    private int aciertos = 0;
    private int errores = 0;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        rb.velocity = new Vector2(rb.velocity.x, velocidad);

        // Verificar si está en Scope y si se presiona la barra espaciadora
        if (dentroDeScope && Input.GetKeyDown(KeyCode.Space))
        {
            aciertos += 1;
            Debug.Log("¡Presioné espacio dentro de Scope!");
            CambiarEscalaSegunAciertos();
        }
        else
        {
            errores += 1;
        }
    }
    public int GetAciertos()
    {
        return aciertos;
    }
    public int GetErrores()
    {
        return errores;
    }

    void CambiarEscalaSegunAciertos()
    {
        Vector3 nuevaEscala = transform.localScale;

        // Cambiar la escala según los aciertos
        if (aciertos == 1)
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
        transform.position = new Vector3(transform.position.x, 2.2f, transform.position.z); // Establecer la posición Y

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

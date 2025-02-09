using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class BarsController : MonoBehaviour
{
    public ProgressBarController progressBarController; // Asigna este objeto en el Inspector
    public MovingBarController movingBarController; // Asigna este objeto en el Inspector
    public GameObject progressBar;
    public GameObject scope;
    public GameObject movingBar;
    public GameObject baseBar;
    private bool cambioHecho = false;
    private SpriteRenderer spriteRenderer;
    public GameObject bg;
    public Sprite errorSprite;
    public Sprite startAgainSprite;
    public List<Sprite> listaDeSprites;  // Lista de sprites que cambiarán en bucle
    private int indiceSprite = 0;
    private bool animFinal = false;
    public GameObject introPrefab; // Prefab que se spawnea con la animación
    public GameObject outroPrefab; // Prefab que se spawnea con la animación
    public Transform spawnPoint; // Punto donde aparece el prefab (arrástralo en el editor)
    private GameObject spawnedIntro; // Referencia al prefab instanciado

    void Start()
    {
        if (introPrefab != null && spawnPoint != null)
        {
            spawnedIntro = Instantiate(introPrefab, spawnPoint.position, Quaternion.identity);
        }
        spriteRenderer = bg.GetComponent<SpriteRenderer>(); // Obtener el SpriteRenderer del otro objeto
        progressBar.SetActive(true); // Asegurarse de que progressBar esté activo al principio
    }

    void Update()
    {
        // Si no se ha hecho el cambio y la escala ha dejado de disminuir, hacer el cambio
        if (!cambioHecho && progressBarController.GetDisminuirEscala() == false)
        {
            cambioHecho = true;
            StartCoroutine(ProgressToMoving());
        }

        // Si los aciertos del MovingBarController son mayores o iguales a 4, desactivar movingBar y scope
        if (movingBarController.GetAciertos() >= 4)
        {
            movingBar.SetActive(false);
            scope.SetActive(false);
            baseBar.SetActive(false);
            if (animFinal == false)
            {
                StartCoroutine(AnimVictoria());
                animFinal = true;

            }
        }
        // Si ocurre un error, activar el reset
        if (movingBarController.GetErrores() == true) // Asumiendo que GetErrores devuelve un entero
        {
            Debug.Log("Entra dentro del if needresetTrue");

            StartCoroutine(Reset());
        }
    }

    IEnumerator AnimVictoria()
    {

        for (int i = 0; i < 18; i++)  // Cambiar tres veces
        {
            indiceSprite = (indiceSprite + 1) % listaDeSprites.Count;  // Mover al siguiente sprite en la lista de manera cíclica
            spriteRenderer.sprite = listaDeSprites[indiceSprite];  // Asignar el nuevo sprite
            yield return new WaitForSeconds(0.3f);  // Esperar 0.3 segundos antes de cambiar de nuevo
        }
        if (outroPrefab != null && spawnPoint != null)
        {
            spawnedIntro = Instantiate(outroPrefab, spawnPoint.position, Quaternion.identity);
        }

    }

    // Corrutina para el reset
    IEnumerator Reset()
    {
        movingBarController.SetErrores(false); // Resetear los errores
        movingBar.SetActive(false); // Desactivar la barra de movimiento
        scope.SetActive(false); // Desactivar el scope
        spriteRenderer.sprite = errorSprite;  // Asignar el nuevo sprite
        yield return new WaitForSeconds(1f);  // Esperar 0.3 segundos antes de cambiar de nuevo
        spriteRenderer.sprite = startAgainSprite;  // Asignar el nuevo sprite
        progressBar.SetActive(true); // Reactivar la barra de progreso
        cambioHecho = false; // Reiniciar el estado de cambio        
    }



    // Corrutina para cambiar de progressBar a movingBar después de un breve retraso
    IEnumerator ProgressToMoving()
    {
        Debug.Log("A empezar de nuevo!!!!!!!!");
        yield return new WaitForSeconds(0.1f);

        progressBarController.Reset();
        progressBar.SetActive(false);
        scope.SetActive(true);
        yield return new WaitForSeconds(0.6f);  // Esperar 1 segundo antes de activar movingBar y scope
        movingBar.SetActive(true);
    }
}

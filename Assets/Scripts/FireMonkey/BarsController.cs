using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarsController : MonoBehaviour
{
    public ProgressBarController progressBarController; // Asigna este objeto en el Inspector
    public MovingBarController movingBarController; // Asigna este objeto en el Inspector
    public GameObject progressBar;
    public GameObject scope;
    public GameObject movingBar;
    private bool cambioHecho = false;

    void Start()
    {
        progressBar.SetActive(true);
    }

    void Update()
    {
        if (!cambioHecho && progressBarController.GetDisminuirEscala() == false)
        {
            cambioHecho = true;
            StartCoroutine(ProgressToMoving());  // 🔹 Llamar a la corrutina correctamente
        }

        if (movingBarController.GetAciertos() >= 4)
        {
            movingBar.SetActive(false);
            scope.SetActive(false);
        }
    }

    IEnumerator ProgressToMoving()
    {
        progressBar.SetActive(false);
        yield return new WaitForSeconds(1f);  // ⏳ Esperar 1 segundo antes de activar movingBar y scope
        movingBar.SetActive(true);
        scope.SetActive(true);
    }
}

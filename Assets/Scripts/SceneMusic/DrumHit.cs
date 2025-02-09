using UnityEngine;
using TMPro; // Asegúrate de importar este espacio de nombres para trabajar con TMP_Text

public class DrumHit : MonoBehaviour
{
    public KeyCode hitKey; // Tecla asignada (ej: A, S, D, F)
    [SerializeField] private int debugTotalScore; // Variable para mostrar en el Inspector
    [SerializeField] private int debugTotalMisses; // Variable para mostrar en el Inspector
    public static int totalScore = 0; // Variable global para los hits
    public static int totalMisses = 0; // Variable global para los misses

    // Usamos TMP_Text en lugar de Text
    public TMP_Text scoreText; // Texto para mostrar los aciertos
    public TMP_Text missesText; // Texto para mostrar los fallos

    void Update()
    {
        // Sincronizar con las variables estáticas
        debugTotalScore = totalScore;
        debugTotalMisses = totalMisses;

        // Actualizar solo los números en el Canvas (sin texto adicional)
        if (scoreText != null)
        {
            scoreText.text = totalScore.ToString(); // Solo el número de aciertos
        }
        if (missesText != null)
        {
            missesText.text = totalMisses.ToString(); // Solo el número de fallos
        }

        // Verificar si se presionó la tecla asignada
        if (Input.GetKeyDown(hitKey))
        {
            CheckForHit();
        }
    }

    void CheckForHit()
    {
        // Detectar las notas dentro de un radio de 0.5f alrededor del objeto
        Collider2D[] notes = Physics2D.OverlapCircleAll(transform.position, 0.5f);
        foreach (var note in notes)
        {
            // Verificar si el objeto tiene la etiqueta "Note"
            if (note.CompareTag("Note"))
            {
                totalScore++; // Incrementar los aciertos
                debugTotalScore = totalScore;
                Destroy(note.gameObject); // Eliminar la nota
                return;
            }
        }
        totalMisses++; // Incrementar los fallos
        debugTotalMisses = totalMisses;
    }
}

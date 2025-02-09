using UnityEngine;

public class DrumHit : MonoBehaviour
{
    public KeyCode hitKey; // Tecla asignada (ej: A, S, D, F)
    [SerializeField] private int debugTotalScore; // Variable para mostrar en el Inspector
    [SerializeField] private int debugTotalMisses; // Variable para mostrar en el Inspector
    public static int totalScore = 0; // Variable global para los hits
    public static int totalMisses = 0; // Variable global para los misses

    void Update()
    {
        debugTotalScore = totalScore; // Sincronizar con la variable estática
        debugTotalMisses = totalMisses; // Sincronizar con la variable estática

        if (Input.GetKeyDown(hitKey))
        {
            CheckForHit();
        }
    }

    void CheckForHit()
    {
        Collider2D[] notes = Physics2D.OverlapCircleAll(transform.position, 0.5f);
        foreach (var note in notes)
        {
            if (note.CompareTag("Note"))
            {
                totalScore++;
                debugTotalScore = totalScore;
                Destroy(note.gameObject);
                return;
            }
        }
        totalMisses++;
        debugTotalMisses = totalMisses;
    }
}

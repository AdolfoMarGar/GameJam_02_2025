using UnityEngine;

public class DrumHit : MonoBehaviour
{
    public KeyCode hitKey; // Tecla asignada (ej: A, S, D, F)
    public int score = 0;
    public int misses = 0;

    void Update()
    {
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
                score++;
                Destroy(note.gameObject);
                return;
            }
        }
        misses++;
    }
}
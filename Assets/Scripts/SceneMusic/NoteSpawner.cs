using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Interaction;
using System.IO;

public class NoteSpawner : MonoBehaviour
{
    public AudioSource musicSource;
    public GameObject notePrefab;
    public Transform[] spawnPoints; // 4 posiciones para los tambores
    public TextAsset midiFile; // Arrastra el archivo MIDI aquí en el Inspector
    public float noteSpeed = 5f;
    public float preSpawnTime = 1f; // Tiempo antes de que la nota deba ser golpeada

    private List<float> noteTimings = new List<float>();
    private int index = 0;

    void Start()
    {
        LoadMidiFile();
        musicSource.Play();
        StartCoroutine(SpawnNotes());
    }

    void LoadMidiFile()
    {
        if (midiFile == null)
        {
            Debug.LogError("❌ No se ha asignado ningún archivo MIDI en el Inspector.");
            return;
        }

        Debug.Log("✅ Cargando archivo MIDI: " + midiFile.name);

        using (var stream = new MemoryStream(midiFile.bytes))
        {
            var midiData = MidiFile.Read(stream);
            var tempoMap = midiData.GetTempoMap();
            var notes = midiData.GetNotes();

            foreach (var note in notes)
            {
                var time = TimeConverter.ConvertTo<MetricTimeSpan>(note.Time, tempoMap);
                float seconds = (float)time.TotalSeconds;
                noteTimings.Add(seconds);
            }
            noteTimings.Sort();
        }
    }

    IEnumerator SpawnNotes()
    {
        while (index < noteTimings.Count)
        {
            float spawnTime = noteTimings[index] - preSpawnTime;
            float timeToNext = spawnTime - musicSource.time;
            if (timeToNext > 0)
                yield return new WaitForSeconds(timeToNext);

            SpawnNote(Random.Range(0, spawnPoints.Length));
            index++;
        }
    }

    void SpawnNote(int lane)
    {
        GameObject note = Instantiate(notePrefab, spawnPoints[lane].position, Quaternion.identity);
        Rigidbody2D rb = note.GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.down * noteSpeed;
    }
}

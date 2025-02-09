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
    public float musicDelay = 0f; // Delay antes de que la música comience
    public float midiLoadDelay = 0f; // Delay antes de que el MIDI se cargue y empiecen a spawnear las notas

    private List<float> noteTimings = new List<float>();
    private int index = 0;

    void Start()
    {
        StartCoroutine(StartMusicAndMidi());
    }

    IEnumerator StartMusicAndMidi()
    {
        yield return new WaitForSeconds(musicDelay); // Espera antes de iniciar la música
        if (musicSource != null)
        {
            musicSource.Play();
        }

        yield return new WaitForSeconds(midiLoadDelay); // Espera antes de cargar el MIDI y spawnear notas
        LoadMidiFile();
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
            float timeToNext = spawnTime - (musicSource != null ? musicSource.time : 0f);
            if (timeToNext > 0)
                yield return new WaitForSeconds(timeToNext);

            SpawnNote(Random.Range(0, spawnPoints.Length));
            index++;
        }
    }

    void SpawnNote(int lane)
    {
        if (notePrefab != null && spawnPoints.Length > 0)
        {
            GameObject note = Instantiate(notePrefab, spawnPoints[lane].position, Quaternion.identity);
            Rigidbody2D rb = note.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.velocity = Vector2.down * noteSpeed;
            }
        }
    }
}

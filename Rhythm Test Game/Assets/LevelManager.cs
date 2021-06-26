using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

[System.Serializable]
public class NoteData
{
    public int Beat;
    public Notes Notes;
}
[System.Serializable]
public struct Notes
{
    public bool upNote;
    public bool downNote;
    public bool leftNote;
    public bool rightNote;
}
public class LevelManager : MonoBehaviour
{

    [SerializeField] private float BPM;
    [SerializeField] private float fallSpeed;
    [SerializeField] private NoteData[] notes;

    public static float FallSpeed { get; private set; }
    public static float Delay { get; private set; }

    private readonly SortedDictionary<int, Notes> LevelDataContainer = new SortedDictionary<int, Notes>();

    public static List<float> LevelDataSeconds { get; private set; } = new List<float>();

    private float secondsPerBeat;
    private int[] keys;

    private readonly string path = "Assets/BeatDeviationLog.txt";
    private StreamWriter writer;

    private double cumulativeDeviation = 0;
    private double lastNoteDuration = 0;
    private double maxPosDeviation = 0;
    private double maxNegDeviation = 0;
    private int i = 0;

    private NoteSpawner noteSpawner;

    private void Awake()
    {
        FallSpeed = fallSpeed;
        writer = new StreamWriter(path, true);
        secondsPerBeat = 60 / BPM;

        Delay = NoteSpawner.SpawnOffset / fallSpeed;

        Debug.Log(Delay);

        foreach (NoteData note in notes)
        {
            LevelDataContainer.Add(note.Beat, note.Notes);
        }


        keys = LevelDataContainer.Keys.ToArray();

        foreach(var key in keys)
        {
            LevelDataSeconds.Add((key * secondsPerBeat) + Delay);
        }

        writer.WriteLine($"-----------------------------------------------------------------------");
        writer.WriteLine($"{DateTime.Now}");
        writer.WriteLine($"BPM: {BPM}; # of beats: {keys.Length}");
        writer.WriteLine($"Seconds per beat: {secondsPerBeat}");
        writer.WriteLine($"Estimated time: {secondsPerBeat * keys[keys.Length - 1]}s");
        writer.WriteLine($"-----------------------------------------------------------------------");
    }
    private void Start()
    {
        noteSpawner = NoteSpawner.Instance;
        Invoke(nameof(Step), keys[0] * secondsPerBeat);
    }

    private void InvokeStep()
    {
        if (i < keys.Length)
        {
            Invoke(nameof(Step), (keys[i] - keys[i - 1]) * secondsPerBeat);
        }
        else
        {
            writer.WriteLine($"-----------------------------------------------------------------------");
            writer.WriteLine($"Test complete after {Time.timeSinceLevelLoad} seconds; cumulative deviation: {cumulativeDeviation}s; duration - deviation: {Time.timeSinceLevelLoad - cumulativeDeviation}");
            writer.WriteLine($"maximum +deviation: {maxPosDeviation}");
            writer.WriteLine($"maximum -deviation: {maxNegDeviation}");
            writer.WriteLine($"-----------------------------------------------------------------------");
            writer.Close();

            AssetDatabase.ImportAsset(path);
            return;
        }
    }
    private void Step()
    {
        lastNoteDuration = Time.timeSinceLevelLoadAsDouble - lastNoteDuration;

        Debug.Log("Beat!");

        if (i > 0)
        {
            //Debug.Log($"{lastNoteDuration}, {(keys[i] - keys[i - 1]) * secondsPerBeat}");
            var currentDeviation = lastNoteDuration - ((keys[i] - keys[i - 1]) * secondsPerBeat);
            cumulativeDeviation += currentDeviation;

            writer.WriteLine($"{DateTime.Now} : {currentDeviation}");

            maxPosDeviation = currentDeviation > maxPosDeviation ? currentDeviation : maxPosDeviation;
            maxNegDeviation = currentDeviation < maxNegDeviation ? currentDeviation : maxNegDeviation;
        }

        NoteSpawner.Instance.Spawn(LevelDataContainer[keys[i]]);
        //Debug.Log("Note!");

        lastNoteDuration = Time.timeSinceLevelLoadAsDouble;
        i++;

        InvokeStep();
    }
}

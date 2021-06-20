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
public class LevelManager : MonoBehaviour
{

    [SerializeField] private float BPM;
    [SerializeField] private int beatsAmount;
    [SerializeField] private NoteData[] notes;
    
    private SortedDictionary<int, Notes> LevelDataContainer = new SortedDictionary<int, Notes>();

    private float secondsPerBeat;
    private int[] keys;

    private readonly string path = "Assets/BeatDeviationLog.txt";
    private StreamWriter writer;

    private double cumulativeDeviation = 0;
    private double lastNoteDuration = 0;
    private double maxPosDeviation = 0;
    private double maxNegDeviation = 0;
    private int i = 0;
    private void Awake()
    {

        writer = new StreamWriter(path, true);
        secondsPerBeat = 60 / BPM;

        writer.WriteLine($"-----------------------------------------------------------------------");
        writer.WriteLine($"{DateTime.Now}");
        writer.WriteLine($"BPM: {BPM}; # of beats: {beatsAmount}");
        writer.WriteLine($"Seconds per beat: {secondsPerBeat}");
        writer.WriteLine($"Estimated time: {secondsPerBeat * beatsAmount}s");
        writer.WriteLine($"-----------------------------------------------------------------------");

        foreach (NoteData note in notes)
        {
            LevelDataContainer.Add(note.Beat, note.Notes);
        }

        keys = LevelDataContainer.Keys.ToArray();
    }
    private void Start() => Invoke(nameof(Step), keys[0] * secondsPerBeat);
    private void InvokeStep()
    {
        if (i < keys.Length)
        {
            Invoke("Step", (keys[i] - keys[i - 1]) * secondsPerBeat);
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
        lastNoteDuration = Time.timeSinceLevelLoad - lastNoteDuration;

        if (i > 0)
        {
            var currentDeviation = lastNoteDuration - secondsPerBeat;
            cumulativeDeviation += currentDeviation;

            writer.WriteLine($"{DateTime.Now} : {currentDeviation}");

            maxPosDeviation = currentDeviation > maxPosDeviation ? currentDeviation : maxPosDeviation;
            maxNegDeviation = currentDeviation < maxNegDeviation ? currentDeviation : maxNegDeviation;
        }

        lastNoteDuration = Time.timeSinceLevelLoad;
        i++;

        InvokeStep();
    }
}

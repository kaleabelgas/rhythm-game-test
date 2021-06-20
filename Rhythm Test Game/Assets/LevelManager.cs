using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.IO;
using System;
using UnityEditor;

public class LevelManager : MonoBehaviour
{
    [System.Serializable]
    public class NoteData
    {
        public int Beat;
        public Notes Notes;
    }

    [SerializeField] private float BPM;
    [SerializeField] private int beatsAmount;
    [SerializeField] private NoteData[] notes;

    private float secondsPerBeat;
    private int[] keys;
    private LineRenderer lineRenderer;

    private SortedDictionary<int, Notes> LevelDataContainer = new SortedDictionary<int, Notes>();

    private int i = 0;

    private string path = "Assets/BeatDeviationLog.txt";

    private StreamWriter writer;

    private void Awake()
    {

        writer = new StreamWriter(path, true);
        secondsPerBeat = 60 / BPM;

        Debug.Log($"BPM: {BPM}; # of beats: {beatsAmount}\n" +
            $"Seconds per beat: {secondsPerBeat}\n" +
        $"Estimated time: {secondsPerBeat * beatsAmount}s");

        writer.WriteLine($"-----------------------------------------------------------------------");
        writer.WriteLine($"{DateTime.Now}");
        writer.WriteLine($"BPM: {BPM}; # of beats: {beatsAmount}");
        writer.WriteLine($"Seconds per beat: {secondsPerBeat}");
        writer.WriteLine($"Estimated time: {secondsPerBeat * beatsAmount}s");
        writer.WriteLine($"-----------------------------------------------------------------------");




        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = beatsAmount + 3;
        lineRenderer.startWidth = 0.2f;


        //foreach(NoteData note in notes)
        //{
        //    LevelDataContainer.Add(note.Beat, note.Notes);
        //}

        for (int i = 0; i < beatsAmount + 1; i++)
        {
            LevelDataContainer.Add(i, notes[0].Notes);
        }

        keys = LevelDataContainer.Keys.ToArray();
    }

    private void Start()
    {
        Invoke(nameof(Step), keys[0] * secondsPerBeat);

    }


    private void InvokeStep()
    {
        if (i >= keys.Length)
        {
            Debug.Log($"Test complete after {Time.timeSinceLevelLoad} seconds; cumulative deviation: {cumulativeDeviation}s; duration - deviation: {Time.timeSinceLevelLoad - cumulativeDeviation}\n" +
                $"maximum +deviation: {maxPosDeviation}\n" +
                $"maximum -deviation: {maxNegDeviation}");
        writer.WriteLine($"-----------------------------------------------------------------------");


            writer.WriteLine($"Test complete after {Time.timeSinceLevelLoad} seconds; cumulative deviation: {cumulativeDeviation}s; duration - deviation: {Time.timeSinceLevelLoad - cumulativeDeviation}");
            writer.WriteLine($"maximum +deviation: {maxPosDeviation}");
            writer.WriteLine($"maximum -deviation: {maxNegDeviation}");
            writer.WriteLine($"-----------------------------------------------------------------------");
            writer.Close();

            AssetDatabase.ImportAsset(path);



            lineRenderer.SetPosition(i, new Vector3(i * (60 / (float)beatsAmount), 0));
            lineRenderer.SetPosition(i + 1, new Vector3(0, 0));

            return;
        }

        Invoke("Step", (keys[i] - keys[i - 1]) * secondsPerBeat);
    }

    private double cumulativeDeviation = 0;
    private double lastNoteDuration = 0;
    //private float time = 0;

    private double maxPosDeviation = 0;
    private double maxNegDeviation = 0;

    private Vector3 dataPoint;

    private void Step()
    {
        //Debug.Log($"Note made at {Time.time * (1 /timePerBeat)}; {LevelDataContainer[keys[i]]}");


        lastNoteDuration = Time.timeSinceLevelLoad - lastNoteDuration;


        //Debug.Log($"Duration: {lastDuration}s");

        if (i > 0)
        {
            var currentDeviation = lastNoteDuration - secondsPerBeat;
            cumulativeDeviation += currentDeviation;

            //Debug.Log(60 /(float) notesAmount);
            dataPoint.x = i * (60 / (float)beatsAmount);
            dataPoint.y = (float)currentDeviation * 400;

            writer.WriteLine($"{DateTime.Now} : {currentDeviation}");


            lineRenderer.SetPosition(i, dataPoint);
            maxPosDeviation = currentDeviation > maxPosDeviation ? currentDeviation : maxPosDeviation;
            maxNegDeviation = currentDeviation < maxNegDeviation ? currentDeviation : maxNegDeviation;
        }

        lastNoteDuration = Time.timeSinceLevelLoad;

        //time = Time.time;
        i++;
        InvokeStep();
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

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

    private void Awake()
    {

        secondsPerBeat = 60 / BPM;

        Debug.Log($"BPM: {BPM}; # of beats: {beatsAmount}\n" +
            $"Seconds per beat: {secondsPerBeat}\n" +
        $"Estimated time: {secondsPerBeat * beatsAmount}s");

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
            Debug.Log($"Test complete after {Time.time} seconds; cumulative deviation: {cumulativeDeviation}s; duration - deviation: {Time.time - cumulativeDeviation}\n" +
                $"maximum +deviation: {maxPosDeviation}\n" +
                $"maximum -deviation: {maxNegDeviation}");


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


        lastNoteDuration = Time.time - lastNoteDuration;


        //Debug.Log($"Duration: {lastDuration}s");

        if (i > 0)
        {
            var currentDeviation = lastNoteDuration - secondsPerBeat;
            cumulativeDeviation += currentDeviation;

            //Debug.Log(60 /(float) notesAmount);
            dataPoint.x = i * (60 / (float)beatsAmount);
            dataPoint.y = (float)currentDeviation * 400;

            lineRenderer.SetPosition(i, dataPoint);
            maxPosDeviation = currentDeviation > maxPosDeviation ? currentDeviation : maxPosDeviation;
            maxNegDeviation = currentDeviation < maxNegDeviation ? currentDeviation : maxNegDeviation;
        }

        lastNoteDuration = Time.time;

        //time = Time.time;
        i++;
        InvokeStep();
    }
}

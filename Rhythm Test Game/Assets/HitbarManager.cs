using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
//using System.Linq;
using System;
using Utils;
using System.Linq;

public class HitbarManager : MonoBehaviour
{
    [Header("Score threshold properties")]
    [SerializeField] private float perfectThreshold = 0.1f;
    [SerializeField] private float goodThreshold = 0.3f;

    public static event Action<Score> OnButtonPressed;

    public void OnUp()
    {
        var note = NoteSpawner.ActiveNotes[0].FirstOrDefault();
        if (note == null) return;

        var closestNote = LevelManager.LevelDataSeconds.GetClosest(Time.time);
        var delay = Mathf.Abs(Time.time - closestNote);
        note.Hit();
        NoteSpawner.ActiveNotes[0].Remove(note);

        if (delay <= perfectThreshold) OnButtonPressed?.Invoke(Score.perfect);
        else if (delay < goodThreshold) OnButtonPressed?.Invoke(Score.good);
        else OnButtonPressed?.Invoke(Score.bad);
    }
    public void OnDown()
    {
        var note = NoteSpawner.ActiveNotes[1].FirstOrDefault();
        if (note == null) return;

        var closestNote = LevelManager.LevelDataSeconds.GetClosest(Time.time);
        var delay = Mathf.Abs(Time.time - closestNote);
        note.Hit();
        NoteSpawner.ActiveNotes[1].Remove(note);

        if (delay <= perfectThreshold) OnButtonPressed?.Invoke(Score.perfect);
        else if (delay < goodThreshold) OnButtonPressed?.Invoke(Score.good);
        else OnButtonPressed?.Invoke(Score.bad);
    }
    public void OnLeft()
    {
        var note = NoteSpawner.ActiveNotes[2].FirstOrDefault();
        if (note == null) return;

        var closestNote = LevelManager.LevelDataSeconds.GetClosest(Time.time);
        var delay = Mathf.Abs(Time.time - closestNote);
        note.Hit();
        NoteSpawner.ActiveNotes[2].Remove(note);

        if (delay <= perfectThreshold) OnButtonPressed?.Invoke(Score.perfect);
        else if (delay < goodThreshold) OnButtonPressed?.Invoke(Score.good);
        else OnButtonPressed?.Invoke(Score.bad);
    }
    public void OnRight()
    {
        var note = NoteSpawner.ActiveNotes[3].FirstOrDefault();
        if (note == null) return;

        var closestNote = LevelManager.LevelDataSeconds.GetClosest(Time.time);
        var delay = Mathf.Abs(Time.time - closestNote);
        note.Hit();
        NoteSpawner.ActiveNotes[3].Remove(note);

        if (delay <= perfectThreshold) OnButtonPressed?.Invoke(Score.perfect);
        else if (delay < goodThreshold) OnButtonPressed?.Invoke(Score.good);
        else OnButtonPressed?.Invoke(Score.bad);
    }
}

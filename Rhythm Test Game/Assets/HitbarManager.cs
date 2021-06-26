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
    [SerializeField] private float badThreshold = 1.0f;

    public static event Action<Score> OnButtonPressed;

    private GameManager gameManager;

    private void Start()
    {
        gameManager = GameManager.Instance;
    }

    public void OnUp()
    {
        var note = gameManager.GetClosestObject(ObjectLists.note, transform.position, out _, 100f);
        if (note == null) return;

        var closestNote = LevelManager.LevelDataSeconds.GetClosest(Time.time);
        var delay = Mathf.Abs(Time.time - closestNote);
        note.GetComponent<Note>().Hit();

        if (delay <= perfectThreshold) OnButtonPressed?.Invoke(Score.perfect);
        else if (delay < goodThreshold) OnButtonPressed?.Invoke(Score.good);
        else OnButtonPressed?.Invoke(Score.bad);
    }
    public void OnDown()
    {
        var note = gameManager.GetClosestObject(ObjectLists.note, transform.position, out _, 100f);
        if (note == null) return;

        var closestNote = LevelManager.LevelDataSeconds.GetClosest(Time.time);
        var delay = Mathf.Abs(Time.time - closestNote);
        note.GetComponent<Note>().Hit();

        if (delay <= perfectThreshold) OnButtonPressed?.Invoke(Score.perfect);
        else if (delay < goodThreshold) OnButtonPressed?.Invoke(Score.good);
        else OnButtonPressed?.Invoke(Score.bad);
    }
    public void OnLeft()
    {
        var note = gameManager.GetClosestObject(ObjectLists.note, transform.position, out _, 100f);
        if (note == null) return;

        var closestNote = LevelManager.LevelDataSeconds.GetClosest(Time.time);
        var delay = Mathf.Abs(Time.time - closestNote);
        note.GetComponent<Note>().Hit();

        if (delay <= perfectThreshold) OnButtonPressed?.Invoke(Score.perfect);
        else if (delay < goodThreshold) OnButtonPressed?.Invoke(Score.good);
        else OnButtonPressed?.Invoke(Score.bad);

    }
    public void OnRight()
    {
        var note = gameManager.GetClosestObject(ObjectLists.note, transform.position, out _, 100f);
        if (note == null) return;

        var closestNote = LevelManager.LevelDataSeconds.GetClosest(Time.time);
        var delay = Mathf.Abs(Time.time - closestNote);
        note.GetComponent<Note>().Hit();

        if (delay <= perfectThreshold) OnButtonPressed?.Invoke(Score.perfect);
        else if (delay < goodThreshold) OnButtonPressed?.Invoke(Score.good);
        else OnButtonPressed?.Invoke(Score.bad);
    }
}

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
    [Header("Bar properties")]
    [SerializeField] private float barSpeed = 1;
    [Header("Score threshold properties")]
    [SerializeField] private float missedThreshold = 1f;
    [SerializeField] private float badThreshold = 0.5f;
    [SerializeField] private float goodThreshold = 0.3f;
    [SerializeField] private float perfectThreshold = 0.1f;

    private const int barHeight = 12;

    public static event Action<Score> OnButtonPressed;

    private SortedDictionary<float, Notes> noteData;

    private List<float> noteTimePositions;

    private GameManager gameManager;

    private Vector3 position;

    private void Start()
    {
        gameManager = GameManager.Instance;
        noteData = LevelManager.LevelDataSeconds;

        noteTimePositions = noteData.Keys.ToList();

    }

    public void OnUp()
    {
        var note = gameManager.GetClosestObject(ObjectLists.up, transform.position, out _, 100f);

        if (note == null)
        {
            Debug.Log("Missed!");
            OnButtonPressed?.Invoke(Score.missed);
            return;
        }

        float closestNote = noteTimePositions.GetClosest(Time.time);
        var delay = Time.time - closestNote;

        note.GetComponent<Note>().Hit();
        if (delay > badThreshold) OnButtonPressed?.Invoke(Score.bad);
        else if (delay > perfectThreshold) OnButtonPressed?.Invoke(Score.good);
        else if (delay <= perfectThreshold) OnButtonPressed?.Invoke(Score.perfect);
    }
    public void OnDown()
    {
        var note = gameManager.GetClosestObject(ObjectLists.down, transform.position, out _, 100f);

        if (note == null)
        {
            Debug.Log("Missed!");
            OnButtonPressed?.Invoke(Score.missed);
            return;
        }

        float closestNote = noteTimePositions.GetClosest(Time.time);
        var delay = Time.time - closestNote;

        note.GetComponent<Note>().Hit();
        if (delay > badThreshold) OnButtonPressed?.Invoke(Score.bad);
        else if (delay > perfectThreshold) OnButtonPressed?.Invoke(Score.good);
        else if (delay <= perfectThreshold) OnButtonPressed?.Invoke(Score.perfect);
    }
    public void OnLeft()
    {
        var note = gameManager.GetClosestObject(ObjectLists.left, transform.position, out _, 100f);

        if (note == null)
        {
            Debug.Log("Missed!");
            OnButtonPressed?.Invoke(Score.missed);
            return;
        }

        float closestNote = noteTimePositions.GetClosest(Time.time);
        var delay = Time.time - closestNote;

        note.GetComponent<Note>().Hit();
        if (delay > badThreshold) OnButtonPressed?.Invoke(Score.bad);
        else if (delay > perfectThreshold) OnButtonPressed?.Invoke(Score.good);
        else if (delay <= perfectThreshold) OnButtonPressed?.Invoke(Score.perfect);

    }
    public void OnRight()
    {
        var note = gameManager.GetClosestObject(ObjectLists.right, transform.position, out _, 100f);

        if (note == null)
        {
            Debug.Log("Missed!");
            OnButtonPressed?.Invoke(Score.missed);
            return;
        }

        float closestNote = noteTimePositions.GetClosest(Time.time);
        var delay = Time.time - closestNote;

        note.GetComponent<Note>().Hit();
        if (delay > badThreshold) OnButtonPressed?.Invoke(Score.bad);
        else if (delay > perfectThreshold) OnButtonPressed?.Invoke(Score.good);
        else if (delay <= perfectThreshold) OnButtonPressed?.Invoke(Score.perfect);

    }
    private void Update()
    {
        var y = HelperUtils.TriangleWave(Time.time * barSpeed) * barHeight;
        position.x = transform.position.x;
        position.y = y;
        position.z = transform.position.z;
        transform.position = position;
    }
}

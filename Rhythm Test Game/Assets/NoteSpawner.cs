using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class NoteSpawner : MonoBehaviour
{
    public static float SpawnOffset { get; private set; } = 10;
    public static NoteSpawner Instance { get; private set; }
    private ObjectPooler objectPooler;

    public static List<Note>[] ActiveNotes { get; private set; } = new List<Note>[4];  

    private void Awake()
    {
        Instance = this;

        for (int i = 0; i < 4; i++)
        {
            ActiveNotes[i] = new List<Note>();
        }
    }

    private void Start()
    {
        objectPooler = ObjectPooler.Instance;
    }
    public void Spawn(Notes notes)
    {
        if (notes.upNote)
        {
            var noteObj = objectPooler.SpawnFromPool(ObjectLists.note, new Vector2(-2, SpawnOffset), Quaternion.identity);
            var note = noteObj.GetComponent<Note>();
            note.Index = 0;
            ActiveNotes[0].Add(note);
        }
        if (notes.downNote)
        {
            var noteObj = objectPooler.SpawnFromPool(ObjectLists.note, new Vector2(2, SpawnOffset), Quaternion.identity);
            var note = noteObj.GetComponent<Note>();
            note.Index = 1;
            ActiveNotes[1].Add(note);
        }
        if (notes.leftNote)
        {
            var noteObj = objectPooler.SpawnFromPool(ObjectLists.note, new Vector2(-6, SpawnOffset), Quaternion.identity);
            var note = noteObj.GetComponent<Note>();
            note.Index = 2;
            ActiveNotes[2].Add(note);
        }
        if (notes.rightNote)
        {
            var noteObj = objectPooler.SpawnFromPool(ObjectLists.note, new Vector2(6, SpawnOffset), Quaternion.identity);
            var note = noteObj.GetComponent<Note>();
            note.Index = 3;
            ActiveNotes[3].Add(note);
        }
    }
}

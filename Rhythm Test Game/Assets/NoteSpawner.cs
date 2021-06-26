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
            var note = objectPooler.SpawnFromPool(ObjectLists.note, new Vector2(-4, SpawnOffset), Quaternion.identity);
            ActiveNotes[0].Add(note.GetComponent<Note>());
        }
        if (notes.downNote)
        {
            var note = objectPooler.SpawnFromPool(ObjectLists.note, new Vector2(4, SpawnOffset), Quaternion.identity);
            ActiveNotes[1].Add(note.GetComponent<Note>());
        }
        if (notes.leftNote)
        {
            var note = objectPooler.SpawnFromPool(ObjectLists.note, new Vector2(-8, SpawnOffset), Quaternion.identity);
            ActiveNotes[2].Add(note.GetComponent<Note>());
        }
        if (notes.rightNote)
        {
            var note = objectPooler.SpawnFromPool(ObjectLists.note, new Vector2(8, SpawnOffset), Quaternion.identity);
            ActiveNotes[3].Add(note.GetComponent<Note>());
        }
    }
}

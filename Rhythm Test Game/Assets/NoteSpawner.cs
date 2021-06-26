using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class NoteSpawner : MonoBehaviour
{
    public static float SpawnOffset { get; private set; } = 10;
    public static NoteSpawner Instance { get; private set; }
    private ObjectPooler objectPooler;

    private void Awake()
    {
        Instance = this;
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
            GameManager.Instance.AddObjectToList(ObjectLists.note, note);
        }
        if (notes.downNote)
        {
            var note = objectPooler.SpawnFromPool(ObjectLists.note, new Vector2(4, SpawnOffset), Quaternion.identity);
            GameManager.Instance.AddObjectToList(ObjectLists.note, note);

        }
        if (notes.leftNote)
        {
            var note = objectPooler.SpawnFromPool(ObjectLists.note, new Vector2(-8, SpawnOffset), Quaternion.identity);
            GameManager.Instance.AddObjectToList(ObjectLists.note, note);

        }
        if (notes.rightNote)
        {
            var note = objectPooler.SpawnFromPool(ObjectLists.note, new Vector2(8, SpawnOffset), Quaternion.identity);
            GameManager.Instance.AddObjectToList(ObjectLists.note, note);

        }
    }
}

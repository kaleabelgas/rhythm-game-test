using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class NoteSpawner : MonoBehaviour
{
    [SerializeField] private float spawnOffset;
    public static NoteSpawner Instance { get; private set; }
    private ObjectPooler objectPooler;

    private Vector3 spawnLocation = Vector3.zero;

    private const int barHeight = 12;


    private float yPos;
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
            objectPooler.SpawnFromPool(ObjectLists.up, new Vector2(-4, 0), Quaternion.identity);
        }
        if (notes.downNote)
        {
            objectPooler.SpawnFromPool(ObjectLists.down, new Vector2(4, 5), Quaternion.identity);
        }
        if (notes.leftNote)
        {
            objectPooler.SpawnFromPool(ObjectLists.left, new Vector2(-8, 0), Quaternion.identity);
        }
        if (notes.rightNote)
        {
            objectPooler.SpawnFromPool(ObjectLists.right, new Vector2(8, 0), Quaternion.identity);
        }
    }

    private void Update()
    {
        yPos = HelperUtils.TriangleWave(Time.time) * barHeight;
    }
}

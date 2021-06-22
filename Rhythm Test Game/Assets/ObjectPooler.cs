using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    [System.Serializable]
    public class Pool
    {
        public ObjectLists type;
        public GameObject objectPrefab;
        public int poolSize;
    }

    public static ObjectPooler Instance { get; private set; }

    public Dictionary<ObjectLists, Queue<GameObject>> poolDictionary = new Dictionary<ObjectLists, Queue<GameObject>>();
    public List<Pool> pools;
    private void Awake()
    {
        Instance = this;

        foreach (Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < pool.poolSize; i++)
            {
                GameObject objectToPool = Instantiate(pool.objectPrefab, new Vector3(0, -100), Quaternion.identity);
                objectToPool.SetActive(false);
                objectPool.Enqueue(objectToPool);
            }

            poolDictionary.Add(pool.type, objectPool);
        }
    }


    public GameObject SpawnFromPool(ObjectLists type, Vector2 position, Quaternion rotation)
    {
        if (!poolDictionary.ContainsKey(type))
        {
            Debug.LogError("NO SUCH POOL EXISTS!" + type) ;
            return null;
        }

        GameObject objectToSpawn = poolDictionary[type].Dequeue();

        if(objectToSpawn != null && !objectToSpawn.activeInHierarchy)
        {

            objectToSpawn.SetActive(true);
            objectToSpawn.transform.position = position;
            objectToSpawn.transform.rotation = rotation;

            poolDictionary[type].Enqueue(objectToSpawn);

            //Debug.Log("Pooler Used");

            return objectToSpawn;
        }
        else
        {
            foreach (Pool pool in pools)
            {
                if (pool.type == type)
                {
                    pool.poolSize++;
                    objectToSpawn = Instantiate(pool.objectPrefab, position, rotation);
                    poolDictionary[type].Enqueue(objectToSpawn);
                    return objectToSpawn;
                }
            }
        }
        return null;
    }
}

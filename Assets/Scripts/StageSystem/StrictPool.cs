using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages object pooling for various reusable game objects.
/// Prevents runtime instantiation overhead by reusing inactive instances.
/// </summary>
public enum PoolType
{
    DamageText,
    DamageTextCritical,
    Coin
}

public class StrictPool : MonoBehaviour
{
    public static StrictPool Instance { get; private set; }

    [System.Serializable]
    public class Pool
    {
        public PoolType type;
        public GameObject prefab;
        public int size;
        public Transform parent;
    }

    public List<Pool> pools;

    private Dictionary<PoolType, Queue<GameObject>> poolDictionary;
    private Dictionary<PoolType, Pool> poolInfoLookup;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        poolDictionary = new Dictionary<PoolType, Queue<GameObject>>();
        poolInfoLookup = new Dictionary<PoolType, Pool>();

        foreach (var pool in pools)
        {
            Queue<GameObject> objectPool = new();

            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab, pool.parent);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }

            poolDictionary[pool.type] = objectPool;
            poolInfoLookup[pool.type] = pool;
        }
    }

    public GameObject SpawnFromPool(PoolType type, Vector3 position, Quaternion rotation, Transform parent = null)
    {
        if (!poolDictionary.ContainsKey(type))
        {
            return null;
        }

        Queue<GameObject> objectPool = poolDictionary[type];
        GameObject obj = null;

        int count = objectPool.Count;
        for (int i = 0; i < count; i++)
        {
            GameObject candidate = objectPool.Dequeue();
            objectPool.Enqueue(candidate);

            if (!candidate.activeInHierarchy)
            {
                obj = candidate;
                break;
            }
        }

        if (obj == null)
        {
            if (!poolInfoLookup.TryGetValue(type, out Pool pool) || pool.prefab == null)
            {
                return null;
            }

            obj = Instantiate(pool.prefab, pool.parent);
            obj.SetActive(false);
            poolDictionary[type].Enqueue(obj);
        }

        if (parent != null)
            obj.transform.SetParent(parent, false);

        obj.transform.SetPositionAndRotation(position, rotation);
        obj.SetActive(true);
        return obj;
    }

    public void ReturnToPool(GameObject obj, PoolType type)
    {
        if (!poolDictionary.ContainsKey(type))
        {
            return;
        }

        obj.SetActive(false);
        poolDictionary[type].Enqueue(obj);
    }
}

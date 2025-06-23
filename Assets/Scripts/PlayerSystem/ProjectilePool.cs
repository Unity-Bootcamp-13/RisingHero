using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages pooled projectiles by type using string keys.
/// Each projectile type is initialized with a prefab and pool size.
/// </summary>
public class ProjectilePool : MonoBehaviour
{
    public static ProjectilePool Instance { get; private set; }

    [System.Serializable]
    public class PoolInfo
    {
        public string key;             // Unique identifier for the projectile type
        public GameObject prefab;      // Prefab to pool
        public int initialSize = 10;   // Number of instances to pre-instantiate
    }

    [SerializeField] private List<PoolInfo> pools;

    private Dictionary<string, Queue<GameObject>> poolDict = new();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        // Initialize pools
        foreach (var pool in pools)
        {
            Queue<GameObject> queue = new();

            for (int i = 0; i < pool.initialSize; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                queue.Enqueue(obj);
            }

            poolDict[pool.key] = queue;
        }
    }

    // Spawns a projectile from the pool or creates a new one if the pool is empty.
    public GameObject Spawn(string key, Vector3 position, Quaternion rotation)
    {
        if (!poolDict.TryGetValue(key, out var queue))
        {
            Debug.LogWarning($"[ProjectilePoolManager] Pool for key '{key}' not found.");
            return null;
        }

        GameObject obj = queue.Count > 0 ? queue.Dequeue() : Instantiate(GetPrefabByKey(key));
        obj.transform.position = position;
        obj.transform.rotation = rotation;
        obj.SetActive(true);
        return obj;
    }

    // Returns a projectile to the pool.
    public void Despawn(string key, GameObject obj)
    {
        obj.SetActive(false);

        if (!poolDict.ContainsKey(key))
        {
            Debug.LogWarning($"[ProjectilePoolManager] Tried to despawn unknown key '{key}'. Destroying object.");
            Destroy(obj);
            return;
        }

        poolDict[key].Enqueue(obj);
    }

    // Finds the prefab associated with a given key from the original pool list
    private GameObject GetPrefabByKey(string key)
    {
        var info = pools.Find(p => p.key == key);
        return info?.prefab;
    }
}

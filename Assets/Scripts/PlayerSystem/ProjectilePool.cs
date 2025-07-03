using System.Collections.Generic;
using UnityEngine;

public class ProjectilePool : MonoBehaviour
{
    public static ProjectilePool Instance { get; private set; }

    [System.Serializable]
    public class PoolInfo
    {
        public string key;
        public GameObject prefab;
        public int initialSize = 10;
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

    public GameObject Spawn(string key, Vector3 position, Quaternion rotation)
    {
        if (!poolDict.TryGetValue(key, out var queue))
        {
            return null;
        }

        GameObject obj = queue.Count > 0 ? queue.Dequeue() : Instantiate(GetPrefabByKey(key));
        obj.transform.position = position;
        obj.transform.rotation = rotation;
        obj.SetActive(true);
        return obj;
    }

    public void Despawn(string key, GameObject obj)
    {
        obj.SetActive(false);

        if (!poolDict.ContainsKey(key))
        {
            Destroy(obj);
            return;
        }

        poolDict[key].Enqueue(obj);
    }

    private GameObject GetPrefabByKey(string key)
    {
        var info = pools.Find(p => p.key == key);
        return info?.prefab;
    }
}

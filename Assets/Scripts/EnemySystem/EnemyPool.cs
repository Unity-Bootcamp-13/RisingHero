using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : MonoBehaviour
{
    public enum EnemyType // Pool type도 프로젝트에 맞게 수정해야 함.
    {
        Normal1,
        Elite1,
        Normal2,
        Elite2
    }

    public static EnemyPool Instance { get; private set; }

    [System.Serializable]
    public class EnemyPoolInfo
    {
        public EnemyType type;
        public List<GameObject> prefabs;
        public int initialSize = 10;
    }

    public List<EnemyPoolInfo> enemyPools;

    private Dictionary<EnemyType, List<GameObject>> poolDictionary = new();
    private Dictionary<EnemyType, List<GameObject>> prefabDictionary = new();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        foreach (var pool in enemyPools)
        {
            if (!prefabDictionary.ContainsKey(pool.type))
                prefabDictionary[pool.type] = new List<GameObject>(pool.prefabs);

            List<GameObject> list = new();
            for (int i = 0; i < pool.initialSize; i++)
            {
                GameObject prefab = GetRandomPrefab(pool.type);
                if (prefab == null) continue;

                GameObject obj = Instantiate(prefab);
                obj.SetActive(false);
                list.Add(obj);
            }

            poolDictionary[pool.type] = list;
        }
    }

    public GameObject GetEnemy(EnemyType type)
    {
        if (!poolDictionary.ContainsKey(type))
            return null;

        foreach (var enemy in poolDictionary[type])
        {
            if (enemy != null && !enemy.activeInHierarchy)
                return enemy;
        }

        GameObject prefab = GetRandomPrefab(type);
        if (prefab != null)
        {
            GameObject obj = Instantiate(prefab);
            obj.SetActive(false);
            poolDictionary[type].Add(obj);
            return obj;
        }

        return null;
    }
    private GameObject GetRandomPrefab(EnemyType type)
    {
        if (!prefabDictionary.ContainsKey(type) || prefabDictionary[type].Count == 0)
            return null;

        var list = prefabDictionary[type];
        return list[Random.Range(0, list.Count)];
    }
}

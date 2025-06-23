using System.Collections.Generic;
using UnityEngine;

public class AliveEnemyManager : MonoBehaviour
{
    public static AliveEnemyManager Instance { get; private set; }

    private readonly List<Transform> aliveEnemies = new();

    public IReadOnlyList<Transform> Enemies => aliveEnemies;

    // Initializes the singleton instance
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    // Registers a newly activated enemy
    public void Register(Transform enemy)
    {
        if (!aliveEnemies.Contains(enemy))
            aliveEnemies.Add(enemy);
    }

    // Unregisters a deactivated enemy
    public void Unregister(Transform enemy)
    {
        aliveEnemies.Remove(enemy);
    }
}

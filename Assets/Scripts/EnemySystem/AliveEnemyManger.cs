using System.Collections.Generic;
using UnityEngine;

public static class AliveEnemyManager
{
    private static readonly HashSet<Enemy> aliveEnemies = new();

    public static IReadOnlyCollection<Enemy> Enemies => aliveEnemies;

    public static void Register(Enemy enemy)
    {
        if (enemy != null)
            aliveEnemies.Add(enemy);
    }

    public static void Unregister(Enemy enemy)
    {
        if (enemy != null)
            aliveEnemies.Remove(enemy);
    }

    public static int GetAliveEnemyCount()
    {
        aliveEnemies.RemoveWhere(e => e == null || !e.gameObject.activeInHierarchy);
        return aliveEnemies.Count;
    }
}

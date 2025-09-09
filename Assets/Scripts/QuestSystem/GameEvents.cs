using System;

public static class GameEvents
{
    public static event Action EnemyKilled;

    public static void RaiseEnemyKilled()
    {
        EnemyKilled?.Invoke();
    }
}
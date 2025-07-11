using System.Collections.Generic;
using UnityEngine;

public class PlayerDetection
{
    private readonly PlayerStatus playerStatus;

    public PlayerDetection(PlayerStatus playerStatus)
    {
        this.playerStatus = playerStatus;
    }

    public Transform FindNearestEnemy(Vector3 origin, IReadOnlyCollection<Enemy> enemies)
    {
        float minDist = float.MaxValue;
        Transform nearest = null;

        foreach (var enemy in enemies)
        {
            float dist = Vector3.SqrMagnitude(enemy.transform.position - origin);
            if (dist < minDist)
            {
                minDist = dist;
                nearest = enemy.transform;
            }
        }

        return nearest;
    }

    public bool IsInRange(Vector3 origin, Transform target)
    {
        if (target == null) return false;
        return Vector3.Distance(origin, target.position) <= playerStatus.attackRange;
    }

    public int GetDirectionCode(Vector2 dir)
    {
        if (Mathf.Abs(dir.x) > Mathf.Abs(dir.y))
            return dir.x > 0 ? 4 : 2; // Right : Left
        else
            return dir.y > 0 ? 1 : 3; // Up : Down
    }
}
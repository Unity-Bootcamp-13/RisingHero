using UnityEngine;

public class AOESkillBehavior : ISkillBehavior
{
    public void Execute(SkillData data, Transform casterTransform, int level)
    {
        int damage = data.GetPowerWithLevel(level);

        foreach (var enemy in AliveEnemyManager.Enemies)
        {
            float dist = Vector2.Distance(enemy.transform.position, casterTransform.position);
            if (dist <= data.Range && enemy.TryGetComponent(out IDamageable damageable))
            {
                damageable.TakeDamage(damage);
            }
        }

        var prefab = Resources.Load<GameObject>(data.PrefabName);
        if (prefab != null)
        {
            Object.Instantiate(prefab, casterTransform.position, Quaternion.identity);
        }
    }
}

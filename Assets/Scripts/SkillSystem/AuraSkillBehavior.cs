using UnityEngine;
using System.Collections;

public class AuraSkillBehavior : ISkillBehavior
{
    public void Execute(SkillData data, Transform casterTransform, int level)
    {
        casterTransform.GetComponent<MonoBehaviour>().StartCoroutine(ApplyAuraEffect(data, casterTransform, level));

        var prefab = Resources.Load<GameObject>(data.PrefabName);
        if (prefab != null)
        {
            var obj = Object.Instantiate(prefab, casterTransform.position, Quaternion.identity);

            var lifetimeComponent = obj.GetComponent<SkillEffectLifetime>();
            if (lifetimeComponent != null)
            {
                lifetimeComponent.SetLifetime(data.Duration);
            }
        }
    }

    private IEnumerator ApplyAuraEffect(SkillData data, Transform casterTransform, int level)
    {
        float elapsed = 0f;
        int tickDamage = data.GetTickDamageWithLevel(level);

        while (elapsed < data.Duration)
        {
            foreach (var enemy in AliveEnemyManager.Enemies)
            {
                float dist = Vector2.Distance(enemy.transform.position, casterTransform.position);
                if (dist <= data.Range && enemy.TryGetComponent(out IDamageable damageable))
                {
                    damageable.TakeDamage(tickDamage);
                }
            }

            elapsed += 1f;
            yield return new WaitForSeconds(1f);
        }
    }
}

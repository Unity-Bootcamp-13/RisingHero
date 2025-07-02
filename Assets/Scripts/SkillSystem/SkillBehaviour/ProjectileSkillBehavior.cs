using UnityEngine;

// 플레이어가 발사하는 투사체 스킬의 정의
public class ProjectileSkillBehavior : ISkillBehaviour
{
    private Vector3 GetEnemyPosition(Vector3 origin, float searchRadius, LayerMask targetLayer, Transform caster)
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(origin, searchRadius, targetLayer);

        // 만약 타겟이 없다면, 플레이어의 정면으로 발사
        if (hits.Length == 0)
            return origin + caster.right;

        Collider2D closest = hits[0];   // 제일 가까운 적을 맞혀야 할 타겟 배열 맨 앞에 둠
        float minDist = Vector2.Distance(origin, hits[0].transform.position);

        foreach (var hit in hits)
        {
            float dist = Vector2.Distance(origin, hit.transform.position);

            if (dist < minDist)
            {
                minDist = dist;
                closest = hit;
            }
        }

        return closest.transform.position;
    }

    public void Execute(SkillData data, Transform casterTransform)
    {
        var prefab = Resources.Load<GameObject>(data.PrefabName);
        GameObject projectile = Object.Instantiate(prefab, casterTransform.position, Quaternion.identity);

        // 투사체 이동 로직 추가 필요
        // 테스트용
        var proj = projectile.GetComponent<Projectile>();

        if (proj != null)
        {
            LayerMask targetLayer = LayerMask.GetMask("Enemy");
            float searchRadius = 6f;

            Vector3 targetPos = GetEnemyPosition(casterTransform.position, searchRadius, targetLayer, casterTransform);
            Vector2 direction = (targetPos - casterTransform.position).normalized;

            proj.Initialize(
                direction: direction,
                speed: 5f,
                damage: data.Power,
                lifetime: 2f,
                targetLayer: targetLayer,
                cooldown: data.CooldownTime
            );
        }
    }
}
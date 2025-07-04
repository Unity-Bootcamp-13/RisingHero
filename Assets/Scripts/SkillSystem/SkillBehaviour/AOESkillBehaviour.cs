using UnityEngine;

// 플레이어가 시전하는 범위 지정형 스킬의 정의
public class AOESkillBehaviour : ISkillBehaviour
{
    private Vector3 GetEnemyPosition(Vector3 origin, float searchRadius, LayerMask targetLayer)
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(origin, searchRadius, targetLayer);

        // 감지된 타겟이 없다면, 플레이어의 발 밑에 쏨(일단 임시 설정, 개선 가능)
        if (hits.Length == 0)
            return origin;

        Collider2D closest = hits[0];       // 제일 가까운 적을 맞혀야 할 타겟 배열 맨 앞에 둠
        float minDist = Vector2.Distance(origin, hits[0].transform.position);

        // 일단 foreach로 순회 하는데, 이 역시 개선 필요 있어보임
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
        LayerMask targetLayer = LayerMask.GetMask("Enemy");
        float searchRadius = 8f;
        
        Vector3 spawnPos = GetEnemyPosition(casterTransform.position, searchRadius, targetLayer);

        var go = GameObject.Instantiate(Resources.Load<GameObject>(data.PrefabName), spawnPos, Quaternion.identity);

        var aoe = go.GetComponent<AOE>();

        if (aoe != null)
        {
            SkillCaster caster = casterTransform.GetComponent<SkillCaster>();
            float finalDamage = caster != null ? caster.GetSlotDamage(data) : data.Power;

            aoe.Initialize(
                damage: finalDamage,
                radius: data.Range,
                duration: data.Duration,
                tickInterval: data.TickInterval,
                cooldown: data.CooldownTime,
                targetLayer: targetLayer
            );
        }

        // 범위 피해 처리 로직 추가 필요
    }
}
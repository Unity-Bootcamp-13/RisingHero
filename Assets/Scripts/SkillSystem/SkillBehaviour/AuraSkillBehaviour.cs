using UnityEngine;

// 플레이어 중심의 오라형 스킬에 대한 정의
public class AuraSkillBehaviour : ISkillBehaviour
{
    public void Execute(SkillData data, Transform casterTransform)
    {
        var prefab = Resources.Load<GameObject>(data.PrefabName);
        var auraObject = GameObject.Instantiate(prefab, casterTransform.position,Quaternion.identity);

        var aura = auraObject.GetComponent<Aura>();

        if (aura != null)
        {
            LayerMask targetLayer = LayerMask.GetMask("Enemy");

            // SkillData의 필드를 받아와야 SkillTester에서 설정한 duration 적용가능
            float duration = data.Duration;

            SkillCaster caster = casterTransform.GetComponent<SkillCaster>();
            float finalDamage = caster != null ? caster.GetSlotDamage(data) : data.Power;

            aura.Initialize(
                damage: finalDamage,
                radius: data.Range,
                duration: duration,   // 0 이하로 설정 시 영구 지속
                tickInterval: data.TickInterval,
                cooldown: data.CooldownTime,
                caster: casterTransform,
                targetLayer: targetLayer
            );
        }
    }
}
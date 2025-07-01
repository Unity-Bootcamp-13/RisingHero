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

            aura.Initialize(
                damage: data.Power,
                radius: data.Range,
                tickInterval: 1f,
                duration: 4f,   // 0 이하로 설정 시 영구 지속
                caster: casterTransform,
                targetLayer: targetLayer
            );
        }
    }
}
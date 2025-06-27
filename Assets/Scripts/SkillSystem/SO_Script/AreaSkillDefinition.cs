using UnityEngine;
using System.Collections.Generic;

// Assets/Create/Skills/Area Skill Definition
[CreateAssetMenu(fileName = "NewAreaSkill", menuName = "Skills/Area Skill Definition", order = 2)]
public class AreaSkillDefinition : SkillDefinition
{
    [Header("Area Specifics")]
    public GameObject AreaEffectPrefab;     // ���� ��ų ����Ʈ ������
    public float AreaRadius = 5f;           // ���� �ݰ� (�ӽ�)
    public float AreaDuration = 2f;         // ���� ���� �ð� (�ӽ�)
    public float DamageTickInterval = 0.5f; // ���� �� ������ ���� ������ �ֱ� (�ӽ�)

    public override bool CanCastSkill(CharacterStats casterStats, Dictionary<SkillDefinition, float> skillCooldowns)
    {
        if (!base.CanCastSkill(casterStats, skillCooldowns))
            return false;

        return true;
    }

    public override void ExecuteSkill(SkillCaster caster, Targetable123 target, Vector3? castPosition = null)
    {
        if (AreaEffectPrefab == null)
        {
            Debug.LogError($"������ų {SkillName}�� ����Ʈ ������ �Ǿ� ���� �ʽ��ϴ�!");
            return;
        }

        Vector3 spawnPosition = castPosition ?? caster.transform.position;  // ĳ���� ��ġ or �������� ��ġ

        // ���� ����Ʈ ���� (Ǯ�� ���� ����)
        GameObject areaGO = Instantiate(AreaEffectPrefab, spawnPosition, Quaternion.identity);
        AreaEffect areaEffect = areaGO.GetComponent<AreaEffect>();

        if (areaEffect != null)
        {
            // ��ų ������ ���� ���ط� ���
            int currentSkillLevel = caster.GetSkillLevel(this); // SkillCaster���� ���� ��ų���� �޾ƿ���
            float calculatedDamage = GetCalculatedDamage(currentSkillLevel);

            areaEffect.Initialize(
                caster: caster,
                damage: calculatedDamage,
                radius: AreaRadius,
                duration: AreaDuration,
                damageTickInterval: DamageTickInterval,
                hitEffect: HitEffect
            );
        }

        else
            Debug.LogError($"���� ��ų ����Ʈ {areaEffect.name}�� ������Ʈ�� �����ϴ�!");
    }
}

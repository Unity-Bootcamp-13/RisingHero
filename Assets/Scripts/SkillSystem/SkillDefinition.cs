using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "NewSkillDefinition", menuName = "Skills/Base Skill Definition")]
public abstract class SkillDefinition : ScriptableObject
{
    [Header("Basic Skill Info")]
    public string SkillName = "New Skill";
    public Sprite SkillIcon;
    [TextArea] public string Description = "Skill Description";

    [Header("Core Stats")]
    // Skill Level�� SkillCaster���� ������ �����̹Ƿ�, ���⼱ �⺻���� �ӽ÷� ����.
    public float BaseDamage = 10f;      // �⺻ ���ط�
    public float DamagePerLevel = 2f;   // ���� ��� �� ������ ���ط�
    public float CoolDown = 5f;         // ��Ÿ��
    public int ManaCost = 10;           // ���� �Ҹ�
    public float CastTime = 0f;         // ��ų �����ð� (0�̸� ��� ����)

    [Header("Effects")]
    public GameObject HitEffect;        // Ÿ�ٿ��� �ǰ� �� �߻��ϴ� ����Ʈ

    public float GetCalculatedDamage(int currentSkillLevel)
    {
        return BaseDamage + (currentSkillLevel - 1) * DamagePerLevel;
    }

    /// <summary>
    /// ��ų �ߵ� ��, ��ų ��뿡 �ʿ��� ������ �˻�
    /// </summary>
    public virtual bool CanCastSkill(CharacterStats casterStats, Dictionary<SkillDefinition, float> skillCooldowns)
    {
        // �÷��̾��� ������ ������ ���
        if (casterStats.CurrentMana < ManaCost)
        {
            Debug.Log($"������ �����մϴ�. �ʿ� ���� : {ManaCost}, ���� ���� : {casterStats.CurrentMana}");
            return false;
        }

        if (skillCooldowns.ContainsKey(this) && skillCooldowns[this] > 0)
        {
            Debug.Log($"{SkillName}�� ���� ������Դϴ�. ���� �ð� : {skillCooldowns[this]:F2}��");
            return false;
        }

        return true;
    }

    /// <summary>
    /// ��ų�� ���� ����
    /// </summary>
    public abstract void ExecuteSkill(SkillCaster caster, Targetable target, Vector3? castPosition = null);
}

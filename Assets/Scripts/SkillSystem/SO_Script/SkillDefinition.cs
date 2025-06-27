using UnityEngine;
using System.Collections.Generic;
using System;

[CreateAssetMenu(fileName = "NewSkillDefinition", menuName = "Skills/Base Skill Definition")]
public abstract class SkillDefinition : ScriptableObject
{
    [Header("Skill ID")]
    public int SkillID;     // ��ų�� ���� ID. �����Ϳ��� ���� �Ҵ絵 �ǰ�, �ڵ����� ������ ����.

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
    public abstract void ExecuteSkill(SkillCaster caster, Targetable123 target, Vector3? castPosition = null);

    internal void ExecuteSkill(SkillCaster123 skillCaster123, Targetable123 target, Vector3? castPosition)
    {
        throw new NotImplementedException();
    }

    internal void ExecuteSkill(SkillCaster123 skillCaster123, Targetable123 targetable123)
    {
        throw new NotImplementedException();
    }

    internal bool CanCastSkill(CharacterStats123 characterStats, Dictionary<SkillDefinition, float> skillCooldowns)
    {
        throw new NotImplementedException();
    }
}

using System.Collections.Generic;
using UnityEngine;

public class SkillTester : MonoBehaviour
{
    public SkillCaster player;  // �ν����Ϳ��� ����
    public string projectilePrefabName = "FireballTest";
    // public GameObject projectilePrefab;
    public string areaPrefabName = "AOETest";
    // public GameObject areaPrefab;
    public string auraPrefabName = "AuraTest";
    // public GameObject auraPrefab;

    private void Awake()
    {
        // ��ų ����
        SkillData fireball = new SkillData
        {
            ID = 10001,
            Name = "Fireball",
            Type = SkillType.Projectile,
            Power = 20,
            DamagePerLevel = 4f,
            Range = 5f,
            CooldownTime = 3f,
            PrefabName = projectilePrefabName
        };

        SkillData area = new SkillData
        {
            ID = 20001,
            Name = "Area",
            Type = SkillType.AOE,
            Power = 50,
            DamagePerLevel = 15f,
            Range = 3f,
            CooldownTime = 5f,
            PrefabName = areaPrefabName,
            Duration = 0.3f,
            TickInterval = 0.2f
        };

        SkillData aura = new SkillData
        {
            ID = 30001,
            Name = "Aura",
            Type = SkillType.Aura,
            Power = 10,
            DamagePerLevel = 5f,
            Range = 2f,
            CooldownTime = 10f,
            PrefabName = auraPrefabName,
            Duration = 5f,
            TickInterval = 1f
        };

        // ���� ���� �� SkillCaster�� ����
        player.equippedSkills = new List<SkillSlot>
        {
            new SkillSlot { Skill = fireball, Level = 1 },
            new SkillSlot { Skill = area, Level = 1 },
            new SkillSlot { Skill = aura, Level = 1 }
        };
    }

    public void ToggleAutoMode()
    {
        bool shouldEnable = true;

        // ��ų �ϳ��� �ڵ����� üũ. �ڵ��� �� �ڵ� ��� ������
        for (int i = 0; i < player.equippedSkills.Count; i++)
        {
            if (player.equippedSkills[i].IsAuto)
            {
                shouldEnable = false;
                break;
            }
        }

        for (int i = 0; i < player.equippedSkills.Count; i++)
        {
            var slot = player.equippedSkills[i];
            slot.IsAuto = shouldEnable;

            if (shouldEnable && !slot.IsOnCooldown)
                player.CastSkill(slot.Skill);
        }
    }

    public void LevelUpSkill(int skillID)
    {
        SkillSlot slot = player.equippedSkills.Find(s => s.Skill.ID == skillID);

        if (slot != null)
        {
            slot.Level++;
            Debug.Log($"{slot.Skill.Name}�� ������ {slot.Level}�� �����߽��ϴ�. " +
                $"���� ���ط� : {slot.FinalDamage}");
        }
    }

    // �� ���� �״�� ��ų �ø� �� ���� id�� �ٲٸ� ����
    public void CastProjSkill()
    {
        SkillData skill = player.equippedSkills.Find(slot => slot.Skill.ID == 1)?.Skill;
        if (skill != null)
            player.CastSkill(skill);
    }

    public void CastAreaSkill()
    {
        SkillData skill = player.equippedSkills.Find(slot => slot.Skill.ID == 2)?.Skill;
        if (skill != null)
            player.CastSkill(skill);
    }

    public void CastAuraSkill()
    {
        SkillData skill = player.equippedSkills.Find(slot => slot.Skill.ID == 3)?.Skill;
        if (skill != null)
            player.CastSkill(skill);
    }
}

using System.Collections.Generic;
using UnityEngine;

public class SkillTester : MonoBehaviour
{
    public SkillCaster player;  // 인스펙터에서 연결
    public string projectilePrefabName = "FireballTest";
    // public GameObject projectilePrefab;
    public string areaPrefabName = "AOETest";
    // public GameObject areaPrefab;
    public string auraPrefabName = "AuraTest";
    // public GameObject auraPrefab;

    private void Awake()
    {
        // 스킬 생성
        SkillData fireball = new SkillData
        {
            ID = 1,
            Name = "Fireball",
            Type = SkillType.Projectile,
            Power = 20,
            Range = 5f,
            CooldownTime = 3f,
            PrefabName = projectilePrefabName
        };

        SkillData area = new SkillData
        {
            ID = 2,
            Name = "Area",
            Type = SkillType.AOE,
            Power = 50,
            Range = 3f,
            CooldownTime = 5f,
            PrefabName = areaPrefabName,
            Duration = 0.3f,
            TickInterval = 0.2f
        };

        SkillData aura = new SkillData
        {
            ID = 3,
            Name = "Aura",
            Type = SkillType.Aura,
            Power = 10,
            Range = 2f,
            CooldownTime = 10f,
            PrefabName = auraPrefabName,
            Duration = -1f,
            TickInterval = 1f
        };

        // 슬롯 생성 후 SkillCaster에 장착
        player.equippedSkills = new List<SkillSlot>
        {
            new SkillSlot { Skill = fireball, Level = 1 },
            new SkillSlot { Skill = area, Level = 1 },
            new SkillSlot { Skill = aura, Level = 1 }
        };
    }

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

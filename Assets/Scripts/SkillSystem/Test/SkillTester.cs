using UnityEngine;

public class SkillTester : MonoBehaviour
{
    public SkillCaster player;  // 인스펙터에서 연결
    public string projectilePrefabName = "FireballTest";
    public string areaPrefabName = "AOETest";
    public string auraPrefabName = "AuraTest";

    public void CastProjSkill()
    {
        // 스킬 정보
        SkillData fireball = new SkillData
        {
            ID = 1,
            Name = "Fireball",
            Type = SkillType.Projectile,
            Power = 20,
            Range = 5f,
            PrefabName = projectilePrefabName
        };

        player.CastSkill(fireball);
    }

    public void CastAreaSkill()
    {
        // 스킬 정보
        SkillData areaTest = new SkillData
        {
            ID = 2,
            Name = "Area",
            Type = SkillType.AOE,
            Power = 50,
            Range = 3f, // 범위의 크기
            PrefabName = areaPrefabName
        };

        player.CastSkill(areaTest);
    }

    public void CastAuraSkill()
    {
        // 스킬 정보
        SkillData auraTest = new SkillData
        {
            ID = 3,
            Name = "Aura",
            Type = SkillType.Aura,
            Power = 10,
            Range = 2f, // 오라의 크기
            PrefabName = auraPrefabName
        };

        player.CastSkill(auraTest);
    }
}

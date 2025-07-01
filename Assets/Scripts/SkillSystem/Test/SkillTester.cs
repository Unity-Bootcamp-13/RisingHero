using UnityEngine;

public class SkillTester : MonoBehaviour
{
    public SkillCaster player;  // �ν����Ϳ��� ����
    public string projectilePrefabName = "FireballTest";
    public string areaPrefabName = "AOETest";
    public string auraPrefabName = "AuraTest";

    public void CastProjSkill()
    {
        // ��ų ����
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
        // ��ų ����
        SkillData areaTest = new SkillData
        {
            ID = 2,
            Name = "Area",
            Type = SkillType.AOE,
            Power = 50,
            Range = 3f, // ������ ũ��
            PrefabName = areaPrefabName
        };

        player.CastSkill(areaTest);
    }

    public void CastAuraSkill()
    {
        // ��ų ����
        SkillData auraTest = new SkillData
        {
            ID = 3,
            Name = "Aura",
            Type = SkillType.Aura,
            Power = 10,
            Range = 2f, // ������ ũ��
            PrefabName = auraPrefabName
        };

        player.CastSkill(auraTest);
    }
}

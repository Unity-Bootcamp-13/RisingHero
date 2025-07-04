using System;

public static class SkillBehaviourFactory
{
    // SkillType.cs�� enum���� Projectile, AOE, Aura ������ �����ص�. �� ������ �°� ���ο� ��ų ���� ����
    private static readonly ISkillBehaviour[] behaviours = new ISkillBehaviour[]
    {
        new ProjectileSkillBehavior(),
        new AOESkillBehaviour(),
        new AuraSkillBehaviour()
    };

    // ��ų Ÿ���� enum ���� �´� ISkillBehaviour�� ��ü ��ȯ
    public static ISkillBehaviour GetSkillBehaviour(SkillType type)
    {
        int index = (int)type;
        if (index < 0 || index >= behaviours.Length)
            throw new ArgumentOutOfRangeException(nameof(type), $"{type}�� ��ȿ���� ���� Ÿ���Դϴ�.");

        return behaviours[index];
    }
}
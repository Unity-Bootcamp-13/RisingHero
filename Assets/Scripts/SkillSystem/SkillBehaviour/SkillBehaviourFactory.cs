using System;

public static class SkillBehaviourFactory
{
    // SkillType.cs에 enum으로 Projectile, AOE, Aura 순으로 정의해둠. 그 순서에 맞게 새로운 스킬 정보 정의
    private static readonly ISkillBehaviour[] behaviours = new ISkillBehaviour[]
    {
        new ProjectileSkillBehavior(),
        new AOESkillBehaviour(),
        new AuraSkillBehaviour()
    };

    // 스킬 타입의 enum 값에 맞는 ISkillBehaviour의 객체 반환
    public static ISkillBehaviour GetSkillBehaviour(SkillType type)
    {
        int index = (int)type;
        if (index < 0 || index >= behaviours.Length)
            throw new ArgumentOutOfRangeException(nameof(type), $"{type}은 유효하지 않은 타입입니다.");

        return behaviours[index];
    }
}
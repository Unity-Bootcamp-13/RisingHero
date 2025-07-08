using System;

public static class SkillBehaviorFactory
{
    public static ISkillBehavior GetSkillBehavior(SkillType type)
    {
        return type switch // 외부에서 스킬 타입을 추가한다면 등록기반으로 리팩토링해야함.
        {
            SkillType.AOE => new AOESkillBehavior(),
            SkillType.Aura => new AuraSkillBehavior(),
            _ => throw new ArgumentException($"[SkillBehaviorFactory] Unknown skill type: {type}")
        };
    }
}

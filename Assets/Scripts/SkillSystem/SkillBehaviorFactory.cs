using System;

public static class SkillBehaviorFactory
{
    public static ISkillBehavior GetSkillBehavior(SkillType type)
    {
        return type switch
        {
            SkillType.AOE => new AOESkillBehavior(),
            SkillType.Aura => new AuraSkillBehavior(),
            _ => throw new ArgumentException($"[SkillBehaviorFactory] Unknown skill type: {type}")
        };
    }
}

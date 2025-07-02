using System;

// 스킬 슬롯: 스킬 데이터 + 레벨
[Serializable]
public class SkillSlot
{
    public SkillData Skill;
    public int Level;
    public float CurrentCooldown;   // 현재 쿨타임 (남은 쿨타임)
    public bool IsOnCooldown => CurrentCooldown > 0f;
}
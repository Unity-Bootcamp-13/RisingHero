using System;

// 스킬 슬롯: 스킬 데이터 + 레벨
[Serializable]
public class SkillSlot
{
    public SkillData Skill;
    public int Level;
}
using UnityEngine;

[System.Serializable]   // 인스펙터에서 볼 수 있게 하기 위함. 제거해도 됨
public class SkillData123
{
    public SkillDefinition Definition; // 이 스킬 데이터가 참조하는 SkillDefinition SO
    public float CurrentCooldown;      // 현재 남은 쿨타임
    public int CurrentSLevel;           // 현재 스킬 레벨

    public SkillData123(SkillDefinition definition, int initialLevel = 1)
    {
        Definition = definition;
        CurrentCooldown = 0f;
        CurrentSLevel = initialLevel;
    }

    public void UpdateCooldown(float deltaTime)
    {
        if (CurrentCooldown > 0)
        {
            CurrentCooldown -= deltaTime;
            if (CurrentCooldown < 0)
                CurrentCooldown = 0;
        }
    }

    // 스킬 레벨업 기능
    public void LevelUp()
    {
        CurrentSLevel++;
        Debug.Log($"{Definition.SkillName} 스킬을 레벨업 했습니다. 현재 레벨: {CurrentSLevel}");
        // 여기에 레벨업 시 발생할 추가 효과 (예: 이펙트, 사운드) 로직 추가 가능
    }

    // 스킬 사용 시 쿨타임 적용 기능
    public void ApplyCooldown()
    {
        CurrentCooldown = Definition.CoolDown;
    }
}

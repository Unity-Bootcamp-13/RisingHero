using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 현재 활성화된 스킬들을 관리하고 자동으로 쿨타임을 갱신하고 발동시키는 매니저 입니다.
/// </summary>
public class SkillManager : MonoBehaviour
{
    private readonly List<ISkill> skills = new();

    /// <summary>
    /// 플레이어가 새로운 스킬 습득 시 호출되어 SkillManager에 등록합니다.
    /// </summary>
    /// <param name="skill">등록할 스킬 객체</param>
    public void AddSkill(ISkill skill)
    {
        skills.Add(skill);
    }

    private void Update()
    {
        float deltaTime = Time.deltaTime;

        // 등록되어 있는 (현재 플레이어가 가지고 있는) 모든 스킬을 순회
        foreach (var skill in skills)
        {
            // 시간의 흐름에 맞게 쿨타임 감소
            skill.UpdateCooldown(deltaTime);
            
            // 해당 스킬이 IActivatableSkill을 구현한 액티브 스킬인 경우
            if (skill is IActivatableSkill activatable)
            {
                // 스킬 발동, 이후 다시 쿨타임 초기화하여 다음 발동까지 대기
                activatable.Activate();
            }
        }
    }
}

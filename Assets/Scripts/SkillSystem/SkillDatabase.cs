using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// 전체 스킬 목록을 관리, 외부에서 스킬 ID로 조회할 수 있도록 메서드 제공
public class SkillDatabase : MonoBehaviour
{
    // 게임 내에 존재하는 모든 스킬의 정보를 담은 리스트, csv로 불러올 수 있음.
    public List<SkillData> allSkills;

    // 전달 받은 id를 기준으로 위에서 선언한 리스트에서 일치하는 첫 번째 스킬을 반환, 만약 일치하는 스킬이 없을 시 null
    public SkillData GetSkillById(int id)
    {
        return allSkills.FirstOrDefault(s => s.ID == id);
    }
}
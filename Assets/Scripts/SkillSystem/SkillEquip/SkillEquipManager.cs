using System.Collections.Generic;
using UnityEngine;

// TODO 리스트
// 기존의 레벨 데이터가 있는 스킬 장착할 때의 로직 추가
// 최대 장착 슬롯 개수 제한 (현재 3슬롯 구상중)
// 스킬 레벨업, 스킬 사용 규칙 검증(마나, 쿨타임, 기존에 시전 중인 스킬 유무 판단)
// UI랑 연동

// 플레이어가 선택해서 장착한 스킬 목록 관리
public class SkillEquipManager : MonoBehaviour
{
    // 장착 중인 스킬 목록
    public List<SkillSlot> equippedSkills = new();

    public void Equip(SkillData skill)
    {
        // 중복 장착 방지 (이미 장착 중인 스킬과 새로 장착하려는 스킬 간의 ID 비교)
        if (equippedSkills.Exists(slot => slot.Skill.ID == skill.ID))
            return;

        // 새로이 1레벨로 초기화 하여 장착
        equippedSkills.Add(new SkillSlot { Skill = skill, Level = 1 });

        // 기존에 레벨업 해둔 스킬 장착할 때의 로직 추가 필요
    }

    public void Unequip(SkillData skill)
    {
        // 전달 받은 스킬 장착 해제
        equippedSkills.RemoveAll(slot => slot.Skill.ID == skill.ID);
    }
}
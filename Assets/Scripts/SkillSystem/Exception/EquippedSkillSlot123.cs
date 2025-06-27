using UnityEngine;

// 장착된 스킬 슬롯 정보를 나타내주는 구조체
[System.Serializable]
public struct EquippedSkillSlot123
{
    public int SlotIndex;   // 슬롯 번호
    public int SkillID;     // 해당 슬롯에 장착된 스킬의 ID
    public bool IsEquipped => SkillID != 0; // 스킬 ID 0 : 비어있는 슬롯으로 지정

    public EquippedSkillSlot123(int slotIndex, int skillID)
    {
        SlotIndex = slotIndex;
        SkillID = skillID;
    }
}

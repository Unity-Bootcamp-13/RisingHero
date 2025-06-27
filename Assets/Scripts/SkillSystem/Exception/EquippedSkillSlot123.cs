using UnityEngine;

// ������ ��ų ���� ������ ��Ÿ���ִ� ����ü
[System.Serializable]
public struct EquippedSkillSlot123
{
    public int SlotIndex;   // ���� ��ȣ
    public int SkillID;     // �ش� ���Կ� ������ ��ų�� ID
    public bool IsEquipped => SkillID != 0; // ��ų ID 0 : ����ִ� �������� ����

    public EquippedSkillSlot123(int slotIndex, int skillID)
    {
        SlotIndex = slotIndex;
        SkillID = skillID;
    }
}

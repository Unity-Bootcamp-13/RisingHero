using System.Collections.Generic;
using UnityEngine;

public class SkillEquip : MonoBehaviour
{
    private List<int> equippedSkillIds = new();
    private ISaveService saveService;
    private PlayerSaveData saveData;

    public void Initialize(ISaveService saveService)
    {
        this.saveService = saveService;
        this.saveData = saveService.Load();

        equippedSkillIds = saveData.equippedSkillIds ?? new List<int>();
    }

    public void EquipSkill(int skillId)
    {
        if (!equippedSkillIds.Contains(skillId))
        {
            equippedSkillIds.Add(skillId);
            saveData.equippedSkillIds = equippedSkillIds;
            saveService.Save(saveData);
        }
    }

    public void UnequipSkill(int skillId)
    {
        if (equippedSkillIds.Remove(skillId))
        {
            saveData.equippedSkillIds = equippedSkillIds;
            saveService.Save(saveData);
        }
    }

    public SkillData GetEquippedSkill(int slot)
    {
        if (slot < 0 || slot >= equippedSkillIds.Count) return null;
        return SkillLoader.Instance.GetSkillData(equippedSkillIds[slot]);
    }

    public IReadOnlyList<int> GetEquippedSkillIds() => equippedSkillIds.AsReadOnly();

    public void ReplaceSkillAtSlot(int slotIndex, int newSkillId)
    {
        if (slotIndex < 0 || slotIndex >= equippedSkillIds.Count)
        {
            return;
        }

        equippedSkillIds[slotIndex] = newSkillId;
        saveData.equippedSkillIds = equippedSkillIds;
        saveService.Save(saveData);
    }
}
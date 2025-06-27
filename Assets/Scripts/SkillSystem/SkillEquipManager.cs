using System.Collections.Generic;
using UnityEngine;

public class SkillEquipManager : MonoBehaviour
{
    public List<SkillBase> equippedSkills = new();

    public void Equip(SkillBase skill)
    {
        if (!equippedSkills.Contains(skill))
            equippedSkills.Add(skill);
    }

    public void Unequip(SkillBase skill)
    {
        equippedSkills.Remove(skill);
    }
}

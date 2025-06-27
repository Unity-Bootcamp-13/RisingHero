using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SkillDatabase : MonoBehaviour
{
    public List<SkillBase> allSkills;

    public SkillBase GetSkillById(int id)
    {
        return allSkills.FirstOrDefault(s => s.GetInstanceID() == id);
    }
}

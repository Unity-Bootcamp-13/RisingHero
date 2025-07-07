using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SkillLoader : MonoBehaviour
{
    public static SkillLoader Instance { get; private set; }

    private Dictionary<int, SkillData> skillDict;

    private void Awake()
    {
        Instance = this;
        var skills = CSVLoader.LoadTable<SkillData>("SkillData");
        skillDict = new();
        foreach (var skill in skills)
            skillDict[skill.ID] = skill;
    }

    public SkillData GetSkillData(int id) =>
        skillDict.TryGetValue(id, out var data) ? data : null;

    public List<SkillData> GetAllSkillData()
    {
        return skillDict.Values.ToList();
    }
}


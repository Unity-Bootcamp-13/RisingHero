using UnityEngine;

public class SkillLevelManager
{
    private ISaveService saveService;
    private PlayerSaveData saveData;

    public void Initialize(ISaveService saveService)
    {
        this.saveService = saveService;
        this.saveData = saveService.Load();
    }

    public int GetLevel(int skillId)
    {
        var entry = saveData.skillLevels.Find(x => x.skillId == skillId);
        return entry?.level ?? 1;
    }

    public bool LevelUp(int skillId)
    {
        int currentLevel = GetLevel(skillId);
        int cost = currentLevel * 100;

        if (saveData.coin < cost)
        {
            return false;
        }

        saveData.coin -= cost;
        SetLevel(skillId, currentLevel + 1);
        saveService.Save(saveData);

        return true;
    }

    private void SetLevel(int skillId, int level)
    {
        var entry = saveData.skillLevels.Find(x => x.skillId == skillId);
        if (entry != null)
        {
            entry.level = level;
        }
        else
        {
            saveData.skillLevels.Add(new SkillLevelData(skillId, level));
        }
    }

    public int GetCoin() => saveData.coin;
}

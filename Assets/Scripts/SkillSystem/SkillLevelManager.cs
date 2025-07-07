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
            Debug.Log("[SkillLevelManager] 코인이 부족합니다.");
            return false;
        }

        saveData.coin -= cost;
        SetLevel(skillId, currentLevel + 1);
        saveService.Save(saveData);

        Debug.Log($"[SkillLevelManager] 스킬 {skillId} 레벨업 완료 → Lv.{currentLevel + 1} (코인 {cost} 소모)");
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

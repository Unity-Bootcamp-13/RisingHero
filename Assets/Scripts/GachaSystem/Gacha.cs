using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Gacha
{
    private Dictionary<int, List<GachaWeightEntry>> weightTable = new();
    private ISaveService saveService;

    [SerializeField] public Quest quest;
    [SerializeField] private QuestManager questManager;
    public QuestType Type;
    private PlayerSaveData psd;

    public void Initialize(ISaveService saveService, QuestManager questManager)
    {
        this.saveService = saveService;
        this.questManager = questManager;
        LoadWeightTable();
    }

    private void LoadWeightTable()
    {
        var entries = CSVLoader.LoadTable<GachaWeightEntry>("WeightTable");
        weightTable = entries
            .GroupBy(e => e.groupId)
            .ToDictionary(g => g.Key, g => g.ToList());
    }

    public void SetQuest(Quest quest)
    {
        this.quest = quest;
    }

    public int RollGacha(int groupId)
    {
        if (!weightTable.ContainsKey(groupId))
        {
            return -1;
        }

        var entries = weightTable[groupId];
        int totalWeight = entries.Sum(e => e.weight);
        int roll = Random.Range(0, totalWeight);

        psd = saveService.Load();
        if (psd.currentQuestId == 3)
        {
            SetQuest(questManager.CurrentQuest);
            quest.AddProgress(1);
        }

        int cumulative = 0;

        if (groupId == 101)
        {
            foreach (var entry in entries)
            {
                cumulative += entry.weight;
                if (roll < cumulative)
                {
                    GrantWeapon(entry.itemId);
                    return entry.itemId;
                }
            }
        }
        else if (groupId == 201)
        {
            foreach (var entry in entries)
            {
                cumulative += entry.weight;
                if (roll < cumulative)
                {
                    GrantSkill(entry.itemId);
                    return entry.itemId;
                }
            }
        }

        return -1;
    }

    private void GrantWeapon(int weaponId)
    {
        Debug.Log($"[Gacha] ���� ���� �����: {weaponId}");

        var save = saveService.Load();
        var owned = save.ownedWeapons.Find(w => w.weaponId == weaponId);
        if (owned != null)
        {
            owned.amount++;
        }
        else
        {
            Debug.Log($"[Gacha] �ű� ���� �߰�: {weaponId}");
            save.ownedWeapons.Add(new OwnedWeapon(weaponId, 1, 1));
        }
        saveService.Save(save);
    }

    private void GrantSkill(int skillId)
    {
        var save = saveService.Load();
        var owned = save.skillLevels.Find(w => w.skillId == skillId);
        if (owned != null)
        {
            owned.amount++;
        }
        else
        {
            save.skillLevels.Add(new SkillLevelData(skillId, 1, 1));
        }
        saveService.Save(save);
    }
}

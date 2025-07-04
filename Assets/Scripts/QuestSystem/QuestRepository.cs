using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class QuestRepository
{
    private readonly string questListPath = Path.Combine(Application.streamingAssetsPath, "quest_list.json");
    private readonly string progressPath = Path.Combine(Application.persistentDataPath, "quest_progress.json");

    public List<Quest> LoadQuestList()
    {
        if (!File.Exists(questListPath))
        {
            Debug.LogError("quest_list.json 파일을 찾을 수 없습니다.");
            return new List<Quest>();
        }

        string json = File.ReadAllText(questListPath);
        QuestListWrapper wrapper = JsonUtility.FromJson<QuestListWrapper>(json);

        var quests = new List<Quest>();

        foreach (var dto in wrapper.quests)
        {
            var quest = new Quest(
                dto.questId,
                dto.questType,
                dto.goalValue,
                new RewardRange ( dto.rewardGold.Min, dto.rewardGold.Max),
                new RewardRange ( dto.rewardJewel.Min, dto.rewardJewel.Max)
            );
            quests.Add(quest);
        }

        return quests;
    }

    public void SaveQuestProgress(Quest quest)
    {
        QuestDTO dto = QuestDTO.FromQuest(quest);
        string json = JsonUtility.ToJson(dto, true);
        File.WriteAllText(progressPath, json);
    }

    public Quest LoadQuestProgress()
    {
        if (!File.Exists(progressPath))
            return null;

        string json = File.ReadAllText(progressPath);
        QuestDTO dto = JsonUtility.FromJson<QuestDTO>(json);

        return new Quest(
            dto.questId,
            dto.questType,
            dto.goalValue,
            dto.rewardGold,
            dto.rewardJewel
        );
    }

    [System.Serializable]
    private class QuestListWrapper
    {
        public List<QuestDTO> quests;
    }
}

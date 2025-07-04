using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class QuestRepository
{
    [System.Serializable]
    private class QuestListWrapper
    {
        public List<QuestDTO> quests;
    }

    private readonly string questListPath = Path.Combine(Application.streamingAssetsPath, "quest_list.json");

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
            quests.Add(new Quest(
                dto.questId,
                dto.questType,
                dto.goalValue,
                new RewardRange(dto.rewardGold.Min, dto.rewardGold.Max),
                new RewardRange(dto.rewardJewel.Min, dto.rewardJewel.Max)
            ));
        }
        return quests;
    }
}


using System.Collections.Generic;
using System.IO;
using UnityEngine;

[System.Serializable]
public class QuestListWrapper
{
    public List<QuestData> quests;
}

public class QuestManager : MonoBehaviour
{
    public List<QuestData> questList = new List<QuestData>();
    private int questIndex = 0;
    public QuestData currentQuest;
    private string savePath;

    private void Awake()
    {
        savePath = Path.Combine(Application.persistentDataPath, "quest.json");

        LoadQuestList();

        LoadQuestData();
    }

    private void LoadQuestList()
    {
        string questListJsonPath = Path.Combine(Application.streamingAssetsPath, "quest_list.json");

        if (File.Exists(questListJsonPath))
        {
            string json = File.ReadAllText(questListJsonPath);
            questList = JsonUtility.FromJson<QuestListWrapper>(json).quests;
            Debug.Log("퀘스트 리스트 불러오기 완료: " + questList.Count + "개");
        }
        else
        {
            Debug.LogError("퀘스트 리스트 파일 없음!");
            questList = new List<QuestData>();
        }
    }

    public void SaveQuestData()
    {
        string json = JsonUtility.ToJson(currentQuest, true);
        File.WriteAllText(savePath, json);
        Debug.Log("퀘스트 저장 완료");
    }

    public void LoadQuestData()
    {
        if (File.Exists(savePath))
        {
            string json = File.ReadAllText(savePath);
            currentQuest = JsonUtility.FromJson<QuestData>(json);
            Debug.Log("퀘스트 불러오기 완료");
        }
        else if (questList != null && questList.Count > 0)
        {
            currentQuest = questList[questIndex];
            Debug.Log("처음 퀘스트 설정 완료: " + currentQuest.questId);
            SaveQuestData();
        }
        else
        {
            Debug.LogError("퀘스트를 생성할 수 없습니다.");
        }
    }

    public QuestData GenerateNextQuest()
    {
        questIndex++;

        if (questList != null && questIndex < questList.Count)
        {
            return questList[questIndex];
        }

        Debug.LogWarning("퀘스트 목록 끝까지 도달함. 첫 퀘스트로 순환합니다.");
        questIndex = 0;
        return questList[0];
    }


    public (int gold, int diamond) CalculateReward(QuestData currentQuest)
    {
        int exp = 50 * 10;
        int gold = 100 * 15;
        int diamond = 50 * 10;
        return (gold, diamond);
    }
}

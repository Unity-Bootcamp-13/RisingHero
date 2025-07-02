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
            Debug.Log("����Ʈ ����Ʈ �ҷ����� �Ϸ�: " + questList.Count + "��");
        }
        else
        {
            Debug.LogError("����Ʈ ����Ʈ ���� ����!");
            questList = new List<QuestData>();
        }
    }

    public void SaveQuestData()
    {
        string json = JsonUtility.ToJson(currentQuest, true);
        File.WriteAllText(savePath, json);
        Debug.Log("����Ʈ ���� �Ϸ�");
    }

    public void LoadQuestData()
    {
        if (File.Exists(savePath))
        {
            string json = File.ReadAllText(savePath);
            currentQuest = JsonUtility.FromJson<QuestData>(json);
            Debug.Log("����Ʈ �ҷ����� �Ϸ�");
        }
        else if (questList != null && questList.Count > 0)
        {
            currentQuest = questList[questIndex];
            Debug.Log("ó�� ����Ʈ ���� �Ϸ�: " + currentQuest.questId);
            SaveQuestData();
        }
        else
        {
            Debug.LogError("����Ʈ�� ������ �� �����ϴ�.");
        }
    }

    public QuestData GenerateNextQuest()
    {
        questIndex++;

        if (questList != null && questIndex < questList.Count)
        {
            return questList[questIndex];
        }

        Debug.LogWarning("����Ʈ ��� ������ ������. ù ����Ʈ�� ��ȯ�մϴ�.");
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

using System.IO;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public QuestData currentQuest;
    private string savePath;

    private void Awake()
    {
        savePath = Path.Combine(Application.persistentDataPath, "quest.json");
        LoadQuestData();
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
        else
        {
            Debug.Log("저장된 퀘스트 없음. 새 퀘스트 생성");
            currentQuest = GenerateNextQuest();
        }
    }

    public QuestData GenerateNextQuest()
    {
        int nextId = currentQuest != null ? currentQuest.questId + 1 : 1;
        QuestType newQuestType = GetRandomQuestType();

        int goal = GetGoalValueForType(newQuestType);
        (int exp, int gold, int diamond) = CalculateReward(currentQuest);

        return new QuestData
        {
            questId = nextId,
            questType = newQuestType,
            goalValue = goal,
            currentValue = 0,
            rewardExp = exp,
            rewardGold = gold,
            rewardJewel = diamond,
            isCompleted = false
        };
    }

    private QuestType GetRandomQuestType()
    {
        return (QuestType)Random.Range(0, System.Enum.GetValues(typeof(QuestType)).Length);
    }

    private int GetGoalValueForType(QuestType type)
    {
        switch (type)
        {
            case QuestType.Kill:
                return Random.Range(10, 51); // 10~50
            case QuestType.Upgrade:
                return Random.Range(1, 11);  // 1~10
            case QuestType.Gacha:
                return 1;
            default:
                return 1;
        }
    }

    public (int exp, int gold, int diamond) CalculateReward(QuestData currentQuest)
    {
        int exp = 50 * 10;
        int gold = 100 * 15;
        int diamond = 50 * 10;
        return (exp, gold, diamond);
    }
}

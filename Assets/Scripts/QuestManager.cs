using System.IO;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public QuestData currentQuest;
    private string savePath;

    private void Awake()
    {
        savePath = Application.persistentDataPath + "/quest.json";
        LoadQuestData();
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
        else
        {
            Debug.Log("����� ����Ʈ ����. �� ����Ʈ ����");
            currentQuest = GenerateNextQuest(); // ���� ��� �� ����Ʈ ����
        }
    }

    public QuestData GenerateNextQuest()
    {
        int nextId = currentQuest != null ? currentQuest.questId + 1 : 1;

        // ����Ʈ Ÿ�� ������ ����
        QuestType newQuestType = (QuestType)Random.Range(0, System.Enum.GetValues(typeof(QuestType)).Length);

        int goal = 0;

        switch (newQuestType)
        {
            case QuestType.Kill:
                goal = Random.Range(10, 51); // 10~50 ���� óġ
                break;

            case QuestType.Upgrade:
                goal = Random.Range(1, 11); // ��� ������ 1~10ȸ
                break;

            case QuestType.Gacha:
                goal = 1; // �̱� ��ư 1ȸ Ŭ��
                break;
        }

        return new QuestData
        {
            questId = nextId,
            questType = newQuestType.ToString(),
            goalValue = goal,
            currentValue = 0,
            rewardExp = 50 + nextId * 10,
            rewardGold = 100 + nextId * 15,
            isCompleted = false
        };
    }

}

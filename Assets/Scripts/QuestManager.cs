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
            currentQuest = GenerateNextQuest(); // 없는 경우 새 퀘스트 생성
        }
    }

    public QuestData GenerateNextQuest()
    {
        int nextId = currentQuest != null ? currentQuest.questId + 1 : 1;

        // 퀘스트 타입 무작위 선택
        QuestType newQuestType = (QuestType)Random.Range(0, System.Enum.GetValues(typeof(QuestType)).Length);

        int goal = 0;

        switch (newQuestType)
        {
            case QuestType.Kill:
                goal = Random.Range(10, 51); // 10~50 마리 처치
                break;

            case QuestType.Upgrade:
                goal = Random.Range(1, 11); // 장비 레벨업 1~10회
                break;

            case QuestType.Gacha:
                goal = 1; // 뽑기 버튼 1회 클릭
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

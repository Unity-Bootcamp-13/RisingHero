/*using UnityEngine;

public interface IStageSystem
{
    void LoadStage();
    void SaveStage(string stage);
    PlayerSaveData LoadData();
}

public class StageSystem : MonoBehaviour, IStageSystem
{
    private GameObject currentStageInstance;
    private ISaveService saveService;

    PlayerSaveData saveData;

    public void Initialize(ISaveService saveService)
    {
        this.saveService = saveService;
    }


    private void Start()
    {
        *//*if (saveService == null)
        {
            Debug.LogError("[StageSystem] SaveService�� �ʱ�ȭ���� �ʾҽ��ϴ�.");
            return;
        }

        saveData = saveService.Load();
        LoadStage();*//*
    }

    public void LoadStage()
    {
        string stageName = saveData.currentStage;

        *//*GameObject stagePrefab = Resources.Load<GameObject>($"Tilemaps/{stageName}");

        if (stagePrefab == null)
        {
            Debug.LogError($"[StageSystem] �������� ã�� �� �����ϴ�: Tilemaps/{stageName}");
            return;
        }

        if (currentStageInstance != null)
        {
            Destroy(currentStageInstance);
        }

        currentStageInstance = Instantiate(stagePrefab, Vector3.zero, Quaternion.identity);*//*
    }

    public void SaveStage(string stage)
    {
        saveData.currentStage = stage;
        saveService.Save(saveData);
    }

    public PlayerSaveData LoadData()
    {
        return saveService.Load();
    }
}
*/

using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public interface IStageSystemReceiver
{
    void ApplyStageSettings(float timeLimit, int killGoal);
    void ApplyStageTilemap(GameObject stagePrefab);
}

public class StageSystem : MonoBehaviour, IStageSystemReceiver
{
    [SerializeField] private StageKillCountTimer UIStageKillCountTimer;
    [SerializeField] private StageUIManager stageUIManager;

    private IStageService stageService;
    private IUserStageService userStageService;
    private IUserCurrentStageService userCurrentStageService;
    private IStageType stageTypeSet;

    private int currentStageId;
    private StageData currentStageData;
    private List<UserStageData> userStageDataList;
    private UserCurrentStageData userCurrentStageData;
    private StageType currentStageType;

    private float timeLimit;
    private int killCount;
    private int killGoal;
    private bool isStageCleared = false;

    private void Awake()
    {
        SetStageData();
        // UI
        UIStageKillCountTimer.Show(currentStageType == StageType.Gate);
    }

    private void Start()
    {
        killCount = 0;
        stageUIManager.InitializeButtons(stageService, userStageService, userCurrentStageService);
    }

    void OnApplicationQuit()
    {
        // ���� ���� �� �������� ������ ����
        timeLimit = 0;
        stageTypeSet.Defeat(timeLimit);
    }

    /*void OnApplicationPause(bool pause)
    {
        // ������ �Ͻ������ǰų� ��׶���� �̵��� �� ȣ���
        if (pause)
        {
            SaveAsForcedExit(); // Ȩ��ư, ��׶��� �̵� �� ���� ����� ����
        }
    }*/

    public void Update()
    {
        if (currentStageType == StageType.Normal) // �븻�̸� ���ʿ��� ������Ʈ �Լ��� �������� �ʰ�
            return;
        if (timeLimit <= 0f)
            return;

        timeLimit -= Time.deltaTime;
        UpdateUI();

        stageTypeSet.Clear(killCount, userStageDataList);
        stageTypeSet.Defeat(timeLimit);
    }

    // ================== �������̽� ========================== //

    public void ApplyStageSettings(float timeLimit, int killGoal)
    {
        this.timeLimit = timeLimit;
        this.killGoal = killGoal;
    }

    public void ApplyStageTilemap(GameObject stagePrefab)
    {
        stageUIManager.SetTileMap(stagePrefab);
    }

    // ===================  �޼���  =========================== //
    public void KillEnemy()
    {
        killCount++;
    }

    public void UpdateUI()
    {
        UIStageKillCountTimer.UpdateKillCount(killCount, killGoal);
        UIStageKillCountTimer.UpdateTime(timeLimit);
    }

    public void SetStageData()
    {
        stageService = StageManager.StageService;
        userStageService = StageManager.UserStageService;
        userCurrentStageService = StageManager.UserCurrentStageService;

        SetCurrentStageData();
        DesignStageType();
    }

    public void SetCurrentStageData()
    {
        currentStageId = userCurrentStageService.GetUserCurrentStageID(); // ���� �������� ID ��������
        currentStageData = stageService.GetStageList()[currentStageId]; // ���� �������� ������ ��������

        userStageDataList = userStageService.GetUserStageList().ToList(); // ���� �������� ������ ����Ʈ ��������
        currentStageType = userStageDataList[currentStageId].stage_type; // ���� �������� Ÿ�� ��������
    }

    public void DesignStageType()
    {
        stageTypeSet = StageTypeFactory.SetStageType(currentStageType);

        stageTypeSet.SetStageProperties(currentStageData, userStageDataList, this); // �ڽ��� ����
    }

}




public class StageTypeFactory
{
    public static IStageType SetStageType(StageType stageType)
    {
        switch (stageType)
        {
            case StageType.Normal:
                return new NormalStageType();
            case StageType.Gate:
                return new GateStageType();
            case StageType.Boss:
                return new BossStageType();
            default:
                throw new System.ArgumentException("Invalid stage type");
        }
    }
}
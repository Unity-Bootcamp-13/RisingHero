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
            Debug.LogError("[StageSystem] SaveService가 초기화되지 않았습니다.");
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
            Debug.LogError($"[StageSystem] 프리팹을 찾을 수 없습니다: Tilemaps/{stageName}");
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
        // 게임 종료 시 스테이지 데이터 저장
        timeLimit = 0;
        stageTypeSet.Defeat(timeLimit);
    }

    /*void OnApplicationPause(bool pause)
    {
        // 게임이 일시정지되거나 백그라운드로 이동할 때 호출됨
        if (pause)
        {
            SaveAsForcedExit(); // 홈버튼, 백그라운드 이동 → 강제 종료로 간주
        }
    }*/

    public void Update()
    {
        if (currentStageType == StageType.Normal) // 노말이면 불필요한 업데이트 함수를 시행하지 않게
            return;
        if (timeLimit <= 0f)
            return;

        timeLimit -= Time.deltaTime;
        UpdateUI();

        stageTypeSet.Clear(killCount, userStageDataList);
        stageTypeSet.Defeat(timeLimit);
    }

    // ================== 인터페이스 ========================== //

    public void ApplyStageSettings(float timeLimit, int killGoal)
    {
        this.timeLimit = timeLimit;
        this.killGoal = killGoal;
    }

    public void ApplyStageTilemap(GameObject stagePrefab)
    {
        stageUIManager.SetTileMap(stagePrefab);
    }

    // ===================  메서드  =========================== //
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
        currentStageId = userCurrentStageService.GetUserCurrentStageID(); // 현재 스테이지 ID 가져오기
        currentStageData = stageService.GetStageList()[currentStageId]; // 현재 스테이지 데이터 가져오기

        userStageDataList = userStageService.GetUserStageList().ToList(); // 유저 스테이지 데이터 리스트 가져오기
        currentStageType = userStageDataList[currentStageId].stage_type; // 현재 스테이지 타입 가져오기
    }

    public void DesignStageType()
    {
        stageTypeSet = StageTypeFactory.SetStageType(currentStageType);

        stageTypeSet.SetStageProperties(currentStageData, userStageDataList, this); // 자신을 전달
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
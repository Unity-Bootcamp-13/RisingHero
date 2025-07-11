using UnityEngine;

public class StageClear : MonoBehaviour
{
    [SerializeField] private EliteStage eliteStage;
    [SerializeField] private BossStage bossStage;
    [SerializeField] private StageUI stageUI;
    [SerializeField] private PlayerHealth playerHealth;

    private ISaveService saveService;
    private bool isFinishi;

    public void Initialize(ISaveService saveService)
    {
        this.saveService = saveService;
    }

    private void Awake()
    {
        if (eliteStage != null)
        {
            eliteStage.OnClear += OnStageClear;
            eliteStage.OnFail += OnStageFail;
        }

        if (bossStage != null)
        {
            bossStage.OnClear += OnBossStageClear;
            bossStage.OnFail += OnStageFail;
        }

        if (playerHealth != null)
        {
            playerHealth.OnDie += OnStageFail;
        }
    }

    private void Start()
    {
        isFinishi = false;
    }

    private void OnDestroy()
    {
        if (eliteStage != null)
        {
            eliteStage.OnClear -= OnStageClear;
            eliteStage.OnFail -= OnStageFail;
        }

        if (playerHealth != null)
        {
            playerHealth.OnDie -= OnStageFail;
        }
    }

    private void OnStageClear()
    {
        if (saveService == null)
        {
            return;
        }
        if (isFinishi)
            return;

        isFinishi = true;


        var saveData = saveService.Load();
        if (saveData.currentStage == 28)
        {
            stageUI.ShowClearAllStageWindow();
            return;
        }

        saveData.topStage += 1;
        saveData.diamond += 3000;
        saveService.Save(saveData);

        CoinBuffer.Instance.Initialize(saveService);


        if (stageUI != null)
            stageUI.ShowClearWindow();

        Time.timeScale = 0f;
    }

    private void OnStageFail()
    {
        if (isFinishi)
            return;

        isFinishi = true;

        if (stageUI != null)
            stageUI.ShowDefeatWindow();

        Time.timeScale = 0f;
    }

    private void OnBossStageClear()
    {
        if (saveService == null)
        {
            return;
        }
        if (isFinishi)
            return;

        isFinishi = true;


        var saveData = saveService.Load();

        if (saveData.currentStage == 29)
        {
            saveData.topStage += 1;
            stageUI.ShowClearAllStageWindow();
            return;
        }

        saveData.topStage += 3;
        saveData.diamond += 10000;
        saveService.Save(saveData);

        if (stageUI != null)
            stageUI.ShowClearBossWindow();

        Time.timeScale = 0f;
    }
}

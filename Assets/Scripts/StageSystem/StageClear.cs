using UnityEngine;

public class StageClear : MonoBehaviour
{
    [SerializeField] private EliteStage eliteStage;
    [SerializeField] private BossStage bossStage;
    [SerializeField] private StageUI stageUI;
    [SerializeField] private CharacterHealth playerHealth;

    private ISaveService saveService;

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

        var saveData = saveService.Load();
        saveData.topStage += 1;
        saveService.Save(saveData);

        CoinBuffer.Instance.Initialize(saveService);

        if (stageUI != null)
            stageUI.ShowClearWindow();
    }

    private void OnStageFail()
    {
        if (stageUI != null)
            stageUI.ShowDefeatWindow();
    }

    private void OnBossStageClear()
    {
        if (saveService == null)
        {
            return;
        }


        var saveData = saveService.Load();

        if (saveData.currentStage == 29)
        {
            saveData.topStage += 1;
            stageUI.ShowClearAllStageWindow();
            return;
        }

        saveData.topStage += 3;
        saveService.Save(saveData);

        if (stageUI != null)
            stageUI.ShowClearBossWindow();
    }
}

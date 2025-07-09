using UnityEngine;

public class StageClear : MonoBehaviour
{
    [SerializeField] private EliteStage eliteStage;
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
        Debug.Log("[StageClear] 엘리트 스테이지 클리어!");
        if (saveService == null)
        {
            Debug.LogError("[StageClear] SaveService가 할당되지 않았습니다.");
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
        Debug.Log("[StageClear] 엘리트 스테이지 실패!");
        if (stageUI != null)
            stageUI.ShowDefeatWindow();
    }
}

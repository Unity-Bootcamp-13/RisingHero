using UnityEngine;

public class StageClear : MonoBehaviour
{
    [SerializeField] private EliteStage eliteStage;
    [SerializeField] private StageUI stageUI;

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
            return;
        }

            Debug.LogError("[StageClear] EliteStage가 연결되지 않았습니다.");
    }

    private void OnDestroy()
    {
        if (eliteStage != null)
        {
            eliteStage.OnClear -= OnStageClear;
            eliteStage.OnFail -= OnStageFail;
        }
    }

    private void OnStageClear()
    {
        Debug.Log("[StageClear] 엘리트 스테이지 클리어!");
        // 추가 클리어 처리 작성 가능
        if (saveService == null)
        {
            Debug.LogError("[StageClear] SaveService가 할당되지 않았습니다.");
            return;
        }

        var saveData = saveService.Load();
        saveData.topStage += 1;
        saveService.Save(saveData);

        //  CoinBuffer에 최신 SaveData를 반영하도록 명시적으로 갱신
        CoinBuffer.Instance.Initialize(saveService);  // 안전하게 서비스 재주입

        // 클리어 UI 띄우기
        if (stageUI != null)
            stageUI.ShowClearWindow();
    }

    private void OnStageFail()
    {
        Debug.Log("[StageClear] 엘리트 스테이지 실패!");
        // 추가 실패 처리 작성 가능
        // 클리어 UI 띄우기
        if (stageUI != null)
            stageUI.ShowDefeatWindow();
    }
}

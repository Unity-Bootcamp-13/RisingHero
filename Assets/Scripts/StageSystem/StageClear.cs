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

            Debug.LogError("[StageClear] EliteStage�� ������� �ʾҽ��ϴ�.");
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
        Debug.Log("[StageClear] ����Ʈ �������� Ŭ����!");
        // �߰� Ŭ���� ó�� �ۼ� ����
        if (saveService == null)
        {
            Debug.LogError("[StageClear] SaveService�� �Ҵ���� �ʾҽ��ϴ�.");
            return;
        }

        var saveData = saveService.Load();
        saveData.topStage += 1;
        saveService.Save(saveData);

        //  CoinBuffer�� �ֽ� SaveData�� �ݿ��ϵ��� ��������� ����
        CoinBuffer.Instance.Initialize(saveService);  // �����ϰ� ���� ������

        // Ŭ���� UI ����
        if (stageUI != null)
            stageUI.ShowClearWindow();
    }

    private void OnStageFail()
    {
        Debug.Log("[StageClear] ����Ʈ �������� ����!");
        // �߰� ���� ó�� �ۼ� ����
        // Ŭ���� UI ����
        if (stageUI != null)
            stageUI.ShowDefeatWindow();
    }
}

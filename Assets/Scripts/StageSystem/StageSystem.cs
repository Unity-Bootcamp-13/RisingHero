using UnityEngine;

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
        /*if (saveService == null)
        {
            Debug.LogError("[StageSystem] SaveService�� �ʱ�ȭ���� �ʾҽ��ϴ�.");
            return;
        }

        saveData = saveService.Load();
        LoadStage();*/
    }

    public void LoadStage()
    {
        string stageName = saveData.currentStage;

        /*GameObject stagePrefab = Resources.Load<GameObject>($"Tilemaps/{stageName}");

        if (stagePrefab == null)
        {
            Debug.LogError($"[StageSystem] �������� ã�� �� �����ϴ�: Tilemaps/{stageName}");
            return;
        }

        if (currentStageInstance != null)
        {
            Destroy(currentStageInstance);
        }

        currentStageInstance = Instantiate(stagePrefab, Vector3.zero, Quaternion.identity);*/
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

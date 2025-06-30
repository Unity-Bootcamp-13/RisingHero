using UnityEngine;

public class StageSystem : MonoBehaviour
{
    private GameObject currentStageInstance;
    private ISaveService saveService;

    public void Initialize(ISaveService saveService)
    {
        this.saveService = saveService;
    }

    private void Start()
    {
        if (saveService == null)
        {
            Debug.LogError("[StageSystem] SaveService가 초기화되지 않았습니다.");
            return;
        }

        LoadStage();
    }

    void LoadStage()
    {
        PlayerSaveData saveData = saveService.Load();
        string stageName = saveData.currentStage;

        GameObject stagePrefab = Resources.Load<GameObject>($"Tilemaps/{stageName}");

        if (stagePrefab == null)
        {
            Debug.LogError($"[StageSystem] 프리팹을 찾을 수 없습니다: Tilemaps/{stageName}");
            return;
        }

        if (currentStageInstance != null)
        {
            Destroy(currentStageInstance);
        }

        currentStageInstance = Instantiate(stagePrefab, Vector3.zero, Quaternion.identity);
    }
}

using UnityEngine;

public class StageSystem : MonoBehaviour
{
    private GameObject currentStageInstance;

    private void Start()
    {
        LoadStage();
    }

    void LoadStage()
    {
        PlayerSaveData saveData = SaveManager.Load();
        string stageName = saveData.currentStage;

        // Resources/Tilemaps/{stageName}.prefab 로 프리팹 로드
        GameObject stagePrefab = Resources.Load<GameObject>($"Tilemaps/{stageName}");

        if (stagePrefab == null)
        {
            Debug.LogError($"[StageSystem] 프리팹을 찾을 수 없습니다: Tilemaps/{stageName}");
            return;
        }

        // 기존 인스턴스 제거
        if (currentStageInstance != null)
        {
            Destroy(currentStageInstance);
        }

        // 새 스테이지 프리팹 인스턴스 생성
        currentStageInstance = Instantiate(stagePrefab, Vector3.zero, Quaternion.identity);
    }
}

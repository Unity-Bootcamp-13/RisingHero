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

        // Resources/Tilemaps/{stageName}.prefab �� ������ �ε�
        GameObject stagePrefab = Resources.Load<GameObject>($"Tilemaps/{stageName}");

        if (stagePrefab == null)
        {
            Debug.LogError($"[StageSystem] �������� ã�� �� �����ϴ�: Tilemaps/{stageName}");
            return;
        }

        // ���� �ν��Ͻ� ����
        if (currentStageInstance != null)
        {
            Destroy(currentStageInstance);
        }

        // �� �������� ������ �ν��Ͻ� ����
        currentStageInstance = Instantiate(stagePrefab, Vector3.zero, Quaternion.identity);
    }
}

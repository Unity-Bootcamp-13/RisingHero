using System.Collections.Generic;
using UnityEngine;

public class StageUIManager : MonoBehaviour
{
    [SerializeField] private GameObject currentStageTilePrefab;

    private Dictionary<int, UserStageData> stageDict;

    public void SetTileMap(GameObject stagePrefab)
    {
        if (currentStageTilePrefab != null)
        {
            Destroy(currentStageTilePrefab);
        }

        Instantiate(stagePrefab, Vector3.zero, Quaternion.identity);
    }


    public void InitializeButtons(IStageService stageService, IUserStageService userStageService, IUserCurrentStageService userCurrentStageService)
    {
        List<UserStageData> stageList = userStageService.GetUserStageList() as List<UserStageData>;
        if (stageList == null)
        {
            Debug.LogError("Stage list�� List<UserStageData> Ÿ������ ��ȯ�� �� �����ϴ�.");
            return;
        }

        stageDict = new Dictionary<int, UserStageData>(stageList.Count);

        foreach (UserStageData stage in stageList)
        {
            stageDict[stage.user_stage_id] = stage;
        }

        IReadOnlyList<StageButton> allButtons = StageManager.StageButtonRegistry.GetAll();

        foreach (StageButton btn in allButtons)
        {
            UserStageData data;
            if (stageDict.TryGetValue(btn.StageId, out data))
            {
                bool islocked = !userStageService.BlockMoveToStage(btn.StageId);
                btn.Initialize(data, islocked, stageService, userStageService, userCurrentStageService);
            }
            else
            {
                Debug.LogWarning("Stage ID " + btn.StageId + "�� �ش��ϴ� �����Ͱ� �����ϴ�.");
            }
        }
    }
}

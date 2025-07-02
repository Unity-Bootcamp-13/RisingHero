using Unity.VisualScripting;
using UnityEngine;

public class StageButton : MonoBehaviour
{
    [SerializeField] private int stageId;
    [SerializeField] GameObject BlockStagePannel;

    private IStageService stageService;



    private void Start()
    {
        stageService = new StageService();

        if (stageService.BlockMoveToStage(stageId))
        {
            BlockStagePannel.SetActive(true);
        }
        else
        {
            BlockStagePannel.SetActive(false);
        }
    }

    public void OnClick()
    {
        if (stageService.BlockMoveToStage(stageId))
        {
            //StageEventBus.ShowBlockedStageMove(stageId);
            Debug.Log($"Stage {stageId} is blocked. Cannot move to this stage.");
            return;
        }
        StageEventBus.StageMoved(stageId);
    }
}

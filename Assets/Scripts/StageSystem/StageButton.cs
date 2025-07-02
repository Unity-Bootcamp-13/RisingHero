using Unity.VisualScripting;
using UnityEngine;

public class StageButton : MonoBehaviour
{
    [SerializeField] private int stageId;
    [SerializeField] GameObject BlockStagePannel;

    private IStageService stageService;
    private IUserCurrentStageService userCurrentStageService;
    private IUserStageService userStageService;



    private void Start()
    {
        userStageService = new UserStageService();
        userCurrentStageService = new UserCurrentStageService();

        if (userStageService.BlockMoveToStage(stageId))
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
        if (userStageService.BlockMoveToStage(stageId))
        {
            //StageEventBus.ShowBlockedStageMove(stageId);
            Debug.Log($"Stage {stageId} is blocked. Cannot move to this stage.");
            return;
        }
        userCurrentStageService.SaveCurrentStage(stageId);
    }
}

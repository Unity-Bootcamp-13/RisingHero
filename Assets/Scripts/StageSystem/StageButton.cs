using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class StageButton : MonoBehaviour
{
    [SerializeField] private int stageId;
    public int StageId => stageId;
    [SerializeField] GameObject BlockStagePannel;

    private Button button;
    private IStageState state;

    private IStageService stageService;
    private IUserCurrentStageService userCurrentStageService;
    private IUserStageService userStageService;

    private void Awake()
    {
        button = GetComponent<Button>();
        StageManager.StageButtonRegistry.Register(this);
    }



    public void Initialize(UserStageData data, bool islocked,
        IStageService stageService,
        IUserStageService userStageService,
        IUserCurrentStageService userCurrentStageService)
    {
        this.stageService = stageService;
        this.userStageService = userStageService;
        this.userCurrentStageService = userCurrentStageService;

        if (islocked)
        {
            state = new ActiveStageState(button, data, stageService);
            BlockStagePannel.SetActive(false);
        }
        else
        {
            state = new InActiveStageState(button);
            BlockStagePannel.SetActive(true);
        }
        state.SetButtonState();
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

using UnityEngine;

public class StageManager : MonoBehaviour
{
    private IStageService stageService;
    private IUserCurrentStageService currentStageService;

    private void Start()
    {
        stageService = new StageService();
        currentStageService = new UserCurrentStageService();
    }
}

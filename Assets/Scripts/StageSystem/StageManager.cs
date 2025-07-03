using UnityEngine;

public class StageManager : MonoBehaviour
{
    public static IStageService StageService { get; private set; }
    public static IUserStageService UserStageService { get; private set; }
    public static IUserCurrentStageService UserCurrentStageService { get; private set; }
    public static IStageButtonRegistry StageButtonRegistry { get; private set; }

    private void Awake()
    {
        StageService = new StageService();
        UserStageService = new UserStageService();
        UserCurrentStageService = new UserCurrentStageService();
        StageButtonRegistry = new StageButtonRegistry();
        StageManager.StageButtonRegistry.Clear();
    }
}

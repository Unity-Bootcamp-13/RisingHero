using UnityEngine;
using UnityEngine.SceneManagement;

public interface IStageSceneLoader
{
    void LoadStage(int stageNumber, int topStage);
}

public class StageSceneLoader : IStageSceneLoader
{
    private ISlideScene slideScene;
    private ISaveService saveService;

    public StageSceneLoader()
    {
        saveService = new JsonSaveService();
    }

    public void LoadStage(int stageNumber, int topStage)
    {
        string sceneName = ResolveSceneName(stageNumber, topStage);

        if (Application.CanStreamedLevelBeLoaded(sceneName))
        {
            // 슬라이드 연출 객체 획득
            slideScene = Object.FindFirstObjectByType<SlideScene>();

            if (slideScene != null)
            {
                slideScene.LodeSceneWithSlide(sceneName);
            }
        }
    }

    private string ResolveSceneName(int stageNumber, int topStage)
    {
        if (IsBoss(stageNumber)) return "BossStage";
        if (IsElite(stageNumber, topStage)) return "EliteStage";

        return "Stage1";
    }

    private bool IsBoss(int stage) => stage % 10 == 9;
    private bool IsElite(int stage, int topStage) => stage == topStage + 1;
}

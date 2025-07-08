using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public interface IStageSceneLoader
{
    void LoadStage(int stageNumber, int topStage);
}


public class StageSceneLoader : IStageSceneLoader
{
    private ISlideScene slideScene;

    public void LoadStage(int stageNumber, int topStage)
    {
        string sceneName = ResolveSceneName(stageNumber, topStage);

        if (Application.CanStreamedLevelBeLoaded(sceneName))
        {
            Debug.Log($"[StageSceneLoader] 씬 이동: {sceneName}");
            ISlideScene slideScene = GameObject.FindObjectOfType<SlideScene>();
            //SceneManager.LoadScene(sceneName);
            slideScene.LodeSceneWithSlide(sceneName);
        }
        else
        {
            Debug.LogError($"[StageSceneLoader] 씬 '{sceneName}' 을(를) 찾을 수 없습니다. 빌드 세팅을 확인하세요.");
        }
    }

    private string ResolveSceneName(int stageNumber, int topStage)
    {
        if(IsBoss(stageNumber))
            return "BossStage";
        if (IsElite(stageNumber, topStage))
            return "EliteStage";

        // 일반 스테이지는 홀수 → Stage1, 짝수 → Stage2
        return stageNumber % 2 == 1 ? "Stage1" : "Stage2";
    }

    private bool IsBoss(int stage) => stage % 10 == 0; // 보스 스테이지는 10의 배수
    private bool IsElite(int stage,int topStage) => stage == topStage + 1; // 엘리트 스테이지는 최상위 스테이지 다음 단계
}

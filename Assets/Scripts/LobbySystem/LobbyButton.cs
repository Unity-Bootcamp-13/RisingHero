
using UnityEngine;

public class LobbyButton : MonoBehaviour
{
    private ISaveService saveService;
    private IStageSceneLoader stageSceneLoader;

    public void Initialize(ISaveService saveService, IStageSceneLoader stageSceneLoader)
    {
        this.saveService = saveService;
        this.stageSceneLoader = stageSceneLoader;
    }

    public void onClickStart()
    {
        var saveData = saveService.Load();

        stageSceneLoader.LoadStage(saveData.currentStage, saveData.topStage);
    }
}

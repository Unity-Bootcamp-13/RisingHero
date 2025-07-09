
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
        if (saveData.currentStage > saveData.topStage)
        {
            saveData.currentStage = saveData.topStage;
            saveService.Save(saveData);
        }


        stageSceneLoader.LoadStage(saveData.currentStage, saveData.topStage);
    }
}

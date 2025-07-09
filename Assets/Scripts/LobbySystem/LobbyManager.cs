
using UnityEngine;

public class LobbyManager : MonoBehaviour
{
    [SerializeField] private LobbyButton lobbyButton;

    private ISaveService saveService;
    private IStageSceneLoader stageSceneLoader;

    private void Awake()
    {
        saveService = new JsonSaveService();
        stageSceneLoader = new StageSceneLoader();
        
        lobbyButton.Initialize(saveService, stageSceneLoader);
    }
}


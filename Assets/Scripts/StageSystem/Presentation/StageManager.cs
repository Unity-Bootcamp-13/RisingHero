using UnityEngine;

public class StageManager : MonoBehaviour
{
    [SerializeField] private MoveStage moveStage;
    [SerializeField] private StageSystem stageSystem;
    private ISaveService saveService;

    private void Awake()
    {
        saveService = new JsonSaveService();

        stageSystem.Initialize(saveService);
        moveStage.Initialize(stageSystem);
    }
}

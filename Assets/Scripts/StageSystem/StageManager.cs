using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    [Header("스테이지 시스템")]
    [SerializeField] private TileSpawner tileSpawner;
    [SerializeField] private EnemySpawner enemySpawner;

    [Header("스테이지 버튼 목록")]
    [SerializeField] private List<StageButton> stageButtons;

    [Header("클리어 시스템")]
    [SerializeField] private StageClear stageClear;

    [Header("스테이지 클리어 및 패배 UI")]
    [SerializeField] private StageUI stageUI;

    private ISaveService saveService;
    private IStageSceneLoader stageSceneLoader;

    private void Awake()
    {
        saveService = new JsonSaveService();
        stageSceneLoader = new StageSceneLoader();

        tileSpawner.Initialize(saveService);
        enemySpawner.Initialize(saveService);

        if (stageClear != null && stageUI != null)
        {
            stageClear.Initialize(saveService);
            stageUI.Initialize(saveService, stageSceneLoader);
        }

        foreach (var button in stageButtons)
            button.Initialize(saveService, stageSceneLoader);
    }
}
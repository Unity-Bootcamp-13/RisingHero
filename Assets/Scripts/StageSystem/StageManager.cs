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

    private ISaveService saveService;

    private void Awake()
    {
        saveService = new JsonSaveService();

        tileSpawner?.Initialize(saveService);
        enemySpawner?.Initialize(saveService);
        stageClear?.Initialize(saveService);

        foreach (var button in stageButtons)
            button?.Initialize(saveService);
    }
}
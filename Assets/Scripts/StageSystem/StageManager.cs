using UnityEngine;
using System.Collections.Generic;

public class StageManager : MonoBehaviour
{
    [Header("���� �ý���")]
    [SerializeField] private TileSpawner tileSpawner;
    [SerializeField] private EnemySpawner enemySpawner;

    [Header("�������� ��ư ���")]
    [SerializeField] private List<StageButton> stageButtons;

    [Header("Ŭ���� �ý���")]
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

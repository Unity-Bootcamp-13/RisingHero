using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


class MoveStage : MonoBehaviour
{
    private IStageSystem stageSystem;
    private PlayerSaveData data;
    private List<StageData> stageList;

    private IStageDataRepository stageRepository;

    public void Initialize(IStageSystem stageSystem)
    {
        this.stageSystem = stageSystem;

        var parser = new Parser<StageDataList>("StageTable.json");
        stageRepository = new StageDataRepository(parser);

    }

    private void OnEnable()
    {
        StageEventBus.OnStage += MoveToStage;
    }

    private void OnDisable()
    {
        StageEventBus.OnStage -= MoveToStage;
    }

    private void Start()
    {
        data = stageSystem.LoadData();
        stageList = stageRepository.FindAll().ToList();


        Debug.Log($"Stage {stageList[0].stage_name}");
    }

    public void MoveToStage(int stageId)
    {
        stageSystem.SaveStage(stageList[stageId].stage_name);
    }
}


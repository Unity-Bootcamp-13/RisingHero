using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


/// <summary>
/// 서비스로 옮기면서 의미가 퇴색됨
/// </summary>
class MoveStage : MonoBehaviour
{
    private IStageService stageService;
    private List<StageData> stageList;

    public void Initialize(IStageSystem stageSystem)
    {
        this.stageService = new StageService(stageSystem);
    }

    private void OnEnable()
    {
        StageEventBus.OnStageMoved += stageService.MoveToStage;
    }
    private void OnDisable()
    {
        StageEventBus.OnStageMoved -= stageService.MoveToStage;
    }


    private void Start()
    {
        stageList = stageService.GetStageList().ToList();

        Debug.Log($"Stage {stageList[0].stage_name}");
    }

}


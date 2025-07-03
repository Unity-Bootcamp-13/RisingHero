using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public interface IStageType
{
    void SetStageProperties(StageData stageData, List<UserStageData> userStageDataLista, IStageSystemReceiver receiver);
    void Clear(int killCount, List<UserStageData> userStageDataList);
    void Defeat(float timeLimita);
}


// 노멀 타입은 1
public class NormalStageType : IStageType
{
    private StageData stageData;
    private List<UserStageData> userStageDataList;
    private UserCurrentStageData userCurrentStageData;
    public void SetStageProperties(StageData stageData, List<UserStageData> userStageDataList, IStageSystemReceiver receiver)
    {
        this.stageData = stageData;
        this.userStageDataList = userStageDataList;
        // 노멀 스테이지에 대한 속성 설정
        // 예: 적의 수, 난이도, 시간 제한 등
        /*GameObject stagePrefab = Resources.Load<GameObject>($"{this.stageData.stage_map}");

        if (stagePrefab == null)
        {
            Debug.LogError($"[StageType1] 프리팹을 찾을 수 없습니다: {this.stageData.stage_map}");
        }*/
        //receiver.ApplyStageTilemap(stagePrefab);

    }
    public void Clear(int killCount, List<UserStageData> userStageDataList)
    {
    }
    public void Defeat(float timeLimit)
    {
    }
}

// 관문 타입은 2
public class GateStageType : IStageType
{
    private StageData stageData;
    private List<UserStageData> userStageDataList;
    private UserCurrentStageData userCurrentStageData;
    public void SetStageProperties(StageData stageData, List<UserStageData> userStageDataList, IStageSystemReceiver receiver)
    {
        // 관문 스테이지에 대한 속성 설정
        // 예: 특정 조건을 만족해야 통과 가능, 보상 등
        this.stageData = stageData;
        this.userStageDataList = userStageDataList;

        /*GameObject stagePrefab = Resources.Load<GameObject>($"{this.stageData.stage_map}");

        if (stagePrefab == null)
        {
            Debug.LogError($"[StageType2] 프리팹을 찾을 수 없습니다: {this.stageData.stage_map}");
        }*/

        // IStageSystemReceiver 인터페이스를 통해 설정을 적용
        receiver.ApplyStageSettings(stageData.stage_time, stageData.stage_enemy_kill);
        //receiver.ApplyStageTilemap(stagePrefab);
    }

    public void Clear(int killCount, List<UserStageData> userStageDataList)
    {
        if (killCount >= this.stageData.stage_enemy_kill) // 적을 모두 처치했는지 확인
        {   // 관문 스테이지 클리어 로직
            // 예: 다음 스테이지로 이동, 보상 지급 등
            // 현재는 한 개만 언락이 가능하지만, 추 후 여러 걔의 언락을 지원할려면 테이블을 하나 더 만들어야 함
            this.userStageDataList[stageData.unlock_stage_id].is_unlocked = true; // 다음 스테이지 잠금 해제
            this.userStageDataList[stageData.stage_id].is_clear = true; // 현재 스테이지 클리어 표시
            this.userStageDataList[stageData.stage_id].stage_type = StageType.Normal; // 스테이지 타입 설정
            StageManager.UserStageService.SaveUserStageList(); // 변경 사항 저장
        }
    }

    public void Defeat(float timeLimit)
    {
        // 관문 스테이지에서 패배 조건을 정의
        // 예: 시간 초과, 특정 적에게 패배 등
        if (timeLimit <= 0) // 시간 초과 시 패배
        {
            // 패배 로직
            // 예: 게임 오버, 재시도 등
            StageManager.UserCurrentStageService.MovePrevStage(); // 이전 스테이지로 이동
        }
    }
}

// 보스 타입은 3
public class BossStageType : IStageType
{
    private StageData stageData;
    private List<UserStageData> userStageDataList;
    private UserCurrentStageData userCurrentStageData;
    public void SetStageProperties(StageData stageData, List<UserStageData> userStageDataList, IStageSystemReceiver receiver)
    {
        // 보스 스테이지에 대한 속성 설정
        // 예: 보스의 체력, 공격 패턴, 보상 등
    }
    public void Clear(int killCount, List<UserStageData> userStageDataList)
    {
        if (killCount <= 0) // 보스를 처치했는지 확인
        {   // 보스 스테이지 클리어 로직
            // 예: 다음 스테이지로 이동, 보상 지급 등
        }
    }

    public void Defeat(float timeLimit)
    {
        // 보스 스테이지에서 패배 조건을 정의
        // 예: 시간 초과, 보스에게 패배 등
        if (timeLimit <= 0) // 시간 초과 시 패배
        {
            // 패배 로직
            // 예: 게임 오버, 재시도 등
        }
    }
}

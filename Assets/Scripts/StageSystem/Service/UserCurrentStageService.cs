using System.Collections.Generic;

public interface IUserCurrentStageService
{
    void SaveCurrentStage(int stageId);
    void SaveCheckpointStage(int stageId);
    int GetUserCurrentStageID();
    int GetUserPrevStageID();
    int GetUserCheckpointStageID();
    void MovePrevStage();
}

public class UserCurrentStageService : IUserCurrentStageService
{
    private IUserCurrentStageDataRepository userCurrentStageDataRepository;
    private UserCurrentStageData userCurrentStage;
    private int checkpointStageId;

    public UserCurrentStageService()
    {
        var parser = new Parser<UserCurrentStageData>("UserCurrentStageTable.json");
        userCurrentStageDataRepository = new UserCurrentStageDataRepository(parser);
        userCurrentStage = userCurrentStageDataRepository.GetData();
    }

    public int GetUserCurrentStageID()
    {
        return userCurrentStage.current_stage_id;
    }

    public int GetUserPrevStageID()
    {
        return userCurrentStage.prev_stage_id;
    }

    // 플러스 알파로 이거 데이터 삭제 가능성 높음 궭(추후 리팩토리 시 검색)
    public int GetUserCheckpointStageID()
    {
        return userCurrentStage.checkpoint_stage_id;
    }

    //클릭 시 체크포인트 이동하게
    public void SaveCurrentStage(int stageId)
    {
        userCurrentStageDataRepository.CurrentStageSave(stageId);
        //userCurrentStageDataRepository.CurrentStageSave(checkpointStageId); //쓸데 없음 아직
    }

    // 체크포인트는 테이블이 아닌 여기에서 메모리로 저장
    public void SaveCheckpointStage(int stageId)
    {
        checkpointStageId = stageId;
    }

    public void MovePrevStage()
    {
        userCurrentStageDataRepository.MovePrevStage();
    }


}


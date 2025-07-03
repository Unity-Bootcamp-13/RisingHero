using System.Collections.Generic;

public interface IUserCurrentStageDataRepository
{
    UserCurrentStageData GetData();
    void CurrentStageSave(int stageId);
    void CheckpointStageSave(int stageId);
}

public class UserCurrentStageDataRepository : IUserCurrentStageDataRepository
{
    private readonly string Path = "StageData/UserCurrentStage";
    private readonly IParser<UserCurrentStageData> _parser;
    private UserCurrentStageData _userCurrentStage;

    public UserCurrentStageDataRepository(IParser<UserCurrentStageData> parser)
    {
        _parser = parser;
        _userCurrentStage = parser.Load(Path);
        
    }

    public UserCurrentStageData GetData()
    {
        return _userCurrentStage;
    }

    public void CurrentStageSave(int stageId)
    {
        _userCurrentStage = _parser.Load(Path);
        _userCurrentStage.prev_stage_id = _userCurrentStage.current_stage_id;
        _userCurrentStage.current_stage_id = stageId;
        
        _parser.Save(_userCurrentStage);
    }

    public void CheckpointStageSave(int stageId)
    {
        _userCurrentStage.checkpoint_stage_id = stageId;
        
        _parser.Save(_userCurrentStage);
    }

}


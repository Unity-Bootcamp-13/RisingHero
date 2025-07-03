using System.Collections.Generic;
using System.Linq;

public interface IUserStageService
{
    IReadOnlyList<UserStageData> GetUserStageList();

    void SaveUserStageList();
    bool BlockMoveToStage(int stageId);
}

public class UserStageService : IUserStageService
{
    private IUserStageDataRepository userStageDataRepository;
    private List<UserStageData> userStageList;
    public UserStageService()
    {
        var parser = new Parser<UserStageDataList>("UserStageTable.json");
        userStageDataRepository = new UserStageDataRepository(parser);
        userStageList = userStageDataRepository.FindAll().ToList();
    }

    public IReadOnlyList<UserStageData> GetUserStageList()
    {
        return userStageList;
    }

    public void SaveUserStageList()
    {
        userStageDataRepository.UserStageSave(userStageList);
    }


    public bool BlockMoveToStage(int stageId)
    {
        if (!userStageList[stageId].is_unlocked)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public StageType GetStageType(int stageId)
    {
        return userStageList[stageId].stage_type;
    }
}


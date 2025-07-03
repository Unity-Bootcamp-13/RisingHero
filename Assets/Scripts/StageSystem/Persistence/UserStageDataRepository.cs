using System.Collections.Generic;

public interface IUserStageDataRepository
{
    IReadOnlyList<UserStageData> FindAll();
}

public class UserStageDataRepository : IUserStageDataRepository
{
    private readonly string Path = "StageData/UserStageTable";
    private List<UserStageData> _userStageDataList;

    public UserStageDataRepository(IParser<UserStageDataList> parser)
    {
        UserStageDataList userStageData = parser.Load(Path);
        _userStageDataList = userStageData.userStageList;
    }

    public IReadOnlyList<UserStageData> FindAll()
    {
        return _userStageDataList;
    }

}


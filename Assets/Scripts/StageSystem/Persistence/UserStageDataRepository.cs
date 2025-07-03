using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;

public interface IUserStageDataRepository
{
    IReadOnlyList<UserStageData> FindAll();
    void UserStageSave(List<UserStageData> userStageList);
}

public class UserStageDataRepository : IUserStageDataRepository
{
    private readonly string Path = "StageData/UserStageTable";
    private readonly IParser<UserStageDataList> _parser;
    private List<UserStageData> _userStageDataList;
    private UserStageDataList _userStageData;

    public UserStageDataRepository(IParser<UserStageDataList> parser)
    {
        _parser = parser;
        _userStageData = _parser.Load(Path);
        _userStageDataList = _userStageData.userStageList;

    }

    public IReadOnlyList<UserStageData> FindAll()
    {
        return _userStageDataList;
    }

    public void UserStageSave(List<UserStageData> userStageList)
    {
        _userStageData.userStageList = userStageList;
        _parser.Save(_userStageData);
    }

}


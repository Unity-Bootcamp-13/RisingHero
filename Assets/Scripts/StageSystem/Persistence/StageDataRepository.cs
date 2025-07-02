using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public interface IStageDataRepository
{
    IReadOnlyList<StageData> FindAll();
}

public class StageDataRepository : IStageDataRepository
{
    private readonly string Path = "StageData/StageTable";
    private List<StageData> _stageDataList;

    public StageDataRepository(IParser<StageDataList> parser)
    {
        StageDataList stageData = parser.Load(Path);
        _stageDataList = stageData.stageList;
    }

    public IReadOnlyList<StageData> FindAll()
    {
        return _stageDataList;
    }
}


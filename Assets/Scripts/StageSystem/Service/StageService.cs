
using System.Collections.Generic;
using System.Linq;

public interface IStageService
{
    IReadOnlyList<StageData> GetStageList();

    void MoveToStage(int stageId);
}

public class StageService : IStageService
{
    private IStageSystem stageSystem;
    private List<StageData> stageList;

    private IStageDataRepository stageRepository;


    public StageService()
    {
        var parser = new Parser<StageDataList>("StageTable.json");
        stageRepository = new StageDataRepository(parser);
        stageList = stageRepository.FindAll().ToList();
    }

    public IReadOnlyList<StageData> GetStageList()
    {
        return stageList;
    }

    public void MoveToStage(int stageId)
    {
        if (stageId == 1 || stageId == 3 || stageId == 5 || stageId == 7 || stageId == 9)
        {
            stageSystem.SaveStage("Stage_01");
        }
        else if (stageId == 2 || stageId == 4 || stageId == 6 || stageId == 8 || stageId == 10)
        {
            stageSystem.SaveStage("Stage_02");
        }
    }
}

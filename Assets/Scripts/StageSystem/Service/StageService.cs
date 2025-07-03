
using System.Collections.Generic;
using System.Linq;

public interface IStageService
{
    IReadOnlyList<StageData> GetStageList();

    void MoveToStage(int stageId);
}

public class StageService : IStageService
{
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
        // 데이터테이블에 저장되어있는 아이디별 스테이지 경로 불러와서 저장하면 됨
    }
}

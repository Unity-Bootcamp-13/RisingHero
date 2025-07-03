using System.Collections.Generic;

public interface IStageButtonRegistry
{
    IReadOnlyList<StageButton> GetAll();
    void Register(StageButton stageButton);
    void Clear();
}

public class StageButtonRegistry : IStageButtonRegistry
{
    private readonly List<StageButton> buttons = new();
    public  IReadOnlyList<StageButton> GetAll() => buttons;

    public void Register(StageButton stageButton)
    {
        buttons.Add(stageButton);
    }

    public void Clear()
    {
        buttons.Clear();
    }
}


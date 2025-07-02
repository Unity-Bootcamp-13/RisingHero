using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/// <summary>
/// 안 쓸 거 같지만 일단 남겨둠
/// </summary>
public static class StageEventBus
{
    public static event Action<int> OnStageBlocked;
    public static event Action<int> OnStageMoved;

    public static void StageMoved(int stageId)
    {
        OnStageMoved.Invoke(stageId);
    }

    public static void ShowBlockedStageMove(int stageId)
    {
        OnStageBlocked.Invoke(stageId);
    }
}


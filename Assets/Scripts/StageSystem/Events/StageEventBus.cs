using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public static class StageEventBus
{
    public static event Action<int> OnStage;
    public static void MoveStage(int stageId)
    {
        OnStage?.Invoke(stageId);
    }
}


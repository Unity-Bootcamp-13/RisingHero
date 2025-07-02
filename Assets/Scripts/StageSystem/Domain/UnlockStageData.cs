using System;

[Serializable]
public class UnlockStageData
{
    public int UnLockStageId { get; private set; }
    public int CommonStageId { get; private set; }
    public string UnLockStageName { get; private set; }
    public bool IsClear { get; private set; }
}

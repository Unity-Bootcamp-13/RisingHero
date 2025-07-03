using System;
using System.Collections.Generic;

public enum StageType
{
    None = 0,
    Normal,
    Gate,
    Boss
}

[Serializable]
public class UserStageData
{
    public int user_stage_id;
    public StageType stage_type;
    public bool is_clear;
    public bool is_unlocked;
}

[Serializable]
public class UserStageDataList
{
    public List<UserStageData> userStageList = new List<UserStageData>();
}

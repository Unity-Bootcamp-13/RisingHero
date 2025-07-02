using System;
using System.Collections.Generic;

[Serializable]
public class UserStageData
{
    public int user_stage_id;
    public bool is_clear;
    public bool is_unlocked;
}

[Serializable]
public class UserStageDataList
{
    public List<UserStageData> userStageList = new List<UserStageData>();
}

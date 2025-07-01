using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class StageData
{
    public int stage_id;
    public string stage_name;
    public bool can_unlock;
    public bool is_clear;
    public bool is_boss;
}

[Serializable]
public class StageDataList
{
    public List<StageData> stageList = new List<StageData>();
}
using System;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class StageData
{
    public int stage_id;
    public string stage_name;
    public float stage_time;
    public int stage_enemy_kill;
    public string stage_map;
    public int unlock_stage_id;
}

[Serializable]
public class StageDataList
{
    public List<StageData> stageList = new List<StageData>();
}
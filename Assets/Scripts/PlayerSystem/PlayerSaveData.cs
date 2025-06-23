using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerSaveData
{
    public List<StageClearData> clearedStages = new List<StageClearData>();
}

[Serializable]
public class StageClearData
{
    public string stageId;
    public bool isCleared;

    public StageClearData(string stageId, bool isCleared)
    {
        this.stageId = stageId;
        this.isCleared = isCleared;
    }
}

using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerSaveData
{
    public int coin = 0;
    public int diamond = 0;
    public List<StageClearData> clearedStages = new();
    public List<OwnedWeapon> ownedWeapons = new();
    public int equippedWeaponId = -1;
    public string currentStage = "Stage_01";  // ← main 브랜치의 필드 수동 병합
}

[Serializable]
public class OwnedWeapon
{
    public int weaponId;
    public int level;
    public int amount;

    public OwnedWeapon(int weaponId, int level, int amount = 1)
    {
        this.weaponId = weaponId;
        this.level = level;
        this.amount = amount;
    }
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

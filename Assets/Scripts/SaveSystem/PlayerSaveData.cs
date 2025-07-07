using System;
using System.Collections.Generic;


[Serializable]
public class PlayerSaveData
{
    public int coin = 0;
    public int diamond = 0;
    public List<OwnedWeapon> ownedWeapons = new();
    public int equippedWeaponId = -1;
    public int currentStage = 11;
    public int topStage = 11;
    public int currentQuestId = -1;

    public PlayerSaveData()
    {
        coin = 0;
        diamond = 0;
        equippedWeaponId = -1;
        currentStage = 11;
        topStage = 11;
        currentQuestId = -1;
    }
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

    public override string ToString()
    {
        return $"WeaponID: {weaponId}, Level: {level}, Amount: {amount}";
    }
}
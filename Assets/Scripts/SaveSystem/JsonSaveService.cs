using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class JsonSaveService : ISaveService
{
    private readonly string savePath;
    private PlayerSaveData cachedData;

    public JsonSaveService()
    {
        savePath = Path.Combine(Application.persistentDataPath, "save.json");
    }

    public void Save(PlayerSaveData data)
    {
        Debug.Log("[JsonSaveService] Save 호출됨");
        Debug.Log($"[SaveService] 저장됨 - topStage: {data.topStage}, currentStage: {data.currentStage}");

        // 저장되야하는 목록이 캐시로 남아서 사라지는 ISSUE
        if (cachedData != null)
        {
            cachedData.coin = data.coin;
            cachedData.diamond = data.diamond;
            cachedData.equippedWeaponId = data.equippedWeaponId;
            cachedData.ownedWeapons = new List<OwnedWeapon>(data.ownedWeapons);
            cachedData.currentStage = data.currentStage;
            cachedData.topStage = data.topStage;
            cachedData.currentQuestId = data.currentQuestId;
        }
        else
        {
            cachedData = data;
        }

        string json = JsonUtility.ToJson(cachedData, true);
        File.WriteAllText(savePath, json);
    }

    public PlayerSaveData Load()
    {
        if (cachedData != null)
            return cachedData;

        if (File.Exists(savePath))
        {
            string json = File.ReadAllText(savePath);
            cachedData = JsonUtility.FromJson<PlayerSaveData>(json);
        }
        else
        {
            cachedData = new PlayerSaveData();
            Save(cachedData);
        }

        return cachedData;
    }

    public void Delete()
    {
        if (File.Exists(savePath))
            File.Delete(savePath);

        cachedData = null;
    }
}

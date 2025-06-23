using System.IO;
using UnityEngine;

public static class SaveManager
{
    private static readonly string savePath = Application.persistentDataPath + "/save.json";
    private static PlayerSaveData cachedData;

    public static void Save(PlayerSaveData data)
    {
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(savePath, json);
        cachedData = data;
    }

    public static PlayerSaveData Load()
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

    public static void DeleteSave()
    {
        if (File.Exists(savePath))
            File.Delete(savePath);

        cachedData = null;
    }
}

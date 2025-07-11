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
        cachedData = data; // Ä³½Ì
        string json = JsonUtility.ToJson(data, true);
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

    public PlayerSaveData ReloadFromFile()
    {
        if (File.Exists(savePath))
        {
            string json = File.ReadAllText(savePath);
            cachedData = JsonUtility.FromJson<PlayerSaveData>(json);
        }
        return cachedData;
    }
}

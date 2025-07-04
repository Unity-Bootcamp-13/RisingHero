using System.Collections.Generic;
using System.IO;
using UnityEngine;

public interface IParser<T>
{
    void Save(T data);
    T Load(string path);
}

public class Parser<T> : IParser<T>
{
    private readonly string savePath;
    public Parser(string path)
    {
        savePath = Path.Combine(Application.persistentDataPath, path);
    }

    public void Save(T data)
    {
        Debug.Log("[Parser] Save 호출됨");

        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(savePath, json);
    }

    public T Load(string path)
    {

        if (File.Exists(savePath))
        {
            string json = File.ReadAllText(savePath);
            Debug.Log("[Parser] 로컬 저장 데이터 불러오기 성공");
            return JsonUtility.FromJson<T>(json);
        }
        else
        {
            TextAsset jsonText = Resources.Load<TextAsset>(path);

            T cachedData = JsonUtility.FromJson<T>(jsonText.text);
            Save(cachedData); // Resources에서 로드한 데이터를 로컬에 저장
            Debug.Log("[Parser] Resources 로드 후 저장");
            return cachedData;
        }
    }
}

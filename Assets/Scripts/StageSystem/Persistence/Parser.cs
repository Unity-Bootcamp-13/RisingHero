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
        Debug.Log("[Parser] Save ȣ���");

        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(savePath, json);
    }

    public T Load(string path)
    {

        if (File.Exists(savePath))
        {
            string json = File.ReadAllText(savePath);
            Debug.Log("[Parser] ���� ���� ������ �ҷ����� ����");
            return JsonUtility.FromJson<T>(json);
        }
        else
        {
            TextAsset jsonText = Resources.Load<TextAsset>(path);

            T cachedData = JsonUtility.FromJson<T>(jsonText.text);
            Save(cachedData); // Resources���� �ε��� �����͸� ���ÿ� ����
            Debug.Log("[Parser] Resources �ε� �� ����");
            return cachedData;
        }
    }
}

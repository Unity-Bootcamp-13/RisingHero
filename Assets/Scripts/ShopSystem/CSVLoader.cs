using System;
using System.Collections.Generic;
using UnityEngine;

public interface ICSVLoadable
{
    void LoadFromDictionary(Dictionary<string, object> dict);
}

public class CSVLoader
{
    public static List<T> Load<T>(
         string resourcePath,
         ICSVRead reader = null,
         ICSVParser parser = null
    ) where T : ICSVLoadable, new()
    {
        if (reader == null)
            reader = new CSVReader();

        if (parser == null)
            parser = new CSVParser();

        string[] lines = reader.ReadLines(resourcePath);
        List<Dictionary<string, object>> rawData = parser.Parse(lines);

        return ConvertToObjects<T>(rawData);
    }

    private static List<T> ConvertToObjects<T>(List<Dictionary<string, object>> rawData)
        where T : ICSVLoadable, new()
    {
        List<T> result = new List<T>();

        foreach (Dictionary<string, object> row in rawData)
        {
            try
            {
                T obj = new T();
                obj.LoadFromDictionary(row);
                result.Add(obj);
            }
            catch (Exception e)
            {
                Debug.LogError($"[CSVLoader] {typeof(T).Name} 변환 실패: {e.Message}");
            }
        }

        return result;
    }
}

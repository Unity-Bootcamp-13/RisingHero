using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        var result = new List<T>();
        foreach (var row in rawData)
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

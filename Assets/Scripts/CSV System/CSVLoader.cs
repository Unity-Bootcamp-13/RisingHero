using System.Collections.Generic;
using UnityEngine;

public static class CSVLoader
{
    public static List<T> LoadTable<T>(string fileName) where T : ICSVParsable, new()
    {
        TextAsset csvFile = Resources.Load<TextAsset>($"CSV/{fileName}");
        if (csvFile == null)
        {
            Debug.LogError($"[CSVLoader] 파일을 찾을 수 없습니다: {fileName}");
            return new List<T>();
        }

        var lines = csvFile.text.Split('\n');
        var list = new List<T>();

        for (int i = 1; i < lines.Length; i++) // 헤더 스킵
        {
            var line = lines[i].Trim();
            if (string.IsNullOrEmpty(line)) continue;

            var values = line.Split(',');
            var entry = new T();
            entry.Parse(values);
            list.Add(entry);
        }

        return list;
    }
}
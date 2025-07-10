using System.Collections.Generic;
using UnityEngine;

public static class CSVLoader
{
    public static List<T> LoadTable<T>(string fileName) where T : ICSVParsable, new()
    {
        TextAsset csvFile = Resources.Load<TextAsset>($"CSV/{fileName}");
        if (csvFile == null)
        {
            return new List<T>();
        }

        var lines = csvFile.text.Split('\n');
        var list = new List<T>();

        for (int i = 1; i < lines.Length; i++) // Çì´õ ½ºÅµ
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
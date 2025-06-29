using System;
using System.Collections.Generic;
using UnityEngine;

public interface ICSVParser
{
    List<Dictionary<string, object>> Parse(string[] lines);
}

public class CSVParser : ICSVParser
{
    public List<Dictionary<string, object>> Parse(string[] lines)
    {
        var result = new List<Dictionary<string, object>> ();

        if (lines.Length < 3) // 컬려명 + 타입행 + 데이터 최소 1줄 필요
            return result;

        var headers = lines[0].Split(',');
        var types = lines[1].Split(',');

        if (headers.Length != types.Length)
        {
            Debug.LogError("[CSVParser] 헤더 열 개수와 타입 열 개수가 다릅니다.");
            return result;
        }


        // 컬럼별 변환 함수 배열
        Func<string, object>[] parseFuncs = new Func<string, object>[types.Length];
        for (int i = 0; i < types.Length; i++)
            parseFuncs[i] = GetParseFunc(types[i].Trim());

        for (int i = 2; i < lines.Length; i++) // 3번째 줄부터 데이터
        {
            string line = lines[i];
            if (string.IsNullOrWhiteSpace(line))
                continue;

            var values = line.Split(',');

            var row = new Dictionary<string, object>();
            for (int j = 0; j < headers.Length; j++)
            {
                string raw = j < values.Length ? values[j].Trim() : "";
                object val = parseFuncs[j](raw);
                row[headers[j].Trim()] = val;
            }
            try
            {
                result.Add(row);
            }
            catch (Exception e)
            {
                Debug.LogError($"[CSVParser] 데이터 변환 실패: {e.Message}");
            }

        }

        return result;
    }

    // 문자열을 자동으로 타입에 맞게 변환하는 함수
    private Func<string, object> GetParseFunc(string typeStr)
    {
        switch (typeStr.ToLower())
        {
            case "int": return s => int.TryParse(s, out int i) ? i : 0;
            case "float": return s => float.TryParse(s, out float f) ? f : 0f;
            case "bool": return s => bool.TryParse(s, out bool b) ? b : false;
            case "string": return s => s;
            default:
                Debug.LogWarning($"[CSVParser] 알 수 없는 타입 '{typeStr}', string 처리");
                return s => s;
        }
    }
}

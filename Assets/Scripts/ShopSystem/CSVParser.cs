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
        List<Dictionary<string, object>> result = new List<Dictionary<string, object>>(); // 결과 리스트
        string[] headers = lines[0].Split(','); // 첫 줄: 열 이름
        string[] types = lines[1].Split(',');   // 둘째 줄: 열 타입

        if (!IsValidCSV(lines, headers, types))
            return result;

        Func<string, object>[] parseFuncs = CreateParseFuncs(types);

        for (int i = 2; i < lines.Length; i++) // 데이터는 3번째 줄부터 시작
        {
            string line = lines[i];
            if (string.IsNullOrWhiteSpace(line))
                continue;

            string[] values = line.Split(',');

            Dictionary<string, object> row = ParseRow(headers, values, parseFuncs);

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

    private Func<string, object>[] CreateParseFuncs(string[] types)
    {
        Func<string, object>[] funcs = new Func<string, object>[types.Length];
        for (int i = 0; i < types.Length; i++)
            funcs[i] = GetParseFunc(types[i].Trim());
        return funcs;
    }

    private Func<string, object> GetParseFunc(string typeStr)
    {
        switch (typeStr.ToLower())
        {
            case "int": return s => int.Parse(s);
            case "float": return s => float.Parse(s);
            case "bool": return s => bool.Parse(s);
            case "string": return s => s;
            default:
                Debug.LogWarning($"[CSVParser] 알 수 없는 타입 '{typeStr}', string 처리");
                return s => s;
        }
    }

    private static bool IsValidCSV(string[] lines, string[] headers, string[] types)
    {
        if (lines == null || lines.Length < 3)
        {
            Debug.LogError("[CSVParser] CSV 파일의 형식이 잘못되었습니다. 최소 3줄이 필요합니다.");
            return false;
        }

        if (headers.Length != types.Length)
        {
            Debug.LogError("[CSVParser] 헤더 열 개수와 타입 열 개수가 다릅니다.");
            return false;
        }

        return true;
    }

    private Dictionary<string, object> ParseRow(
        string[] headers,
        string[] values,
        Func<string, object>[] parseFuncs)
    {
        Dictionary<string, object> row = new Dictionary<string, object>();
        for (int j = 0; j < headers.Length; j++)
        {
            if (j < values.Length)
            {
                string raw = values[j].Trim();
                object val = parseFuncs[j](raw);
                row[headers[j].Trim()] = val;
            }
            else
            {
                Debug.Log($"[CSVParser] {headers[j].Trim()} = {values[j].Trim()}");
                return row;
            }
        }

        return row;
    }
}
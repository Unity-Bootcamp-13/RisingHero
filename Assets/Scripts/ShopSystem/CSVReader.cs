using UnityEngine;

public interface ICSVRead
{
    string[] ReadLines(string resourcePathWithoutExtension);
}

public class CSVReader : ICSVRead
{
    // Resources에서 CSV 텍스트 읽기
    public string[] ReadLines(string resourcePathWithoutExtension)
    {
        TextAsset textAsset = Resources.Load<TextAsset>(resourcePathWithoutExtension);
        if (textAsset == null)
        {
            Debug.LogError($"리소스 경로에 파일이 없습니다: {resourcePathWithoutExtension}");
            return new string[0];
        }

        return textAsset.text.Split('\n'); // 줄 단위 분할
    }
}

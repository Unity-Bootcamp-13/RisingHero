using TMPro;
using UnityEngine;
using System.Collections;

public class TextAlphaBlink : MonoBehaviour
{
    [SerializeField] private TMP_Text text;
    [SerializeField] private float duration = 1f; // 한 번의 페이드 인/아웃 시간

    public void StartBlink()
    {
        StartCoroutine(BlinkAlpha());
    }

    private IEnumerator BlinkAlpha()
    {
        while (true)
        {
            // 투명 → 불투명
            yield return StartCoroutine(FadeAlpha(0f, 1f));
            // 불투명 → 투명
            yield return StartCoroutine(FadeAlpha(1f, 0f));
        }
    }

    private IEnumerator FadeAlpha(float from, float to)
    {
        float elapsed = 0f;
        Color color = text.color;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Lerp(from, to, elapsed / duration);
            text.color = new Color(color.r, color.g, color.b, alpha);
            yield return null;
        }

        // 정확히 목표 알파로 설정
        text.color = new Color(color.r, color.g, color.b, to);
    }
}

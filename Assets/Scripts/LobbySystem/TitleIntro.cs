using TMPro;
using UnityEngine;
using System.Collections;

public class TitleIntro : MonoBehaviour
{
    [SerializeField] private TMP_Text titleText;
    [SerializeField] private float moveDistance = 100f;  // 내려오는 거리
    [SerializeField] private float duration = 1f;        // 애니메이션 시간

    public IEnumerator PlayTitleIntro()
    {
        RectTransform rect = titleText.GetComponent<RectTransform>();

        Vector2 endPos = rect.anchoredPosition;
        Vector2 startPos = endPos + new Vector2(0, moveDistance);
        rect.anchoredPosition = startPos;

        // 알파값 0으로 초기화 (컬러 유지)
        Color originalColor = titleText.color;
        titleText.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0f);

        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration;

            // 위치 보간
            rect.anchoredPosition = Vector2.Lerp(startPos, endPos, t);

            // 알파 보간
            float alpha = Mathf.Lerp(0f, 1f, t);
            titleText.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);

            yield return null;
        }

        // 정확한 위치와 알파값 보정
        rect.anchoredPosition = endPos;
        titleText.color = new Color(originalColor.r, originalColor.g, originalColor.b, 1f);
    }
}

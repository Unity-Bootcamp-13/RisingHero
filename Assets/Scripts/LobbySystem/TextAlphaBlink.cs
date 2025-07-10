using TMPro;
using UnityEngine;
using System.Collections;

public class TextAlphaBlink : MonoBehaviour
{
    [SerializeField] private TMP_Text text;
    [SerializeField] private float duration = 1f; // �� ���� ���̵� ��/�ƿ� �ð�

    public void StartBlink()
    {
        StartCoroutine(BlinkAlpha());
    }

    private IEnumerator BlinkAlpha()
    {
        while (true)
        {
            // ���� �� ������
            yield return StartCoroutine(FadeAlpha(0f, 1f));
            // ������ �� ����
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

        // ��Ȯ�� ��ǥ ���ķ� ����
        text.color = new Color(color.r, color.g, color.b, to);
    }
}

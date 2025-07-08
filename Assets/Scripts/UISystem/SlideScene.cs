using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public interface ISlideScene
{
    void LodeSceneWithSlide(string sceneName);
}

public class SlideScene : MonoBehaviour, ISlideScene
{
    [SerializeField] private RectTransform SceneLodeStartPanel;
    [SerializeField] private RectTransform SceneLodeEndPanel;

    [SerializeField] private float slideDuration = 0.5f; // �ε巴�� �̵��ϴ� �ð�


    private Vector2 SceneLodePanelHiddenPosition;
    private Vector2 SceneLodePanelShownPosition;

    private void Start()
    {
        SceneLodePanelHiddenPosition = SceneLodeEndPanel.anchoredPosition + new Vector2(2000, 0f); // ȭ�� ������ �� ����

        SceneLodePanelShownPosition = SceneLodeStartPanel.anchoredPosition + new Vector2(2000, 0f); // ȭ�� ������ ���� (���������� �̵�)

        StartCoroutine(Slide(SceneLodeEndPanel, SceneLodeEndPanel.anchoredPosition, SceneLodePanelHiddenPosition));

    }

    public void LodeSceneWithSlide(string sceneName)
    {
        StartCoroutine(SlideAndLoad(sceneName));
    }

    private IEnumerator SlideAndLoad(string sceneName)
    {
        yield return StartCoroutine(Slide(SceneLodeStartPanel, SceneLodeStartPanel.anchoredPosition, SceneLodePanelShownPosition));
        yield return new WaitForSeconds(slideDuration); // �����̵尡 �Ϸ�� ������ ���
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }

    private IEnumerator Slide(RectTransform target, Vector2 start, Vector2 end)
    {
        float elapsed = 0f;

        while (elapsed < slideDuration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / slideDuration;

            // �ε巴�� �̵�
            target.anchoredPosition = Vector2.Lerp(start, end, Mathf.SmoothStep(0, 1, t));

            yield return null;
        }

        target.anchoredPosition = end;
    }

}

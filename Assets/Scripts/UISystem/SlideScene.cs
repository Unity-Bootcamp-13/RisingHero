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

    [SerializeField] private float slideDuration = 0.5f; // �����̵� �ð�

    private Vector2 SceneLodePanelHiddenPosition;
    private Vector2 SceneLodePanelShownPosition;

    private void Start()
    {
        // �г��� ������ ��ġ �� �������� ��ġ ����
        SceneLodePanelHiddenPosition = SceneLodeEndPanel.anchoredPosition + new Vector2(2000f, 0f);
        SceneLodePanelShownPosition = SceneLodeStartPanel.anchoredPosition + new Vector2(2000f, 0f);

        // ������ �� EndPanel�� ���������� �����̵� �ƿ�
        StartCoroutine(Slide(SceneLodeEndPanel, SceneLodeEndPanel.anchoredPosition, SceneLodePanelHiddenPosition));
    }

    public void LodeSceneWithSlide(string sceneName)
    {
        StartCoroutine(SlideAndLoad(sceneName));
    }

    private IEnumerator SlideAndLoad(string sceneName)
    {
        // �����̵� �� (StartPanel �� ����������)
        yield return StartCoroutine(Slide(SceneLodeStartPanel, SceneLodeStartPanel.anchoredPosition, SceneLodePanelShownPosition));

        yield return new WaitForSeconds(slideDuration); // �����̵� �Ϸ� ���

        SceneManager.LoadScene(sceneName, LoadSceneMode.Single); // ��� �ε�
    }

    private IEnumerator Slide(RectTransform target, Vector2 start, Vector2 end)
    {
        Vector2 velocity = Vector2.zero;
        float elapsedTime = 0f;

        // ���� ��ġ ����
        target.anchoredPosition = start;

        while (Vector2.Distance(target.anchoredPosition, end) > 0.1f)
        {
            target.anchoredPosition = Vector2.SmoothDamp(target.anchoredPosition, end, ref velocity, slideDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        target.anchoredPosition = end; // ��Ȯ�� ��ġ ���߱�
    }
}

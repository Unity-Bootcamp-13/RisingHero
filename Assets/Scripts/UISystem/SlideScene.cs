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

    [SerializeField] private float slideDuration = 0.5f; // 슬라이드 시간

    private Vector2 SceneLodePanelHiddenPosition;
    private Vector2 SceneLodePanelShownPosition;

    private void Start()
    {
        // 패널의 숨겨진 위치 및 보여지는 위치 설정
        SceneLodePanelHiddenPosition = SceneLodeEndPanel.anchoredPosition + new Vector2(2000f, 0f);
        SceneLodePanelShownPosition = SceneLodeStartPanel.anchoredPosition + new Vector2(2000f, 0f);

        // 시작할 때 EndPanel을 오른쪽으로 슬라이드 아웃
        StartCoroutine(Slide(SceneLodeEndPanel, SceneLodeEndPanel.anchoredPosition, SceneLodePanelHiddenPosition));
    }

    public void LodeSceneWithSlide(string sceneName)
    {
        StartCoroutine(SlideAndLoad(sceneName));
    }

    private IEnumerator SlideAndLoad(string sceneName)
    {
        // 슬라이드 인 (StartPanel → 오른쪽으로)
        yield return StartCoroutine(Slide(SceneLodeStartPanel, SceneLodeStartPanel.anchoredPosition, SceneLodePanelShownPosition));

        yield return new WaitForSeconds(slideDuration); // 슬라이드 완료 대기

        SceneManager.LoadScene(sceneName, LoadSceneMode.Single); // 장면 로드
    }

    private IEnumerator Slide(RectTransform target, Vector2 start, Vector2 end)
    {
        Vector2 velocity = Vector2.zero;
        float elapsedTime = 0f;

        // 시작 위치 설정
        target.anchoredPosition = start;

        while (Vector2.Distance(target.anchoredPosition, end) > 0.1f)
        {
            target.anchoredPosition = Vector2.SmoothDamp(target.anchoredPosition, end, ref velocity, slideDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        target.anchoredPosition = end; // 정확히 위치 맞추기
    }
}

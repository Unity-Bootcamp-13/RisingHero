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

    [SerializeField] private float slideDuration = 0.5f; // 부드럽게 이동하는 시간


    private Vector2 SceneLodePanelHiddenPosition;
    private Vector2 SceneLodePanelShownPosition;

    private void Start()
    {
        SceneLodePanelHiddenPosition = SceneLodeEndPanel.anchoredPosition + new Vector2(2000, 0f); // 화면 오른쪽 밖 설정

        SceneLodePanelShownPosition = SceneLodeStartPanel.anchoredPosition + new Vector2(2000, 0f); // 화면 안으로 설정 (오른쪽으로 이동)

        StartCoroutine(Slide(SceneLodeEndPanel, SceneLodeEndPanel.anchoredPosition, SceneLodePanelHiddenPosition));

    }

    public void LodeSceneWithSlide(string sceneName)
    {
        StartCoroutine(SlideAndLoad(sceneName));
    }

    private IEnumerator SlideAndLoad(string sceneName)
    {
        yield return StartCoroutine(Slide(SceneLodeStartPanel, SceneLodeStartPanel.anchoredPosition, SceneLodePanelShownPosition));
        yield return new WaitForSeconds(slideDuration); // 슬라이드가 완료될 때까지 대기
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }

    private IEnumerator Slide(RectTransform target, Vector2 start, Vector2 end)
    {
        float elapsed = 0f;

        while (elapsed < slideDuration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / slideDuration;

            // 부드럽게 이동
            target.anchoredPosition = Vector2.Lerp(start, end, Mathf.SmoothStep(0, 1, t));

            yield return null;
        }

        target.anchoredPosition = end;
    }

}

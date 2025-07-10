using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class IntroManager : MonoBehaviour
{
    [SerializeField] private TitleIntro titleIntro;
    [SerializeField] private TextAlphaBlink textAlphaBlink;
    [SerializeField] private Button LobbyStartButton;

    private void Start()
    {
        StartCoroutine(PlayIntroSequence());
    }

    private IEnumerator PlayIntroSequence()
    {
        yield return StartCoroutine(titleIntro.PlayTitleIntro());
        textAlphaBlink.StartBlink(); // 타이틀 애니 끝나고 시작
        LobbyStartButton.gameObject.SetActive(true);
    }
}

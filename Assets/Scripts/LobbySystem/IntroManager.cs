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
        textAlphaBlink.StartBlink(); // Ÿ��Ʋ �ִ� ������ ����
        LobbyStartButton.gameObject.SetActive(true);
    }
}

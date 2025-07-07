using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Gpm.Ui;

public class ChapterItem : InfiniteScrollItem
{
    [SerializeField] private TMP_Text titleText;
    [SerializeField] private Button button;

    private int chapterIndex;
    private StageSelector selector;

    /// <summary>
    /// StageSelector 참조를 주입받는다.
    /// </summary>
    public void Initialize(StageSelector selector)
    {
        this.selector = selector;
    }

    /// <summary>
    /// 무한 스크롤에서 데이터가 바뀔 때 호출됨.
    /// 데이터에 맞춰 UI 텍스트와 버튼 리스너를 설정한다.
    /// </summary>
    public override void UpdateData(InfiniteScrollData scrollData)
    {
        base.UpdateData(scrollData);

        if (selector == null)
        {
            selector = FindSelector();
        }

        if (scrollData == null)
            return;

        ChapterData data = GetChapterData(scrollData);
        if (data == null)
            return;

        UpdateUI(data);
        SetupButtonListener();
    }

    /// <summary>
    /// 씬에서 StageSelector를 찾아 반환한다.
    /// </summary>
    private StageSelector FindSelector()
    {
        return (StageSelector)FindObjectOfType(typeof(StageSelector));
    }

    /// <summary>
    /// 전달된 데이터를 ChapterData 타입으로 변환 시도.
    /// </summary>
    private ChapterData GetChapterData(InfiniteScrollData data)
    {
        if (data is ChapterData)
            return (ChapterData)data;

        return null;
    }

    /// <summary>
    /// 챕터 제목 텍스트를 업데이트한다.
    /// </summary>
    private void UpdateUI(ChapterData data)
    {
        chapterIndex = data.chapterIndex;

        if (titleText != null)
            titleText.text = data.chapterTitle;
    }

    /// <summary>
    /// 버튼 클릭 리스너를 설정한다.
    /// </summary>
    private void SetupButtonListener()
    {
        if (button == null)
            return;

        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(OnButtonClicked);
    }

    /// <summary>
    /// 버튼 클릭 시 해당 챕터 스테이지를 표시하도록 StageSelector에 요청한다.
    /// </summary>
    private void OnButtonClicked()
    {
        if (selector != null)
        {
            selector.ShowStagesForChapter(chapterIndex);
        }
    }
}

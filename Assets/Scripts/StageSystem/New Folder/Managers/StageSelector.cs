using System.Collections.Generic;
using UnityEngine;
using Gpm.Ui;

public class StageSelector : MonoBehaviour
{
    [Header("스크롤 연결")]
    [SerializeField] private InfiniteScroll chapterScroll;
    [SerializeField] private InfiniteScroll stageScroll;

    [Header("챕터 수")]
    [SerializeField] private int chapterCount = 2;

    private Dictionary<int, List<StageData>> chapterStages = new Dictionary<int, List<StageData>>();
    private ISaveService saveService;
    private IStageSceneLoader stageSceneLoader;

    private void Awake()
    {
        // 저장 서비스와 씬 로더를 초기화한다
        saveService = new JsonSaveService();
        stageSceneLoader = new StageSceneLoader();
    }

    private void Start()
    {
        InitializeChapters();  // 챕터 리스트 생성 및 UI에 연결
        InitializeStages();    // 챕터별 스테이지 데이터 생성
        ShowStagesForChapter(1); // 첫 챕터 스테이지 표시
    }

    /// <summary>
    /// 챕터 데이터를 생성하고 스크롤에 삽입하며,
    /// 각 ChapterItem을 초기화해서 자신을 참조하도록 설정한다.
    /// </summary>
    private void InitializeChapters()
    {
        List<ChapterData> chapters = CreateChapterDataList();
        InsertChapterData(chapters);
        InitializeChapterItems();
    }

    /// <summary>
    /// 챕터Count만큼 ChapterData 리스트를 생성한다.
    /// </summary>
    private List<ChapterData> CreateChapterDataList()
    {
        List<ChapterData> chapters = new List<ChapterData>();

        for (int i = 1; i <= chapterCount; i++)
        {
            ChapterData chapter = new ChapterData();
            chapter.chapterIndex = i;
            chapter.chapterTitle = "Chapter " + i;
            chapters.Add(chapter);
        }

        return chapters;
    }

    /// <summary>
    /// chapterScroll에 생성한 챕터 데이터를 삽입한다.
    /// </summary>
    private void InsertChapterData(List<ChapterData> chapters)
    {
        if (chapterScroll != null)
        {
            chapterScroll.InsertData(chapters.ToArray());
        }
    }

    /// <summary>
    /// chapterScroll의 자식 ChapterItem들을 찾아서
    /// 각 아이템에 이 StageSelector를 주입한다.
    /// </summary>
    private void InitializeChapterItems()
    {
        if (chapterScroll == null)
            return;

        ChapterItem[] chapterItems = chapterScroll.GetComponentsInChildren<ChapterItem>();

        if (chapterItems == null || chapterItems.Length == 0)
            return;

        for (int i = 0; i < chapterItems.Length; i++)
        {
            if (chapterItems[i] != null)
            {
                chapterItems[i].Initialize(this);
            }
        }
    }

    /// <summary>
    /// 각 챕터별 스테이지 리스트를 생성하고
    /// chapterStages 딕셔너리에 저장한다.
    /// </summary>
    private void InitializeStages()
    {
        for (int chapter = 1; chapter <= chapterCount; chapter++)
        {
            List<StageData> stages = CreateStageDataForChapter(chapter);
            chapterStages[chapter] = stages;
        }
    }

    /// <summary>
    /// 주어진 챕터 인덱스에 속하는 10개의 스테이지 데이터를 생성한다.
    /// </summary>
    private List<StageData> CreateStageDataForChapter(int chapterIndex)
    {
        List<StageData> stages = new List<StageData>();

        int startStageNumber = chapterIndex * 10 + 1;
        int endStageNumber = startStageNumber + 9;

        for (int stageNumber = startStageNumber; stageNumber <= endStageNumber; stageNumber++)
        {
            StageData stage = new StageData();
            stage.chapterIndex = chapterIndex;
            stage.stageNumber = stageNumber;
            stages.Add(stage);
        }

        return stages;
    }

    /// <summary>
    /// 선택된 챕터 인덱스의 스테이지 데이터를 stageScroll에 표시한다.
    /// </summary>
    public void ShowStagesForChapter(int chapterIndex)
    {
        if (!chapterStages.ContainsKey(chapterIndex))
            return;

        ClearStageScroll();
        InsertStageData(chapterStages[chapterIndex]);
    }

    /// <summary>
    /// stageScroll에 등록된 기존 데이터를 모두 제거한다.
    /// </summary>
    private void ClearStageScroll()
    {
        if (stageScroll != null)
        {
            stageScroll.Clear();
        }
    }

    /// <summary>
    /// 주어진 스테이지 데이터를 stageScroll에 삽입한다.
    /// </summary>
    private void InsertStageData(List<StageData> stages)
    {
        if (stageScroll != null && stages != null && stages.Count > 0)
        {
            stageScroll.InsertData(stages.ToArray());
        }
    }
}

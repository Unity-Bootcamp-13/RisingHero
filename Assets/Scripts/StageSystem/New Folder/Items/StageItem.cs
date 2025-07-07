using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Gpm.Ui;

public class StageItem : InfiniteScrollItem
{
    [SerializeField] private TMP_Text stageText;
    [SerializeField] private Button button;
    [SerializeField] private Image background;

    private int stageNumber;
    private ISaveService saveService;
    private IStageSceneLoader stageSceneLoader;
    private PlayerSaveData playerSaveData;
    private StageData stageData;

    /// <summary>
    /// 무한 스크롤에서 데이터가 바뀔 때 호출됨.
    /// StageData를 받아 UI를 갱신한다.
    /// </summary>
    public override void UpdateData(InfiniteScrollData scrollData)
    {
        base.UpdateData(scrollData);

        if (scrollData == null)
            return;

        StageData data = GetStageData(scrollData);
        if (data == null)
            return;

        stageData = data;
        RefreshUI(stageData);
    }

    /// <summary>
    /// 전달된 데이터를 StageData 타입으로 변환 시도.
    /// </summary>
    private StageData GetStageData(InfiniteScrollData data)
    {
        if (data is StageData)
            return (StageData)data;

        return null;
    }

    /// <summary>
    /// UI 텍스트, 버튼 상태 등을 갱신한다.
    /// 저장소 및 씬 로더 인스턴스가 없으면 생성한다.
    /// </summary>
    private void RefreshUI(StageData data)
    {
        if (saveService == null)
            saveService = new JsonSaveService();

        if (stageSceneLoader == null)
            stageSceneLoader = new StageSceneLoader();

        stageNumber = data.stageNumber;
        playerSaveData = saveService.Load();

        SetStageText();
        SetStageState();
        SetupButtonListener();
    }

    /// <summary>
    /// 스테이지 번호에 따라 텍스트를 "Stage N" 또는 보스 스테이지 표시로 세팅한다.
    /// </summary>
    private void SetStageText()
    {
        if (stageText == null)
            return;

        if (stageNumber % 10 == 0)
            stageText.text = "Stage " + stageNumber + "\n(Boss)";
        else
            stageText.text = "Stage " + stageNumber;
    }

    /// <summary>
    /// 플레이어 진행 상황에 따라 버튼 색상과 활성 상태를 설정한다.
    /// </summary>
    private void SetStageState()
    {
        if (background == null || button == null || playerSaveData == null)
            return;

        if (stageNumber <= playerSaveData.topStage)
        {
            SetState(Color.cyan, true);
        }
        else if (stageNumber == playerSaveData.topStage + 1)
        {
            SetState(new Color(0.7f, 0.5f, 1f), true);
        }
        else
        {
            SetState(Color.gray, false);
        }
    }

    /// <summary>
    /// 배경 색과 버튼 활성 상태를 설정한다.
    /// </summary>
    private void SetState(Color color, bool interactable)
    {
        background.color = color;
        button.interactable = interactable;
    }

    /// <summary>
    /// 버튼 클릭 리스너를 초기화하고 등록한다.
    /// </summary>
    private void SetupButtonListener()
    {
        if (button == null)
            return;

        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(OnClick);
    }

    /// <summary>
    /// 버튼 클릭 시 호출된다.
    /// 플레이어 데이터를 갱신하고, 버퍼 코인을 적용한 뒤 씬을 로드한다.
    /// </summary>
    private void OnClick()
    {
        if (button == null || !button.interactable)
            return;

        if (saveService == null || stageSceneLoader == null)
            return;

        PlayerSaveData latestData = saveService.Load();
        if (latestData == null)
            return;

        latestData.coin += CoinBuffer.Instance.GetBufferedCoin();
        latestData.currentStage = stageNumber;

        saveService.Save(latestData);
        CoinBuffer.Instance.ResetBuffer();

        stageSceneLoader.LoadStage(stageNumber, latestData.topStage);
    }
}

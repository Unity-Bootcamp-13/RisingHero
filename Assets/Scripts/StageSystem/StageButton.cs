using System;
using TMPro;
using UnityEditor.Overlays;
using UnityEngine;
using UnityEngine.UI;

public class StageButton : MonoBehaviour
{
    [Header("스테이지 번호")]
    [SerializeField] private int stageNumber;

    private Button button;
    private Image background;
    private TMP_Text stageText;
    private ISaveService saveService;
    private IStageSceneLoader stageSceneLoader;

    private PlayerSaveData StageData;

    public void Initialize(ISaveService saveService, IStageSceneLoader stageSceneLoader)
    {
        this.saveService = saveService;
        this.stageSceneLoader = stageSceneLoader;
    }

    private void Awake()
    {
        button = GetComponent<Button>();
        background = GetComponent<Image>();
        stageText = GetComponentInChildren<TMP_Text>();

        if (button == null || background == null || stageText == null)
        {
            Debug.LogError("[StageButton] 버튼 또는 UI 컴포넌트 연결 실패");
        }
    }

    private void Start()
    {
        UpdateState(); // 순서로 인해 스타트가 호출될 때 초기 상태 업데이트
    }

    private void UpdateState()
    {
        if (saveService == null)
        {
            Debug.LogError("[StageButton] SaveService가 초기화되지 않았습니다.");
            return;
        }

        StageData = saveService.Load();


        stageText.text = $"Stage{stageNumber}";

        if (stageNumber <= StageData.topStage)
        {
            SetState(Color.cyan, true);
        }
        else if (stageNumber == StageData.topStage + 1)
        {
            SetState(new Color(0.7f, 0.5f, 1f), true);
        }
        else
        {
            SetState(Color.gray, false);
        }
    }

    private void SetState(Color color, bool interactable)
    {
        if (background != null)
            background.color = color;

        if (button != null)
            button.interactable = interactable;
    }

    public void OnClick()
    {
        Debug.Log($"[StageButton] Stage {stageNumber} 선택됨");
        saveService = new JsonSaveService();

        // 1. 먼저 현재 데이터 로드
        var latestData = saveService.Load();

        // 2. 코인 버퍼 적용
        latestData.coin += CoinBuffer.Instance.GetBufferedCoin();

        // 3. 스테이지 정보 적용
        latestData.currentStage = stageNumber;

        // 4. 저장
        saveService.Save(latestData);

        // 5. 버퍼 초기화
        CoinBuffer.Instance.ResetBuffer();

        // 6. 씬 전환
        stageSceneLoader.LoadStage(stageNumber, latestData.topStage);
    }
}

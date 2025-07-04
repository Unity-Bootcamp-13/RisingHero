public class StageButton : MonoBehaviour
{
    [Header("스테이지 번호")]
    [SerializeField] private int stageNumber;

    private Button button;
    private Image background;
    private TMP_Text stageText;
    private ISaveService saveService;

    public void Initialize(ISaveService saveService)
    {
        this.saveService = saveService;
        UpdateState();
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

    private void UpdateState()
    {
        if (saveService == null)
        {
            Debug.LogError("[StageButton] SaveService가 초기화되지 않았습니다.");
            return;
        }

        int topStage = saveService.Load().topStage;

        stageText.text = $"Stage{stageNumber}";

        if (stageNumber <= topStage)
        {
            SetState(Color.cyan, true);
        }
        else if (stageNumber == topStage + 1)
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
        // 스테이지 이동 처리 구현 예정
    }
}

using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StageButton : MonoBehaviour
{
    [Header("�������� ��ȣ")]
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
            Debug.LogError("[StageButton] ��ư �Ǵ� UI ������Ʈ �ڵ� ���� ����");
        }
    }

    private void UpdateState()
    {
        if (saveService == null)
        {
            Debug.LogError("[StageButton] SaveService�� �������� �ʾҽ��ϴ�.");
            return;
        }

        int topStage = saveService.Load().topStage;

        stageText.text = $"Stage{stageNumber}";

        if (stageNumber <= topStage)
        {
            SetState(Color.cyan, true); // Ŭ����� ��������
        }
        else if (stageNumber == topStage + 1)
        {
            SetState(new Color(0.7f, 0.5f, 1f), true); // ���� �������� (�����)
        }
        else
        {
            SetState(Color.gray, false); // ��� ��������
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
        Debug.Log($"[StageButton] Stage {stageNumber} ���õ�");

        // ���� �������� ���� ó�� ����
    }
}

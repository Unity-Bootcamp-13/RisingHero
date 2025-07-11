using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Diamond : MonoBehaviour
{
    [Header("UI 연결")]
    [SerializeField] private TextMeshProUGUI diamondText;
    [SerializeField] private Button addButton;

    [Header("설정")]
    [SerializeField] private int increaseAmount = 10;

    private ISaveService saveService;
    private PlayerSaveData saveData;

    private void Start()
    {
        // 저장 서비스 초기화 (간단한 로컬 저장용 JsonSaveService 사용)
        saveService = new JsonSaveService();
        saveData = saveService.Load();

        // 버튼 이벤트 연결
        if (addButton != null)
            addButton.onClick.AddListener(AddDiamond);

        // UI 초기 표시
        UpdateUI();
    }

    private void AddDiamond()
    {
        saveData.diamond += increaseAmount;
        saveService.Save(saveData);
        UpdateUI();

        Debug.Log($"다이아몬드 {increaseAmount}개 추가됨. 현재: {saveData.diamond}");
    }

    private void UpdateUI()
    {
        if (diamondText != null)
            diamondText.text = $"{saveData.diamond}";
    }
}

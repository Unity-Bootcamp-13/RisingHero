using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Diamond : MonoBehaviour
{
    [Header("UI ����")]
    [SerializeField] private TextMeshProUGUI diamondText;
    [SerializeField] private Button addButton;

    [Header("����")]
    [SerializeField] private int increaseAmount = 10;

    private ISaveService saveService;
    private PlayerSaveData saveData;

    private void Start()
    {
        // ���� ���� �ʱ�ȭ (������ ���� ����� JsonSaveService ���)
        saveService = new JsonSaveService();
        saveData = saveService.Load();

        // ��ư �̺�Ʈ ����
        if (addButton != null)
            addButton.onClick.AddListener(AddDiamond);

        // UI �ʱ� ǥ��
        UpdateUI();
    }

    private void AddDiamond()
    {
        saveData.diamond += increaseAmount;
        saveService.Save(saveData);
        UpdateUI();

        Debug.Log($"���̾Ƹ�� {increaseAmount}�� �߰���. ����: {saveData.diamond}");
    }

    private void UpdateUI()
    {
        if (diamondText != null)
            diamondText.text = $"{saveData.diamond}";
    }
}

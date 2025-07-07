using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using System.Linq;

public class SkillDetailUI : MonoBehaviour
{
    [Header("UI ����")]
    [SerializeField] private Image iconImage;
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text levelText;
    [SerializeField] private TMP_Text descriptionText;
    [SerializeField] private Button levelUpButton;
    [SerializeField] private Button equipButton;

    private SkillData currentSkillData;
    private SkillEquip skillEquip;
    private SkillLevelManager levelManager;
    private List<SkillEquipUI> equipSlotUIs;

    public void Initialize(SkillEquip skillEquip, SkillLevelManager levelManager, List<SkillEquipUI> equipSlotUIs)
    {
        this.skillEquip = skillEquip;
        this.levelManager = levelManager;
        this.equipSlotUIs = equipSlotUIs;

        UpdateEquipSlotUI();
    }

    public void SetSkill(SkillData data)
    {
        currentSkillData = data;

        if (data == null) return;

        iconImage.sprite = Resources.Load<Sprite>($"SkillIcons/{data.ID}");
        nameText.text = data.Name;
        levelText.text = $"Lv. {levelManager.GetLevel(data.ID)}";
        descriptionText.text = GetDescription(data);

        levelUpButton.onClick.RemoveAllListeners();
        levelUpButton.onClick.AddListener(OnClickLevelUp);

        equipButton.onClick.RemoveAllListeners();
        equipButton.onClick.AddListener(OnClickEquip);
    }

    private void OnClickLevelUp()
    {
        if (currentSkillData == null) return;

        if (levelManager.LevelUp(currentSkillData.ID))
        {
            levelText.text = $"Lv. {levelManager.GetLevel(currentSkillData.ID)}";
        }
    }

    private void OnClickEquip()
    {
        if (currentSkillData == null) return;

        var equipped = skillEquip.GetEquippedSkillIds();
        if (equipped.Contains(currentSkillData.ID))
        {
            Debug.Log("[SkillDetailUI] �̹� ������ ��ų�Դϴ�.");
            return;
        }

        if (equipped.Count >= equipSlotUIs.Count)
        {
            Debug.Log("[SkillDetailUI] �� ������ �����ϴ�.");
            return;
        }

        // ����� �ݿ�
        skillEquip.EquipSkill(currentSkillData.ID);

        // UI ���Կ� ǥ��
        UpdateEquipSlotUI();
    }

    private void UpdateEquipSlotUI()
    {
        var ids = skillEquip.GetEquippedSkillIds();

        for (int i = 0; i < equipSlotUIs.Count; i++)
        {
            SkillData data = (i < ids.Count) ? SkillLoader.Instance.GetSkillData(ids[i]) : null;
            equipSlotUIs[i].SetSkill(data);
        }
    }

    private string GetDescription(SkillData data)
    {
        return $"����: {data.ManaCost}\n����: {data.Range}\n��Ÿ��: {data.Cooldown}��\n�⺻ ������: {data.Power} (+{data.PowerPerLevel}/Lv)";
    }
}

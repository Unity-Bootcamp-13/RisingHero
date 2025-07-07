using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using System.Linq;

public class SkillDetailUI : MonoBehaviour
{
    [Header("UI 참조")]
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
            Debug.Log("[SkillDetailUI] 이미 장착된 스킬입니다.");
            return;
        }

        if (equipped.Count >= equipSlotUIs.Count)
        {
            Debug.Log("[SkillDetailUI] 빈 슬롯이 없습니다.");
            return;
        }

        // 저장소 반영
        skillEquip.EquipSkill(currentSkillData.ID);

        // UI 슬롯에 표시
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
        return $"마나: {data.ManaCost}\n범위: {data.Range}\n쿨타임: {data.Cooldown}초\n기본 데미지: {data.Power} (+{data.PowerPerLevel}/Lv)";
    }
}

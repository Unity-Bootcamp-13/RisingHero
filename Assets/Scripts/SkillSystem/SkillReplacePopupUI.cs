using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

public class SkillReplacePopupUI : MonoBehaviour
{
    [SerializeField] private List<Button> replaceSlotButtons;

    private SkillData skillToEquip;
    private SkillEquip skillEquip;
    private Action onReplaced;

    private void Awake()
    {
        gameObject.SetActive(false); // 초기 비활성
    }

    public void Show(SkillData skillData, SkillEquip equip, Action onReplaceComplete)
    {
        skillToEquip = skillData;
        skillEquip = equip;
        onReplaced = onReplaceComplete;

        var equippedIds = equip.GetEquippedSkillIds();

        for (int i = 0; i < replaceSlotButtons.Count; i++)
        {
            int index = i;

            var button = replaceSlotButtons[i];
            var image = button.GetComponent<Image>(); // Image만 사용

            SkillData data = (i < equippedIds.Count)
                ? SkillLoader.Instance.GetSkillData(equippedIds[i])
                : null;

            image.sprite = data != null
                ? Resources.Load<Sprite>($"SkillIcons/{data.ID}")
                : null;

            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(() =>
            {
                skillEquip.ReplaceSkillAtSlot(index, skillToEquip.ID);
                onReplaced?.Invoke();
                gameObject.SetActive(false);
            });
        }

        gameObject.SetActive(true);
    }
}
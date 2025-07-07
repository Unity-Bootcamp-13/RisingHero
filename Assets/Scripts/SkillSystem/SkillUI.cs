using UnityEngine;
using UnityEngine.UI;
using System;

public class SkillUI : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private Image iconImage;

    [Header("설정")]
    [SerializeField] private int skillId;

    private SkillData skillData;
    private Action<SkillData> onClickCallback;

    private void Start()
    {
        skillData = SkillLoader.Instance.GetSkillData(skillId);
        if (skillData == null)
        {
            Debug.LogError($"[SkillUI] Skill ID {skillId} 에 해당하는 스킬 데이터를 찾을 수 없습니다.");
            return;
        }

        var icon = Resources.Load<Sprite>($"SkillIcons/{skillData.ID}");
        if (icon != null)
        {
            iconImage.sprite = icon;
        }
    }

    public void OnClick()
    {
        if (skillData != null)
        {
            onClickCallback?.Invoke(skillData);
        }
    }

    public void SetCallback(Action<SkillData> callback)
    {
        this.onClickCallback = callback;
    }

    public int GetSkillId()
    {
        return skillId;
    }
}

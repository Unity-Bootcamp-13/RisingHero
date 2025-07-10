using UnityEngine;
using UnityEngine.UI;
using System;

public class SkillUI : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private Image iconImage;

    [Header("¼³Á¤")]
    [SerializeField] private int skillId;

    private SkillData skillData;
    private Action<SkillData> onClickCallback;

    private void Start()
    {
        skillData = SkillLoader.Instance.GetSkillData(skillId);
        if (skillData == null)
        {
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

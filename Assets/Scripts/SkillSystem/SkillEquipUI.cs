using UnityEngine;
using UnityEngine.UI;

public class SkillEquipUI : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private Image iconImage;
    [SerializeField] private Image cooldownOverlay;
    [SerializeField] private Sprite emptySprite;

    private SkillData currentSkillData;
    private SkillCaster skillCaster;

    public void Initialize(SkillCaster caster)
    {
        this.skillCaster = caster;
    }

    public void OnClick()
    {
        if (currentSkillData != null)
        {
            skillCaster?.CastSkill(currentSkillData, TriggerCooldown);
        }
    }

    public void SetSkill(SkillData data)
    {
        currentSkillData = data;

        if (data == null)
        {
            iconImage.sprite = emptySprite;
        }
        else
        {
            var sprite = Resources.Load<Sprite>($"SkillIcons/{data.ID}");
            iconImage.sprite = sprite != null ? sprite : emptySprite;
        }

        iconImage.gameObject.SetActive(true);
    }

    public void TriggerCooldown(float cooldown)
    {
        if (!gameObject.activeInHierarchy || cooldownOverlay == null) return;
        StartCoroutine(CooldownRoutine(cooldown));
    }

    private System.Collections.IEnumerator CooldownRoutine(float duration)
    {
        cooldownOverlay.gameObject.SetActive(true);
        cooldownOverlay.fillAmount = 1f;

        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            cooldownOverlay.fillAmount = 1f - (elapsed / duration);
            yield return null;
        }

        cooldownOverlay.fillAmount = 0f;
        cooldownOverlay.gameObject.SetActive(false);
    }

    public int GetSkillId()
    {
        return currentSkillData?.ID ?? -1;
    }
}

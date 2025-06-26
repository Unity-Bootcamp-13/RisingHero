using UnityEngine;
using UnityEngine.UI;

public class RewardIcon : MonoBehaviour
{
    [SerializeField] private Image iconImage;
    [SerializeField] private TMPro.TextMeshProUGUI nameText;

    public void Setup(ScriptableObject data)
    {
        if (data is WeaponData weapon)
        {
            iconImage.sprite = weapon.icon;
            nameText.text = weapon.name;
        }
        /*else if (data is SkillData skill)
        {
            iconImage.sprite = skill.icon;
            nameText.text = skill.skillName;
        }*/
    }
}

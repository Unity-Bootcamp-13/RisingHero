using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RewardIcon : MonoBehaviour
{
    [SerializeField] private Image iconImage;
    [SerializeField] private TMPro.TextMeshProUGUI nameText;

    private const string WEAPON_ICON_PATH = "Icons/";

    private static Dictionary<int, Sprite> iconCache = new Dictionary<int, Sprite>();

    public void Setup(int weaponId)
    {
        Sprite icon = SetupResourcesWeaponIcons(weaponId);
        iconImage.sprite = icon;

        /*else if (data is SkillData skill)
        {
            iconImage.sprite = skill.icon;
            nameText.text = skill.skillName;
        }*/
    }

    public Sprite SetupResourcesWeaponIcons(int weaponId)
    {
        if (!iconCache.TryGetValue(weaponId, out Sprite icon))
        {
            icon = Resources.Load<Sprite>($"{WEAPON_ICON_PATH}{weaponId}");
            if (icon == null)
            {
                Debug.LogWarning($"[Gacha] Sprite not found: {WEAPON_ICON_PATH}{weaponId}");
            }
            else
            {
                iconCache[weaponId] = icon;
            }
        }
        return icon;
    }
}
/*
    이거를 Icon을 Resources 폴더에서 가져오는 방식으로 수정.
    이 경우에 장점은 따로 Asset 파일에서 ICON을 가져오지 않아도 ID만으로 Weapon Amount 를 수정할 수 있음.
    다시 말하자면, ICON은 각자의 String으로 이름을 가진다. 나무활 Icon = 1, 단궁 ICON = 2, 장궁 ICON = 3 등등.
    그래서 나온 String ICON과 실제 뽑기에서 나온 ID가 동일하고, 또한 실제 무기 Asset의 ID가 동일하므로, ID를 기준으로 Weapon Amount를 추가할 수 있다.
    즉, 뽑기 시스템과 무기 시스템은 완전 분리가 가능해진다.
 */
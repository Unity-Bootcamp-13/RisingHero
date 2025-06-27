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
    �̰Ÿ� Icon�� Resources �������� �������� ������� ����.
    �� ��쿡 ������ ���� Asset ���Ͽ��� ICON�� �������� �ʾƵ� ID������ Weapon Amount �� ������ �� ����.
    �ٽ� �����ڸ�, ICON�� ������ String���� �̸��� ������. ����Ȱ Icon = 1, �ܱ� ICON = 2, ��� ICON = 3 ���.
    �׷��� ���� String ICON�� ���� �̱⿡�� ���� ID�� �����ϰ�, ���� ���� ���� Asset�� ID�� �����ϹǷ�, ID�� �������� Weapon Amount�� �߰��� �� �ִ�.
    ��, �̱� �ý��۰� ���� �ý����� ���� �и��� ����������.
 */
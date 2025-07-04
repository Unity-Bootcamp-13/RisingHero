using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// OnClick을 사용하기 때문에 Component
/// </summary>
public class GachaUI : MonoBehaviour
{
    private int weaponGachaGroupId = 101;
    private int skillGachaGroupId = 201;

    private int weaponSingleCost = 100;
    private int weaponMultiCost = 1000;
    private int skillSingleCost = 500;
    private int skillMultiCost = 2500;

    [Header("결과 패널 연결")]
    [SerializeField] private GameObject weaponGachaResultPanel;
    [SerializeField] private GameObject skillGachaResultPanel;

    private ISaveService saveService;
    private Gacha gacha;

    public void Initialize(ISaveService saveService, Gacha gacha)
    {
        this.saveService = saveService;
        this.gacha = gacha;
    }

    public void OnClickWeaponGachaOne() => TryGacha(weaponGachaGroupId, 1, weaponSingleCost, weaponGachaResultPanel);
    public void OnClickWeaponGachaTen() => TryGacha(weaponGachaGroupId, 10, weaponMultiCost, weaponGachaResultPanel);
    public void OnClickSkillGachaOne() => TryGacha(skillGachaGroupId, 1, skillSingleCost, skillGachaResultPanel);
    public void OnClickSkillGachaTen() => TryGacha(skillGachaGroupId, 10, skillMultiCost, skillGachaResultPanel);

    private void TryGacha(int groupId, int count, int cost, GameObject resultPanel)
    {
        var save = saveService.Load();
        if (save.diamond < cost)
        {
            Debug.Log("다이아몬드가 부족합니다.");
            return;
        }

        save.diamond -= cost;
        saveService.Save(save);

        List<int> results = new();
        for (int i = 0; i < count; i++)
        {
            results.Add(gacha.RollGacha(groupId));
        }

        resultPanel.SetActive(true);
        var gachaEffect = resultPanel.GetComponent<GachaEffect>();
        if (gachaEffect != null)
            StartCoroutine(gachaEffect.SpawnRewards(results));

        Debug.Log($"[GachaUI] group {groupId}에서 {count}회 가챠 완료");
    }
}
using System.Collections.Generic;
using UnityEngine;

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
    [SerializeField] private GameObject CloseButton;

    private ISaveService saveService;
    private Gacha gacha;
    private WeaponSlotUI weaponSlotUI;
    private WeaponStatus weaponStatus;

    public void Initialize(ISaveService saveService, Gacha gacha, WeaponSlotUI weaponSlotUI)
    {
        this.saveService = saveService;
        this.gacha = gacha;
        this.weaponSlotUI = weaponSlotUI;
    }

    public void SetWeaponStatus(WeaponStatus weaponStatus) // 추가
    {
        this.weaponStatus = weaponStatus;
    }

    public void OnClickWeaponGachaOne() => TryGacha(weaponGachaGroupId, 1, weaponSingleCost, weaponGachaResultPanel);
    public void OnClickWeaponGachaTen() => TryGacha(weaponGachaGroupId, 10, weaponMultiCost, weaponGachaResultPanel);
    public void OnClickSkillGachaOne() => TryGacha(skillGachaGroupId, 1, skillSingleCost, skillGachaResultPanel);
    public void OnClickSkillGachaTen() => TryGacha(skillGachaGroupId, 10, skillMultiCost, skillGachaResultPanel);

    private void TryGacha(int groupId, int count, int cost, GameObject resultPanel)
    {
        var save = saveService.Load();
        if (save.diamond < cost)
            return;

        CloseButton.SetActive(false);

        save.diamond -= cost;
        saveService.Save(save);

        List<int> results = new();
        for (int i = 0; i < count; i++)
        {
            results.Add(gacha.RollGacha(groupId));
        }

        resultPanel.SetActive(true);

        // 무기 가챠일 경우 상태 갱신
        if (groupId == weaponGachaGroupId)
        {
            saveService.ReloadFromFile();
            weaponStatus?.ApplyAllWeaponStats();
            weaponSlotUI?.Initialize(saveService);
        }

        var gachaEffect = resultPanel.GetComponent<GachaEffect>();
        if (gachaEffect != null)
            StartCoroutine(HandleGachaEffect(gachaEffect, results, groupId));
    }

    private System.Collections.IEnumerator HandleGachaEffect(GachaEffect gachaEffect, List<int> results, int groupId)
    {
        yield return StartCoroutine(gachaEffect.SpawnRewards(results, groupId));
        CloseButton.SetActive(true);
    }
}

using UnityEngine;
using System.Collections.Generic;

public class SkillManager : MonoBehaviour
{
    [Header("연결할 컴포넌트")]
    [SerializeField] private SkillEquip skillEquip;
    [SerializeField] private SkillCaster skillCaster;

    [Header("UI 컴포넌트")]
    [SerializeField] private List<SkillEquipUI> skillEquipUIs; // 우측 하단 4개 슬롯
    [SerializeField] private SkillDetailUI skillDetailUI;       // 좌측 상세정보 패널

    private ISaveService saveService;
    private SkillLevelManager skillLevelManager;

    [SerializeField] private List<SkillUI> skillUIs;

    private void Awake()
    {
        // 저장소 초기화
        saveService = new JsonSaveService();

        // 스킬 시스템 초기화
        skillEquip?.Initialize(saveService);

        skillLevelManager = new SkillLevelManager();
        skillLevelManager.Initialize(saveService);

        // 스킬 캐스터 연결
        skillCaster?.Initialize(skillEquip, skillLevelManager);

        // 슬롯 UI 초기화 (스킬 발동용)
        for (int i = 0; i < skillEquipUIs.Count; i++)
        {
            skillEquipUIs[i].Initialize(skillCaster);
        }

        // 상세 UI 연결
        skillDetailUI?.Initialize(skillEquip, skillLevelManager, skillEquipUIs);
    }

    private void Start()
    {
        foreach (var skillUI in skillUIs)
        {
            skillUI.SetCallback(skillDetailUI.SetSkill);
        }
    }

    private void OnApplicationQuit()
    {
        var save = saveService.Load();
        saveService.Save(save);
    }
}

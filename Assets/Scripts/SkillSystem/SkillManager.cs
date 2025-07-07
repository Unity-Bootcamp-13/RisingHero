using UnityEngine;
using System.Collections.Generic;

public class SkillManager : MonoBehaviour
{
    [Header("������ ������Ʈ")]
    [SerializeField] private SkillEquip skillEquip;
    [SerializeField] private SkillCaster skillCaster;

    [Header("UI ������Ʈ")]
    [SerializeField] private List<SkillEquipUI> skillEquipUIs; // ���� �ϴ� 4�� ����
    [SerializeField] private SkillDetailUI skillDetailUI;       // ���� ������ �г�

    private ISaveService saveService;
    private SkillLevelManager skillLevelManager;

    [SerializeField] private List<SkillUI> skillUIs;

    private void Awake()
    {
        // ����� �ʱ�ȭ
        saveService = new JsonSaveService();

        // ��ų �ý��� �ʱ�ȭ
        skillEquip?.Initialize(saveService);

        skillLevelManager = new SkillLevelManager();
        skillLevelManager.Initialize(saveService);

        // ��ų ĳ���� ����
        skillCaster?.Initialize(skillEquip, skillLevelManager);

        // ���� UI �ʱ�ȭ (��ų �ߵ���)
        for (int i = 0; i < skillEquipUIs.Count; i++)
        {
            skillEquipUIs[i].Initialize(skillCaster);
        }

        // �� UI ����
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

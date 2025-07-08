using UnityEngine;
using System.Collections.Generic;
using System.Reflection;

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

    [SerializeField] private PlayerMana playerMana;

    [SerializeField] private List<SkillUI> skillUIs;

    private void Awake()
    {
        saveService = new JsonSaveService();

        skillEquip?.Initialize(saveService);

        skillLevelManager = new SkillLevelManager();
        skillLevelManager.Initialize(saveService);

        skillCaster?.Initialize(skillEquip, skillLevelManager);

        for (int i = 0; i < skillEquipUIs.Count; i++)
        {
            skillEquipUIs[i].Initialize(skillCaster);
        }

        skillDetailUI?.Initialize(skillEquip, skillLevelManager, skillEquipUIs);

        skillCaster?.Initialize(skillEquip, skillLevelManager);
        skillCaster.GetType().GetField("playerMana", BindingFlags.NonPublic | BindingFlags.Instance)
                   ?.SetValue(skillCaster, playerMana);
    }

    private void Start()
    {
        foreach (var skillUI in skillUIs)
        {
            skillUI.SetCallback(skillDetailUI.SetSkill);
        }
    }
}

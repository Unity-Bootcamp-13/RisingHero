using UnityEngine;
using System.Collections.Generic;

public class SkillCaster : MonoBehaviour
{
    [SerializeField] private CharacterStats characterStats; // �������� ���� (MP, ���ݷ� etc)
    [SerializeField] private TargetManager targetManager;   // ���� Ÿ�� ����

    // ��ų�� ��Ÿ�� ���� Dictionary
    private Dictionary<SkillDefinition, float> skillCooldowns = new Dictionary<SkillDefinition, float>();

    // ��ų�� ���� ���� Dictionary
    private Dictionary<SkillDefinition, int> skillLevels = new Dictionary<SkillDefinition, int>();

    private SkillDefinition currentCastingSkill;            // ���� ���� ���� ��ų(���� �ð� �ִ� ��ų �뵵)
    private float currentCastTimer;

    public CharacterStats CharacterStats => characterStats;
    public TargetManager TargetManager => targetManager;

    private void Awake()
    {
        if (characterStats == null)
            characterStats = GetComponent<CharacterStats>();
        if (targetManager == null)
            targetManager = GetComponent<TargetManager>();

        // �ʱ� ��ų ���� ����, ������ �ε� �����ؾ� ��.
    }

    // Update is called once per frame
    private void Update()
    {
        // ��Ÿ�� �ǽð� �ݿ�
        List<SkillDefinition> skillsToUpdate = new List<SkillDefinition>(skillCooldowns.Keys);

        foreach (var skillDef in skillsToUpdate)
        {
            if (skillCooldowns[skillDef] > 0)
            {
                skillCooldowns[skillDef] -= Time.deltaTime;

                if (skillCooldowns[skillDef] < 0)
                    skillCooldowns[skillDef] = 0;   // ��Ÿ���� �ּڰ��� 0���� ����
                // ��ٿ� UI �ִϸ��̼� ���� �̺�Ʈ�� ȣ��
            }
        }

        // ���� ���� ���� ��ų ó��
        if (currentCastingSkill != null)
        {
            currentCastTimer -= Time.deltaTime;

            if (currentCastTimer <= 0)
            {
                // ���� ���� ��ų ���� �Ϸ� �� ��ų �ߵ�
                currentCastingSkill.ExecuteSkill(this, targetManager.GetCurrentTarget());
                currentCastingSkill = null;     // ���� �Ϸ�
            }
        }
    }

    /// <summary>
    /// ��ų ��� ��û
    /// </summary>
    /// <param name="skillToCast">����� ��ų ���� Scriptable Object</param>
    /// <param name="targetPosition">������ ��ų�� ��� ������ ��ġ</param>
    public bool TryCastSkill(SkillDefinition skillToCast, Vector3? targetPosition = null)
    {
        if (skillToCast == null)
            return false;

        // 1. ���� �ٸ� ��ų ���� ������ üũ
        if (currentCastingSkill != null)
        {
            Debug.Log($"�̹� {skillToCast.SkillName} ��ų�� ���� ���Դϴ�!");
            return false;
        }

        // 2. MP �Ҹ� �ڿ� üũ + ��Ÿ�� üũ
        if (!skillToCast.CanCastSkill(characterStats, skillCooldowns))
            return false;

        // 3. Ÿ���� ��ȿ�Ѱ�?
        Targetable currentTarget = targetManager.GetCurrentTarget();

        if (skillToCast is ProjectileSkillDefinition projectileSkill)
        {
            // Ÿ���� ����� ���, ���� ó����� �÷��̾��� ���� �������� ����ü �߻�
            if (currentTarget == null)
                Debug.LogWarning($"����ü ��ų {skillToCast.SkillName}�� Ÿ���� �ʿ�������, Ÿ���� �߰ߵ��� �ʾҽ��ϴ�.");
        }

        else if (skillToCast is AreaSkillDefinition areaSkill)
        {
            // ������ ��ų�� Ÿ���� �ʼ��� �ƴ�. (Ÿ���� ��������� ���� ���� ����)
            // Ư�� ��ġ Ŭ�� �� �����ϴ� ���, targetPosition�� �߿�
        }

        // �ٸ� ��ų Ÿ�� �߰� �� �� ���� if else if �߰�

        // ��� ���� ���� �� ��ų �ߵ� ����
        StartSkillExecution(skillToCast, currentTarget, targetPosition);
        return true;
    }

    private void StartSkillExecution(SkillDefinition skillToCast, Targetable target, Vector3? castPosition)
    {
        characterStats.CurrentMana -= skillToCast.ManaCost;     // ��ų�� ������ MP�� �Ҹ�

        // ��Ÿ�� ����
        skillCooldowns[skillToCast] = skillToCast.CoolDown;
        
        // ���⿡ ��ٿ� UI �ִϸ��̼� ȣ�� ���� �ۼ��� ����

        // ��ų ���� �ð� �ִٸ� ���� ����, �ƴϸ� ��� ����
        if (skillToCast.CastTime > 0)
        {
            currentCastingSkill = skillToCast;
            currentCastTimer = skillToCast.CastTime;
        }

        // ��� ���� ���� ��ų
        else
        {
            skillToCast.ExecuteSkill(this, target, castPosition);
        }
    }

    /// <summary>
    /// Ư�� ��ų�� ���� ���� ��ȯ ���
    /// </summary>
    public int GetSkillLevel(SkillDefinition skillDef)
    {
        return skillLevels.ContainsKey(skillDef) ? skillLevels[skillDef] : 1;
    }

    /// <summary>
    /// ��ų ������
    /// </summary>
    public void LevelUpSkill(SkillDefinition skillDef)
    {
        if (skillLevels.ContainsKey(skillDef))
        {
            skillLevels[skillDef]++;
            Debug.Log($"{skillDef.SkillName} ��ų�� ������ �߽��ϴ�.");
            // UI�� ��ų ���� �����ִ� ���, ��ų ���� ������Ʈ ��� ȣ��
        }

        else
        {
            skillLevels.Add(skillDef, 1);   // ��ų�� ó�� ���� ��, 1������ �߰�
            Debug.Log($"{skillDef.SkillName} ��ų�� ȹ���߽��ϴ�!");
        }
    }

    /// <summary>
    /// Ư�� ��ų�� ��Ÿ�� ����� ��ȯ (0 ~ 1)
    /// </summary>
    public float GetSkillCooldownProgress(SkillDefinition skillDef)
    {
        if (!skillCooldowns.ContainsKey(skillDef))
            return 1.0f;    // ��Ÿ�� ������ ��� ����

        float remaining = skillCooldowns[skillDef];

        if (remaining <= 0)
            return 1.0f;

        return 1.0f - (remaining / skillDef.CoolDown);
    }
}

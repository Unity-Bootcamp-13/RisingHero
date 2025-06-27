using UnityEngine;

[System.Serializable]   // �ν����Ϳ��� �� �� �ְ� �ϱ� ����. �����ص� ��
public class SkillData123
{
    public SkillDefinition Definition; // �� ��ų �����Ͱ� �����ϴ� SkillDefinition SO
    public float CurrentCooldown;      // ���� ���� ��Ÿ��
    public int CurrentSLevel;           // ���� ��ų ����

    public SkillData123(SkillDefinition definition, int initialLevel = 1)
    {
        Definition = definition;
        CurrentCooldown = 0f;
        CurrentSLevel = initialLevel;
    }

    public void UpdateCooldown(float deltaTime)
    {
        if (CurrentCooldown > 0)
        {
            CurrentCooldown -= deltaTime;
            if (CurrentCooldown < 0)
                CurrentCooldown = 0;
        }
    }

    // ��ų ������ ���
    public void LevelUp()
    {
        CurrentSLevel++;
        Debug.Log($"{Definition.SkillName} ��ų�� ������ �߽��ϴ�. ���� ����: {CurrentSLevel}");
        // ���⿡ ������ �� �߻��� �߰� ȿ�� (��: ����Ʈ, ����) ���� �߰� ����
    }

    // ��ų ��� �� ��Ÿ�� ���� ���
    public void ApplyCooldown()
    {
        CurrentCooldown = Definition.CoolDown;
    }
}

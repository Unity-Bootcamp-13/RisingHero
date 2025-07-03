using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SkillCaster : MonoBehaviour
{
    public List<SkillSlot> equippedSkills = new();

    // ��ų ���� �� SkillData�� ���ǵ� �����͸� �ҷ���
    public void CastSkill(SkillData data)
    {
        SkillSlot slot = FindSlot(data);

        // ��ų�� �ش� ���Կ� �����Ǿ� ���� ����
        if (slot == null)
        {
            Debug.LogWarning($"Skill [{data.Name}] �� �����Ǿ� ���� �ʽ��ϴ�.");
            return;
        }

        // ��ų ��Ÿ���� ��������
        if (slot.IsOnCooldown)
        {
            Debug.Log($"[{data.Name}] ��Ÿ�� ���� ��: {slot.CurrentCooldown:F1}s");
            return;
        }

        // ��ų ���丮���� ���ǵ� ��ų�� �����͸� ȣ��
        var behaviour = SkillBehaviourFactory.GetSkillBehaviour(data.Type);
        // ȣ��� �����Ϳ� �´� ��ų�� ������.
        behaviour.Execute(data, transform);

        StartCoroutine(Cor_Cooldown(slot));
    }

    private SkillSlot FindSlot(SkillData data)
    {
        for (int i = 0; i < equippedSkills.Count; i++)
        {
            if (equippedSkills[i].Skill.ID == data.ID)
                return equippedSkills[i];
        }

        return null;
    }

    public float GetSlotDamage(SkillData data)
    {
        SkillSlot slot = equippedSkills.Find(s => s.Skill.ID == data.ID);

        if (slot != null)
            return slot.FinalDamage;

        return data.Power;
    }

    private IEnumerator Cor_Cooldown(SkillSlot slot)
    {
        slot.CurrentCooldown = slot.Skill.CooldownTime;

        while (slot.CurrentCooldown > 0f)
        {
            slot.CurrentCooldown -= Time.deltaTime;
            yield return null;
        }

        slot.CurrentCooldown = 0f;
        // ��Ÿ�� ����
        Debug.Log($"[{slot.Skill.Name}] ��Ÿ�� ����");

        if (slot.IsAuto)
        {
            Debug.Log("�ڵ� ���� �����Դϴ�. ��ų�� �ڵ����� ���� �˴ϴ�.");
            CastSkill(slot.Skill);
        }
    }
}
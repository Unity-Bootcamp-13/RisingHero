using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���� Ȱ��ȭ�� ��ų���� �����ϰ� �ڵ����� ��Ÿ���� �����ϰ� �ߵ���Ű�� �Ŵ��� �Դϴ�.
/// </summary>
public class SkillManager : MonoBehaviour
{
    private readonly List<ISkill> skills = new();

    /// <summary>
    /// �÷��̾ ���ο� ��ų ���� �� ȣ��Ǿ� SkillManager�� ����մϴ�.
    /// </summary>
    /// <param name="skill">����� ��ų ��ü</param>
    public void AddSkill(ISkill skill)
    {
        skills.Add(skill);
    }

    private void Update()
    {
        float deltaTime = Time.deltaTime;

        // ��ϵǾ� �ִ� (���� �÷��̾ ������ �ִ�) ��� ��ų�� ��ȸ
        foreach (var skill in skills)
        {
            // �ð��� �帧�� �°� ��Ÿ�� ����
            skill.UpdateCooldown(deltaTime);
            
            // �ش� ��ų�� IActivatableSkill�� ������ ��Ƽ�� ��ų�� ���
            if (skill is IActivatableSkill activatable)
            {
                // ��ų �ߵ�, ���� �ٽ� ��Ÿ�� �ʱ�ȭ�Ͽ� ���� �ߵ����� ���
                activatable.Activate();
            }
        }
    }
}

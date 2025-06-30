using UnityEngine;

public class SkillCaster : MonoBehaviour
{
    // ��ų ���� �� SkillData�� ���ǵ� �����͸� �ҷ���
    public void CastSkill(SkillData data)
    {
        // ��ų ���丮���� ���ǵ� ��ų�� �����͸� ȣ��
        var behaviour = SkillBehaviourFactory.GetSkillBehaviour(data.Type);
        // ȣ��� �����Ϳ� �´� ��ų�� ������.
        behaviour.Execute(data, transform);
    }
}
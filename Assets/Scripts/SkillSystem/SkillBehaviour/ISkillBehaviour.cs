using UnityEngine;

public interface ISkillBehaviour
{
    // ��ų ���� ��, SkillData���� ������ ID, �̸�, Ÿ�� ���� �����Ϳ� �������� ��ġ�� �޾ƿ� ����
    void Execute(SkillData data, Transform casterTransform);
}
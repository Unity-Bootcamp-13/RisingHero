using UnityEngine;

public class TargetManager123 : MonoBehaviour
{
    private Targetable123 currentTarget;

    public Targetable123 GetCurrentTarget()
    {
        // �����δ� �÷��̾� �Է�, ���콺 ����, �ڵ� Ÿ���� ���� �� ���� �ʿ�
        // ����� �ӽ÷� Scene�� �ִ� Ư�� Ÿ���� ��ȯ�ϴ� ����
        if (currentTarget == null)
        {
            // ���� ����� Ÿ���� ã�ų�, Ư�� Tag�� Layer�� ���� ������Ʈ�� ã�� �� ����
            // currentTarget = FindObjectOfType<Targetable123>();
        }

        return currentTarget;
    }

    public void SetTarget(Targetable123 newTarget)
    {
        currentTarget = newTarget;
        Debug.Log($"���ο� Ÿ�� ���� : {newTarget?.name ?? "None"}");
    }
}

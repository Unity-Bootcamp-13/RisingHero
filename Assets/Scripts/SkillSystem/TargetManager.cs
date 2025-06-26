using UnityEngine;

public class TargetManager : MonoBehaviour
{
    private Targetable currentTarget;

    public Targetable GetCurrentTarget()
    {
        // �����δ� �÷��̾� �Է�, ���콺 ����, �ڵ� Ÿ���� ���� �� ���� �ʿ�
        // ����� �ӽ÷� Scene�� �ִ� Ư�� Ÿ���� ��ȯ�ϴ� ����
        if (currentTarget == null)
        {
            // ���� ����� Ÿ���� ã�ų�, Ư�� Tag�� Layer�� ���� ������Ʈ�� ã�� �� ����
            // currentTarget = FindObjectOfType<Targetable>();
        }

        return currentTarget;
    }

    public void SetTarget(Targetable newTarget)
    {
        currentTarget = newTarget;
        Debug.Log($"���ο� Ÿ�� ���� : {newTarget?.name ?? "None"}");
    }
}

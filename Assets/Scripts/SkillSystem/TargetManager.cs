using UnityEngine;

public class TargetManager : MonoBehaviour
{
    private Targetable currentTarget;

    public Targetable GetCurrentTarget()
    {
        // 실제로는 플레이어 입력, 마우스 오버, 자동 타겟팅 로직 등 구현 필요
        // 현재는 임시로 Scene에 있는 특정 타겟을 반환하는 기준
        if (currentTarget == null)
        {
            // 가장 가까운 타겟을 찾거나, 특정 Tag나 Layer를 가진 오브젝트를 찾을 수 있음
            // currentTarget = FindObjectOfType<Targetable>();
        }

        return currentTarget;
    }

    public void SetTarget(Targetable newTarget)
    {
        currentTarget = newTarget;
        Debug.Log($"새로운 타겟 지정 : {newTarget?.name ?? "None"}");
    }
}

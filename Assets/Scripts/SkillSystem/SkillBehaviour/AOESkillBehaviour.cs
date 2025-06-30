using UnityEngine;

// 플레이어가 시전하는 범위 지정형 스킬의 정의
public class AOESkillBehaviour : ISkillBehaviour
{
    public void Execute(SkillData data, Transform casterTransform)
    {
        var go = GameObject.Instantiate(Resources.Load<GameObject>(data.PrefabName), casterTransform.position, Quaternion.identity);

        // 범위 피해 처리 로직 추가 필요
    }
}
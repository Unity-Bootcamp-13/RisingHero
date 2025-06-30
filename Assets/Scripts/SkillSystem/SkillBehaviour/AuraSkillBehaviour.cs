using UnityEngine;

// 플레이어 중심의 오라형 스킬에 대한 정의
public class AuraSkillBehaviour : ISkillBehaviour
{
    public void Execute(SkillData data, Transform casterTransform)
    {
        var aura = GameObject.Instantiate(Resources.Load<GameObject>(data.PrefabName), casterTransform.position, Quaternion.identity);
        aura.transform.SetParent(casterTransform); // 플레이어의 자식으로 넣어야 함
    }
}
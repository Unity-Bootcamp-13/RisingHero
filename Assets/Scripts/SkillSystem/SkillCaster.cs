using UnityEngine;

public class SkillCaster : MonoBehaviour
{
    // 스킬 실행 시 SkillData에 정의된 데이터를 불러옴
    public void CastSkill(SkillData data)
    {
        // 스킬 팩토리에서 정의된 스킬의 데이터를 호출
        var behaviour = SkillBehaviourFactory.GetSkillBehaviour(data.Type);
        // 호출된 데이터에 맞는 스킬을 실행함.
        behaviour.Execute(data, transform);
    }
}
using UnityEngine;

public interface ISkillBehaviour
{
    // 스킬 실행 시, SkillData에서 정의한 ID, 이름, 타입 등의 데이터와 시전자의 위치를 받아와 실행
    void Execute(SkillData data, Transform casterTransform);
}
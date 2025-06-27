using System.Collections.Generic;
using UnityEngine;

public class SkillCaster : MonoBehaviour
{
    [SerializeField] private List<SkillBase> equippedSkills = new();
    private float[] lastCastTimes;

    private void Awake()
    {
        lastCastTimes = new float[equippedSkills.Count];
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < equippedSkills.Count; i++)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                TryCastSkill(i);
            }
        }
    }

    private void TryCastSkill(int index)
    {
        var skill = equippedSkills[index];
        if (skill.CanCast(lastCastTimes[index]))
        {
            Vector2 dir = GetCastDirection(); // 타겟팅 방향 등
            skill.Activate(gameObject, dir);
            lastCastTimes[index] = Time.time;
        }
    }

    private Vector2 GetCastDirection()
    {
        return Vector2.right; // 예시. 실제 구현은 마우스, 플레이어 방향 등
    }
}

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
            Vector2 dir = GetCastDirection(); // Ÿ���� ���� ��
            skill.Activate(gameObject, dir);
            lastCastTimes[index] = Time.time;
        }
    }

    private Vector2 GetCastDirection()
    {
        return Vector2.right; // ����. ���� ������ ���콺, �÷��̾� ���� ��
    }
}

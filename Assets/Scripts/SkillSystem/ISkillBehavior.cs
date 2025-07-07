using UnityEngine;

public interface ISkillBehavior
{
    void Execute(SkillData data, Transform casterTransform, int level);
}

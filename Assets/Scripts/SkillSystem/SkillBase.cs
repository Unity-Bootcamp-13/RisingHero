using UnityEngine;

[CreateAssetMenu(fileName = "SkillBase", menuName = "Scriptable Objects/SkillBase")]
public abstract class SkillBase : ScriptableObject
{
    public string skillName;    // 스킬 이름
    public Sprite icon;         // 스킬 아이콘
    public float cooldown;      // 스킬 쿨타임
    public float damage;        // 스킬 피해량

    public abstract void Activate(GameObject caster, Vector2 direction);

    // 공통 유틸 예: 쿨타임 검사
    public virtual bool CanCast(float lastCastTime)
    {
        return Time.time >= lastCastTime + cooldown;
    }
}

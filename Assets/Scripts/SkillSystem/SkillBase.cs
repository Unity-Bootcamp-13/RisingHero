using UnityEngine;

[CreateAssetMenu(fileName = "SkillBase", menuName = "Scriptable Objects/SkillBase")]
public abstract class SkillBase : ScriptableObject
{
    public string skillName;    // ��ų �̸�
    public Sprite icon;         // ��ų ������
    public float cooldown;      // ��ų ��Ÿ��
    public float damage;        // ��ų ���ط�

    public abstract void Activate(GameObject caster, Vector2 direction);

    // ���� ��ƿ ��: ��Ÿ�� �˻�
    public virtual bool CanCast(float lastCastTime)
    {
        return Time.time >= lastCastTime + cooldown;
    }
}

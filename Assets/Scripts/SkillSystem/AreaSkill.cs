using UnityEngine;

[CreateAssetMenu(menuName = "Skill/Area")]
public class AreaSkill : SkillBase
{
    public GameObject areaEffectPrefab;     // ������ ��ƼŬ ������
    public float areaRadius = 3f;           // ������ ���� ���� (�ӽ�)
    public float duration = 2f;             // ������ ���� ���ӽð� (�ӽ�, 0���� ���� �� �ѹ� ������ ��)
    public float tickInterval = 0.5f;       // ���ӵǴ� �������� ������ ó�� �ֱ�

    public override void Activate(GameObject caster, Vector2 direction)
    {
        Vector2 spawnPos = (Vector2)caster.transform.position;
        var effect = Instantiate(areaEffectPrefab, spawnPos, Quaternion.identity);
        var area = effect.GetComponent<AreaEffect>();
        area.Initialize(damage, areaRadius, duration, tickInterval);
    }
}

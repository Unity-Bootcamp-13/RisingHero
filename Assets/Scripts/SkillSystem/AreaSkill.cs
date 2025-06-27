using UnityEngine;

[CreateAssetMenu(menuName = "Skill/Area")]
public class AreaSkill : SkillBase
{
    public GameObject areaEffectPrefab;     // 광역기 파티클 프리팹
    public float areaRadius = 3f;           // 광역기 범위 지정 (임시)
    public float duration = 2f;             // 광역기 범위 지속시간 (임시, 0으로 지정 시 한번 때리고 끝)
    public float tickInterval = 0.5f;       // 지속되는 범위기의 데미지 처리 주기

    public override void Activate(GameObject caster, Vector2 direction)
    {
        Vector2 spawnPos = (Vector2)caster.transform.position;
        var effect = Instantiate(areaEffectPrefab, spawnPos, Quaternion.identity);
        var area = effect.GetComponent<AreaEffect>();
        area.Initialize(damage, areaRadius, duration, tickInterval);
    }
}

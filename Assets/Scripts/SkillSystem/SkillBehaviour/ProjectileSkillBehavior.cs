using UnityEngine;

// 플레이어가 발사하는 투사체 스킬의 정의
public class ProjectileSkillBehavior : ISkillBehaviour
{
    public void Execute(SkillData data, Transform casterTransform)
    {
        var prefab = Resources.Load<GameObject>(data.PrefabName);
        GameObject projectile = Object.Instantiate(prefab, casterTransform.position, Quaternion.identity);

        // 투사체 이동 로직 추가 필요
    }
}
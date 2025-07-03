using UnityEngine;

public class Arrow : MonoBehaviour
{
    private const string poolKey = "Arrow";
    private const float speed = 10f;
    private const float lifetime = 5f;

    private Vector2 direction;
    private int baseDamage;
    private float critChance;
    private float critMultiplier;
    private float timer;

    public void Shoot(Vector2 direction, int damage, float critChance, float critMultiplier)
    {
        this.direction = direction.normalized;
        this.baseDamage = damage;
        this.critChance = critChance;
        this.critMultiplier = critMultiplier;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle + 90f);
        Debug.Log($"[Arrow] Shoot with baseDamage={damage}");
    }

    private void Update()
    {
        transform.position += (Vector3)(direction * speed * Time.deltaTime);
        timer += Time.deltaTime;

        if (timer >= lifetime)
        {
            Despawn();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy") && other.TryGetComponent<IDamageable>(out var target))
        {
            int finalDamage = baseDamage;
            if (Random.value < critChance)
            {
                finalDamage = Mathf.RoundToInt(baseDamage * critMultiplier);
                Debug.Log($"치명타! {finalDamage} 데미지");
            }
            target.TakeDamage(finalDamage);
            Despawn();
        }
    }

    private void Despawn()
    {
        if (ProjectilePool.Instance != null)
            ProjectilePool.Instance.Despawn(poolKey, gameObject);
        else
            gameObject.SetActive(false);
    }
}

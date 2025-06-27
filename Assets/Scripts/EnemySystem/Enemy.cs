using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    private DamageTextSpawner damageTextSpawner;

    [Header("Stats")]
    [SerializeField] private int attackDamage = 5;
    [SerializeField] private float hitCooldown = 1f;
    private float lastHitTime = -999f;

    [Header("Coin")]
    [SerializeField][Range(0f, 1f)] private float coinDropChance = 0.2f;
    [SerializeField] private int coinValue = 1;
    [SerializeField] private PoolType coinPoolType = PoolType.Coin;

    private EnemyHealth health;
    private SpriteRenderer myRenderer;
    private Color originalColor;

    private void Awake()
    {
        GameObject stageManagerObj = GameObject.Find("StageManager");
        if (stageManagerObj != null)
        {
            damageTextSpawner = stageManagerObj.GetComponent<DamageTextSpawner>();
        }

        health = GetComponent<EnemyHealth>();
        health.OnDie += Die;
        health.OnHealthChanged += OnHpChanged;

        health.OnDamaged += damage =>
        {
            damageTextSpawner?.ShowDamageText(damage, transform.position);
        };

        myRenderer = GetComponent<SpriteRenderer>();
        if (myRenderer != null)
            originalColor = myRenderer.color;
    }

    private void Die()
    {
        gameObject.SetActive(false);

        if (Random.value <= coinDropChance)
        {
            var coin = StrictPool.Instance.SpawnFromPool(coinPoolType, transform.position, Quaternion.identity);

            if (coin.TryGetComponent(out Coin coinComponent))
            {
                coinComponent.SetValue(coinValue);
            }
        }
    }

    private void OnHpChanged(float ratio)
    {
        if (gameObject.activeInHierarchy)
            StartCoroutine(HitFlashEffect());
    }

    private void OnEnable()
    {
        if (myRenderer != null)
        {
            myRenderer.color = originalColor;
        }

        StopAllCoroutines();
        health?.ResetHealth();

        AliveEnemyManager.Register(this);
    }

    private void OnDisable()
    {
        AliveEnemyManager.Unregister(this);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && Time.time - lastHitTime >= hitCooldown)
        {
            lastHitTime = Time.time;

            if (other.TryGetComponent(out IDamageable damageable))
            {
                damageable.TakeDamage(attackDamage);
            }
        }
    }

    private IEnumerator HitFlashEffect()
    {
        if (myRenderer == null) yield break;

        Color hitColor = Color.red;
        float duration = 0.2f;

        myRenderer.color = hitColor;
        yield return new WaitForSeconds(duration);
        myRenderer.color = originalColor;
    }
}
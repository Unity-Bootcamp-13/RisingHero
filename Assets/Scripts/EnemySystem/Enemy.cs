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

    [SerializeField] private KillCounter killCounter; // 주입 확인

    internal EnemyHealth health;
    private SpriteRenderer myRenderer;
    private Color originalColor;

    /// <summary>
    /// 얘가 StageManger를 찾는 형식으로 구현된 이유는
    /// Prefab의 형태로 저장되어 있기때문에 Inspector창에서 참조 받을 수 없기 때문이다.
    /// </summary>
    /// 
    public void Initialize(KillCounter killCounter)
    {
        this.killCounter = killCounter;
    }

    protected virtual void Awake()
    {
        GameObject stageManagerObj = GameObject.Find("StageManager"); // 이 방식 고정.
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

        myRenderer = GetComponent<SpriteRenderer>(); // 피격 시 빨간색으로 Sprite가 변하는데, 다시 소환 됐을때 원래 색깔로 돌리는 코드.
        if (myRenderer != null)
            originalColor = myRenderer.color;
    }

    protected virtual void Die()
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

        if(killCounter != null)
            killCounter.AddKill();
    }

    private void OnHpChanged(float ratio)
    {
        if (gameObject.activeInHierarchy)
            StartCoroutine(HitFlashEffect());
    }

    protected virtual void OnEnable()
    {
        if (myRenderer != null)
        {
            myRenderer.color = originalColor;
        }

        StopAllCoroutines();
        health?.ResetHealth();

        AliveEnemyManager.Register(this);
        Debug.Log($"[Enemy] 등록됨: {gameObject.name}");
    }

    protected virtual void OnDisable()
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
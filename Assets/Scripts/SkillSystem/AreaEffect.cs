using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(Collider2D))]
public class AreaEffect : MonoBehaviour
{
    private float damage;
    private float radius;
    private float duration;
    private float tickInterval;

    private float elapsedTime = 0f;
    private float tickTimer = 0f;

    // Update is called once per frame
    private void Update()
    {
        elapsedTime += Time.deltaTime;
        tickTimer += Time.deltaTime;

        if (tickTimer >= tickInterval)
        {
            tickTimer = 0f;
            ApplyDamage();
        }

        if (elapsedTime >= duration)
        {
            Destroy(gameObject);
        }
    }

    public void Initialize(float damage, float radius, float duration, float tickInterval)
    {
        this.damage = damage;
        this.radius = radius;
        this.duration = duration;
        this.tickInterval = tickInterval;
    }

    private void ApplyDamage()
    {
        var hits = Physics2D.OverlapCircleAll(transform.position, radius);
        foreach (var hit in hits)
        {
            if (hit.TryGetComponent<IDamageable>(out var target))
            {
                target.TakeDamage((int)damage);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}

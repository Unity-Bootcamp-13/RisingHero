using UnityEngine;
using System.Collections;

[RequireComponent(typeof(LineRenderer))]
public class LaserShooter : MonoBehaviour
{
    [Header("레이저 설정")]
    [SerializeField] private Transform target;
    [SerializeField] private float laserLength = 10f;
    [SerializeField] private float fireInterval = 2f;
    [SerializeField] private float laserDuration = 0.5f;
    [SerializeField] private int damage = 10;

    [Header("경고 이펙트")]
    [SerializeField] private GameObject warningPrefab;
    [SerializeField] private float warningDuration = 0.5f;

    [Header("감지 설정")]
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private float maxLaserWidth = 0.2f;

    private LineRenderer lineRenderer;
    private float timer;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 2;
        lineRenderer.enabled = false;
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= fireInterval)
        {
            timer = 0f;
            StartCoroutine(FireWithWarning());
        }
    }

    private IEnumerator FireWithWarning()
    {
        if (target == null) yield break;

        Vector3 start = transform.position;
        Vector3 direction = (target.position - start).normalized;

        // --- 경고 표시 ---
        GameObject warning = null;
        if (warningPrefab != null)
        {
            warning = Instantiate(warningPrefab, start, Quaternion.LookRotation(Vector3.forward, direction));
            Destroy(warning, warningDuration + 0.1f); // 자동 제거
        }

        yield return new WaitForSeconds(warningDuration);

        // --- 레이저 실제 발사 ---
        yield return StartCoroutine(AnimateLaser(start, direction));
    }

    private IEnumerator AnimateLaser(Vector3 start, Vector3 direction)
    {
        float halfDuration = laserDuration / 2f;
        Vector3 end = start + direction * laserLength;
        float t = 0f;

        lineRenderer.enabled = true;
        lineRenderer.SetPosition(0, start);
        lineRenderer.SetPosition(1, end);

        // 확장: 굵어짐만
        while (t < halfDuration)
        {
            t += Time.deltaTime;
            float progress = Mathf.Clamp01(t / halfDuration);
            float width = Mathf.Lerp(0f, maxLaserWidth, progress);

            lineRenderer.startWidth = width;
            lineRenderer.endWidth = width;

            yield return null;
        }

        // 데미지 판정 (길이 고정 상태에서)
        RaycastHit2D hit = Physics2D.Raycast(start, direction, laserLength, playerLayer);
        if (hit.collider != null && hit.collider.TryGetComponent(out IDamageable damageTarget))
        {
            damageTarget.TakeDamage(damage);
        }

        yield return new WaitForSeconds(0.05f); // 유지 잠깐

        // 축소: 얇아짐만
        t = 0f;
        while (t < halfDuration)
        {
            t += Time.deltaTime;
            float progress = Mathf.Clamp01(t / halfDuration);
            float width = Mathf.Lerp(maxLaserWidth, 0f, progress);

            lineRenderer.startWidth = width;
            lineRenderer.endWidth = width;

            yield return null;
        }

        lineRenderer.enabled = false;
    }

}

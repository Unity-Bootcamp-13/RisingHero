using UnityEngine;
using System.Collections;

[RequireComponent(typeof(LineRenderer))]
public class LaserShooter : MonoBehaviour
{
    [Header("������ ����")]
    [SerializeField] private Transform target;
    [SerializeField] private float laserLength = 10f;
    [SerializeField] private float fireInterval = 2f;
    [SerializeField] private float laserDuration = 0.5f;
    [SerializeField] private int damage = 10;

    [Header("��� ����Ʈ")]
    [SerializeField] private GameObject warningPrefab;
    [SerializeField] private float warningDuration = 0.5f;

    [Header("���� ����")]
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

        // --- ��� ǥ�� ---
        GameObject warning = null;
        if (warningPrefab != null)
        {
            warning = Instantiate(warningPrefab, start, Quaternion.LookRotation(Vector3.forward, direction));
            Destroy(warning, warningDuration + 0.1f); // �ڵ� ����
        }

        yield return new WaitForSeconds(warningDuration);

        // --- ������ ���� �߻� ---
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

        // Ȯ��: ��������
        while (t < halfDuration)
        {
            t += Time.deltaTime;
            float progress = Mathf.Clamp01(t / halfDuration);
            float width = Mathf.Lerp(0f, maxLaserWidth, progress);

            lineRenderer.startWidth = width;
            lineRenderer.endWidth = width;

            yield return null;
        }

        // ������ ���� (���� ���� ���¿���)
        RaycastHit2D hit = Physics2D.Raycast(start, direction, laserLength, playerLayer);
        if (hit.collider != null && hit.collider.TryGetComponent(out IDamageable damageTarget))
        {
            damageTarget.TakeDamage(damage);
        }

        yield return new WaitForSeconds(0.05f); // ���� ���

        // ���: �������
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

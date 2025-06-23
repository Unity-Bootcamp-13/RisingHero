using TMPro;
using UnityEngine;

public class DamageTextSpawner : MonoBehaviour
{
    [SerializeField] private PoolType defaultDamageType = PoolType.DamageText;
    [SerializeField] private Canvas worldCanvas;
    [SerializeField] private Vector3 offset = new(0, 1f, 0);

    private void Awake()
    {
        // null Check ÇØ¾ßÇÔ
    }

    public void ShowDamageText(int damage, Vector3 worldPosition, bool isCritical = false)
    {
        if (worldCanvas == null) return;

        Vector3 spawnPos = worldPosition + offset;

        PoolType type = isCritical ? PoolType.DamageTextCritical : defaultDamageType;

        GameObject textObj = StrictPool.Instance.SpawnFromPool(
            type, spawnPos, Quaternion.identity, worldCanvas.transform
        );

        if (textObj != null && textObj.TryGetComponent(out TextMeshProUGUI tmp))
        {
            tmp.text = damage.ToString();
        }
    }
}

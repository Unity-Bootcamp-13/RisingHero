using UnityEngine;
using System.Collections;
using static EnemyPool;

public class EnemySpawner : MonoBehaviour
{
    [Header("참조")]
    [SerializeField] private Transform player;

    [Header("스폰 설정")]
    public Vector2 spawnAreaSize = new Vector2(8f, 5f);

    [Header("Wave 설정")]
    [SerializeField] private int startWaveMonsterCount = 5;
    public float delayBetweenWaves = 3f;

    private int currentWave = 0;

    private void Start()
    {
        StartCoroutine(SpawnWaves());
    }

    private IEnumerator SpawnWaves()
    {
        while (true)
        {
            currentWave++;
            int monsterCount = startWaveMonsterCount + currentWave - 1;

            SpawnEnemy(EnemyType.Normal0, monsterCount);

            yield return new WaitUntil(() => AliveEnemyManager.GetAliveEnemyCount() == 0);

            PullCoinsToPlayer();
            CoinBuffer.Instance.ApplyBufferedCoins();
            CoinBuffer.Instance.ResetBuffer();

            yield return new WaitForSeconds(delayBetweenWaves);
        }
    }

    private void SpawnEnemy(EnemyType type, int count)
    {
        for (int i = 0; i < count; i++)
        {
            GameObject enemy = EnemyPool.Instance.GetEnemy(type);
            if (enemy != null)
            {
                enemy.transform.position = GetRandomPositionNearPlayer();
                enemy.SetActive(true);

                // Enemy 컴포넌트를 얻어와서 등록
                Enemy enemyComponent = enemy.GetComponent<Enemy>();
                if (enemyComponent != null)
                {
                    AliveEnemyManager.Register(enemyComponent);
                }
            }
        }
    }

    private Vector3 GetRandomPositionNearPlayer()
    {
        Vector2 offset = new Vector2(
            Random.Range(-spawnAreaSize.x / 2f, spawnAreaSize.x / 2f),
            Random.Range(-spawnAreaSize.y / 2f, spawnAreaSize.y / 2f)
        );

        Vector3 spawnPos = player.position + (Vector3)offset;
        spawnPos.z = 0f;
        return spawnPos;
    }

    private void PullCoinsToPlayer()
    {
        var coins = FindObjectsByType<Coin>(FindObjectsSortMode.None);
        foreach (var coin in coins)
        {
            coin.PullTowardPlayer(player);
        }
    }
}

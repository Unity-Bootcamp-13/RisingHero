using UnityEngine;
using System.Collections;
using static EnemyPool;

public class EnemySpawner : MonoBehaviour
{
    [Header("����")]
    [SerializeField] private Transform player;
    [SerializeField] private KillCounter killCounter; // �� ���� ����

    private ISaveService saveService;

    [Header("���� ����")]
    public Vector2 spawnAreaSize = new Vector2(8f, 5f);

    [Header("Wave ����")]
    [SerializeField] private int startWaveMonsterCount = 5;
    public float delayBetweenWaves = 3f;

    private int currentWave = 0;


    public void Initialize(ISaveService saveService)
    {
        this.saveService = saveService;
    }

    private void Start()
    {
        if (saveService == null)
        {
            Debug.LogError("[EnemySpawner] SaveService�� �ʱ�ȭ���� �ʾҽ��ϴ�.");
            return;
        }

        StartCoroutine(SpawnWaves());
    }

    private IEnumerator SpawnWaves()
    {
        while (true)
        {
            currentWave++;
            int monsterCount = startWaveMonsterCount + currentWave - 1;

            int stage = saveService.Load().currentStage;
            EnemyType? type = GetEnemyTypeForStage(stage);

            if (type.HasValue)
            {
                SpawnEnemy(type.Value, monsterCount);
            }
            else
            {
                Debug.Log($"[EnemySpawner] Stage {stage}�� ���� ������ �����ϴ� (��: ���� ��������).");
            }

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

                if (enemy.TryGetComponent(out Enemy enemyComponent))
                {
                    enemyComponent.Initialize(killCounter);
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

    private EnemyType? GetEnemyTypeForStage(int stageId)
    {
        var saveData = saveService.Load();

        if (stageId % 10 == 0)
        {
            // BossStage: �� ����
            return null;
        }
        else if (stageId == saveData.topStage + 1)
        {
            // EliteStage
            if (stageId >= 11 && stageId <= 19)
                return EnemyType.Elite1;
            else if (stageId >= 21 && stageId <= 29)
                return EnemyType.Elite2;
        }
        else
        {
            // CommonStage
            if (stageId >= 11 && stageId <= 19)
                return EnemyType.Normal1;
            else if (stageId >= 21 && stageId <= 29)
                return EnemyType.Normal2;
        }

        return null;
    }

    public void SetKillCounter(KillCounter killCounter)
    {
        this.killCounter = killCounter;
    }
}

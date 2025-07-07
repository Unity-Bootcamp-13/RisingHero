using UnityEngine;

public class CoinBuffer : MonoBehaviour
{
    public static CoinBuffer Instance { get; private set; }

    [SerializeField] private CoinUI coinUI;

    private int bufferedCoin = 0;
    private ISaveService saveService;

    public void Initialize(ISaveService saveService)
    {
        this.saveService = saveService;
    }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void AddBufferedCoin(int value)
    {
        bufferedCoin += value;
    }

    public int GetBufferedCoin()
    {
        return bufferedCoin;
    }

    public void ApplyBufferedCoins()
    {
        if (saveService == null)
        {
            Debug.LogError("[CoinBuffer] SaveService가 초기화되지 않았습니다.");
            return;
        }

        saveService = new JsonSaveService();
        var saveData = saveService.Load();
        saveData.coin += bufferedCoin;
        saveService.Save(saveData);
        bufferedCoin = 0;

        coinUI?.UpdateCoinUI();
    }

    public void ResetBuffer()
    {
        bufferedCoin = 0;
    }
}

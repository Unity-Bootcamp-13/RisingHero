using UnityEngine;
using TMPro;

public class CoinUI : MonoBehaviour
{
    [SerializeField] private TMP_Text coinText;
    private ISaveService saveService;

    public void Initialize(ISaveService saveService)
    {
        this.saveService = saveService;
    }

    private void Start()
    {
        UpdateCoinUI();
    }

    public void UpdateCoinUI()
    {
        if (saveService == null)
        {
            Debug.LogError("[CoinUI] SaveService가 초기화되지 않았습니다.");
            return;
        }

        int coin = saveService.Load().coin;
        coinText.text = coin.ToString();
    }
}

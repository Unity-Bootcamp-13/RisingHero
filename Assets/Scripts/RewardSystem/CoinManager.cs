using UnityEngine;

public class CoinManager : MonoBehaviour
{
    [SerializeField] private CoinBuffer coinBuffer;
    [SerializeField] private CoinUI coinUI;

    private ISaveService saveService;

    private void Awake()
    {
        saveService = new JsonSaveService();

        coinBuffer?.Initialize(saveService);
        coinUI?.Initialize(saveService);
    }
}

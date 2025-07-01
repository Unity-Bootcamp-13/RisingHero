using UnityEngine;

public interface IDiamondManager
{
    public void CostDiamond(int cost);
    public void GetDiamond(int amount);
    public bool HasEnoughDiamond(int cost);
}

public class DiamondManager : MonoBehaviour, IDiamondManager
{
    [SerializeField] private DiamondUI diamondUI;

    private IDiamondService diamondService;

    public static IDiamondManager Instance { get; private set; }

    private PlayerSaveData data;

    private void Awake()
    {
        ISaveService saveService = new JsonSaveService();
        diamondService = new DiamondService(saveService);

        if (Instance != null && !ReferenceEquals(Instance, this))
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        data = diamondService.LoadData();
        diamondUI.UpdateDiamondUI(data.diamond);
    }

    public void OnButtonClick() // 테스트 용 버튼
    {
        GetDiamond(500);
    }

    public void CostDiamond(int cost)
    {
        data.diamond -= cost;
        diamondService.SaveData(data);
        diamondUI.UpdateDiamondUI(data.diamond);
    }

    public void GetDiamond(int amount)
    {
        data.diamond += amount;
        diamondService.SaveData(data);
        diamondUI.UpdateDiamondUI(data.diamond);
    }

    public bool HasEnoughDiamond(int cost)
    {
        if (data.diamond < cost)
        {
            diamondUI.ShowNotEnoughDiamond();
            return false;
        }

        return true;
    }
}
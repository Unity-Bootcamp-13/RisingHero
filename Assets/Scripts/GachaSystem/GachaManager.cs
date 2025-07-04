using UnityEngine;

public class GachaManager : MonoBehaviour
{
    [SerializeField] private GachaUI gachaUI;

    private ISaveService saveService;
    private Gacha gacha;

    private void Awake()
    {
        saveService = new JsonSaveService();
        gacha = new Gacha();

        gacha.Initialize(saveService);
        gachaUI.Initialize(saveService, gacha);
    }
}

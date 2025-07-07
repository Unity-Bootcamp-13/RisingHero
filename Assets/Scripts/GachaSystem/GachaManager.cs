using UnityEngine;

public class GachaManager : MonoBehaviour
{
    [SerializeField] private GachaUI gachaUI;
    [SerializeField] private QuestManager questManager;

    private ISaveService saveService;
    private Gacha gacha;

    private void Awake()
    {
        saveService = new JsonSaveService();
        gacha = new Gacha();

        gacha.Initialize(saveService, questManager);
        gachaUI.Initialize(saveService, gacha);
    }
}

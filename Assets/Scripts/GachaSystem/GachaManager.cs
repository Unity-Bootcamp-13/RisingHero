using UnityEngine;

public class GachaManager : MonoBehaviour
{
    [SerializeField] private GachaUI gachaUI;
    [SerializeField] private QuestManager questManager;
    [SerializeField] private WeaponSlotUI weaponSlotUI;
    [SerializeField] private WeaponStatus weaponStatus; // �߰�

    private ISaveService saveService;
    private Gacha gacha;

    private void Awake()
    {
        saveService = new JsonSaveService();
        gacha = new Gacha();

        gacha.Initialize(saveService, questManager);
        gachaUI.Initialize(saveService, gacha, weaponSlotUI);
        gachaUI.SetWeaponStatus(weaponStatus); // �߰�
        weaponSlotUI.Initialize(saveService);  // �ʱ� ���� ����
    }
}

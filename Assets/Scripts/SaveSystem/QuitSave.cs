using UnityEngine;

public class QuitSave : MonoBehaviour
{
    private ISaveService saveService;

    private void Awake()
    {
    }

    private void OnApplicationQuit()
    {
        saveService = new JsonSaveService();
        var cached = saveService.Load(); // 이미 최신 상태라면
        saveService.Save(cached);        // 그대로 저장만 하면 됨
        Debug.Log("[QuitSave] 캐시 저장 완료");
    }
}

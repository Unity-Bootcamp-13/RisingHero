using UnityEngine;
using UnityEngine.UI;

public class WeaponInfoButton : MonoBehaviour
{
    private SlideUI slideUI;

    private void Awake()
    {
        slideUI = FindFirstObjectByType<SlideUI>(); // 씬에 있는 SlideUI 찾기
    }

    public void OnClick()
    {
        if (slideUI != null)
        {
            slideUI.OnClickWeaponInfo(); // ← SlideUI의 왼쪽 무기 정보 패널 열기
        }
    }
}

using UnityEngine;
using UnityEngine.U2D;
/// <summary>
/// �츮�� ������� Asset�� Ÿ�ϼ��� �����־ Pixel Perfect Camera�� ����ؾ��ϴµ�
/// ���� �����ϱ� ���ؼ� ZoomLevel�� ����ϴ� �ڵ�
/// </summary>
public class PPCamera : MonoBehaviour
{
    public int[] zoomLevels = { 12, 18, 24 };
    private int currentZoomIndex = 1;

    private PixelPerfectCamera ppc;

    private void Awake()
    {
        ppc = Camera.main.GetComponent<PixelPerfectCamera>();
    }

    private void Update()
    {
        HandleZoom();
    }

    private void HandleZoom()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");

        if (scroll >= 0.1f && currentZoomIndex < zoomLevels.Length - 1)
        {
            currentZoomIndex++;
            ApplyZoom();
        }
        else if (scroll <= -0.1f && currentZoomIndex > 0)
        {
            currentZoomIndex--;
            ApplyZoom();
        }
    }

    private void ApplyZoom()
    {
        if (ppc != null)
        {
            ppc.assetsPPU = zoomLevels[currentZoomIndex];
        }
    }
}

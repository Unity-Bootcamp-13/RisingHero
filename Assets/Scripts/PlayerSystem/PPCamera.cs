using UnityEngine;
using UnityEngine.U2D;
/// <summary>
/// 우리가 사용중인 Asset의 타일셋이 깨져있어서 Pixel Perfect Camera를 사용해야하는데
/// 줌을 구현하기 위해서 ZoomLevel을 사용하는 코드
/// </summary>
public class PPCamera : MonoBehaviour
{
    public GameObject player;
    public int[] zoomLevels = { 12, 18, 24 };
    private int currentZoomIndex = 1;

    private PixelPerfectCamera ppc;

    private void Awake()
    {
        ppc = Camera.main.GetComponent<PixelPerfectCamera>();
    }

    private void Update()
    {
        FollowPlayer();
        HandleZoom();
    }

    private void FollowPlayer()
    {
        if (player == null) return;

        Vector3 playerPos = player.transform.position;
        transform.position = new Vector3(playerPos.x, playerPos.y, transform.position.z);
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

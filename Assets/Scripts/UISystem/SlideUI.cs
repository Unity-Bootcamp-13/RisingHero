using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// UI �����̵� ��/�ƿ� ����� �����ϴ� ������Ʈ�Դϴ�.
/// �¿� UI �����̵� �� �������̵� ���
/// </summary>
public class SlideUI : MonoBehaviour
{
    [SerializeField] private RectTransform RightWeaponPanel;
    [SerializeField] private RectTransform LeftWeaponPanel;
    [SerializeField] private Button closeWeaponButton;

    [SerializeField] private RectTransform RightSkillPanel;
    [SerializeField] private RectTransform LeftSkillPanel;
    [SerializeField] private Button closeSkillButton;

    [SerializeField] private RectTransform WarpPanel;
    [SerializeField] private Button closeWarpButton;

    [SerializeField] private float slideDuration = 0.1f; // �ε巴�� �̵��ϴ� �ð�

    private Vector2 RightWeaponHiddenPosition;
    private Vector2 RightWeaponShownPosition;
    private Vector2 LeftWeaponHiddenPosition;
    private Vector2 LeftWeaponShownPosition;

    private Vector2 RightSkillHiddenPosition;
    private Vector2 RightSkillShownPosition;
    private Vector2 LeftSkillHiddenPosition;
    private Vector2 LeftSkillShownPosition;

    private Vector2 WarpPanelShowPosition;
    private Vector2 WarpPanelHidePosition;

    private void Start()
    {
        // ȭ�� ������ �ۿ� ��ġ (��: +250�� ȭ�� ��)
        RightWeaponShownPosition = RightWeaponPanel.anchoredPosition;
        RightWeaponHiddenPosition = RightWeaponShownPosition + new Vector2(250f, 0f); // ���������� ����� ����
        // ȭ�� ���� �ۿ� ��ġ (��: -250�� ȭ�� ��)
        LeftWeaponShownPosition = LeftWeaponPanel.anchoredPosition;
        LeftWeaponHiddenPosition = LeftWeaponShownPosition + new Vector2(-250f, 0f); // �������� ����� ����
        // ó���� ������ ��ġ�� ��ġ
        RightWeaponPanel.anchoredPosition = RightWeaponHiddenPosition;
        LeftWeaponPanel.anchoredPosition = LeftWeaponHiddenPosition;

        // ȭ�� ������ �ۿ� ��ġ (��: +250�� ȭ�� ��)
        RightSkillShownPosition = RightSkillPanel.anchoredPosition;
        RightSkillHiddenPosition = RightSkillShownPosition + new Vector2(700f, 0f); // ���������� ����� ����
        // ȭ�� ���� �ۿ� ��ġ (��: -250�� ȭ�� ��)
        LeftSkillShownPosition = LeftSkillPanel.anchoredPosition;
        LeftSkillHiddenPosition = LeftSkillShownPosition + new Vector2(-700f, 0f); // �������� ����� ����
        // ó���� ������ ��ġ�� ��ġ
        RightSkillPanel.anchoredPosition = RightSkillHiddenPosition;
        LeftSkillPanel.anchoredPosition = LeftSkillHiddenPosition;

        if (WarpPanel != null)
        {
            // ȭ�� �ۿ� ��ġ (��: +250�� ȭ�� ��)
            WarpPanelShowPosition = WarpPanel.anchoredPosition;
            WarpPanelHidePosition = WarpPanelShowPosition + new Vector2(500f, 0f); // ���������� ����� ����
                                                                                   // ó���� ������ ��ġ�� ��ġ
            WarpPanel.anchoredPosition = WarpPanelHidePosition;

        }

    }

    public void OnClickInventory()
    {
        if (RightWeaponPanel.anchoredPosition != RightWeaponHiddenPosition)
        {
            return;
        }
        HideAllSkillPanel();
        HideWarpPanel();

        closeWeaponButton.gameObject.SetActive(true);
        ShowWeaponRightPanel();
    }

    public void OnClickWeaponInfo()
    {
        if (LeftWeaponPanel.anchoredPosition != LeftWeaponHiddenPosition)
        {
            return;
        }
        ShowWeaponLeftPanel();
    }

    public void OnClickWeaponClose()
    {
        if (LeftWeaponPanel.anchoredPosition == LeftWeaponShownPosition)
        {
            HideWeaponLeftPanel();
        }
        else
        {
            HideWeaponRightPanel();
            closeWeaponButton.gameObject.SetActive(false);
        }
    }

    public void OnClickSkillInventory()
    {
        if (RightSkillPanel.anchoredPosition != RightSkillHiddenPosition)
        {
            return;
        }
        HideAllWeaponPanel();
        HideWarpPanel();

        closeSkillButton.gameObject.SetActive(true);
        ShowSkillRightPanel();
    }

    public void OnClickSkillInfo()
    {
        if (LeftSkillPanel.anchoredPosition != LeftSkillHiddenPosition)
        {
            return;
        }
        ShowSkillLeftPanel();
    }

    public void OnClickSkillClose()
    {
        if (LeftSkillPanel.anchoredPosition == LeftSkillShownPosition)
        {
            HideSkillLeftPanel();
        }
        else
        {
            HideSkillRightPanel();
            closeSkillButton.gameObject.SetActive(false);
        }
    }

    public void OnClcikWarpPanel()
    {
        HideAllWeaponPanel();
        HideAllSkillPanel();
        ShowWarpPanel();
        closeWarpButton.gameObject.SetActive(true);
    }

    public void OnClickWarpClose()
    {
        HideWarpPanel();
        closeWarpButton.gameObject.SetActive(false);
    }



    private void ShowWeaponRightPanel()
    {
        StartCoroutine(Slide(RightWeaponPanel, RightWeaponPanel.anchoredPosition, RightWeaponShownPosition));
    }

    private void HideWeaponRightPanel()
    {
        StartCoroutine(Slide(RightWeaponPanel, RightWeaponPanel.anchoredPosition, RightWeaponHiddenPosition));
    }

    private void ShowWeaponLeftPanel()
    {
        StartCoroutine(Slide(LeftWeaponPanel, LeftWeaponPanel.anchoredPosition, LeftWeaponShownPosition));
    }

    private void HideWeaponLeftPanel()
    {
        StartCoroutine(Slide(LeftWeaponPanel, LeftWeaponPanel.anchoredPosition, LeftWeaponHiddenPosition));
    }

    private void HideAllWeaponPanel()
    {
        if (closeWeaponButton.gameObject.activeSelf == true)
        {
            if (LeftWeaponPanel.anchoredPosition == LeftWeaponShownPosition)
            {
                HideWeaponLeftPanel();
            }
            HideWeaponRightPanel();
            closeWeaponButton.gameObject.SetActive(false);
        }
    }


    private void ShowSkillRightPanel()
    {
        StartCoroutine(Slide(RightSkillPanel, RightSkillPanel.anchoredPosition, RightSkillShownPosition));
    }

    private void HideSkillRightPanel()
    {
        StartCoroutine(Slide(RightSkillPanel, RightSkillPanel.anchoredPosition, RightSkillHiddenPosition));
    }

    private void ShowSkillLeftPanel()
    {
        StartCoroutine(Slide(LeftSkillPanel, LeftSkillPanel.anchoredPosition, LeftSkillShownPosition));
    }

    private void HideSkillLeftPanel()
    {
        StartCoroutine(Slide(LeftSkillPanel, LeftSkillPanel.anchoredPosition, LeftSkillHiddenPosition));
    }

    private void HideAllSkillPanel()
    {
        if (closeSkillButton.gameObject.activeSelf == true)
        {
            if (LeftSkillPanel.anchoredPosition == LeftSkillShownPosition)
            {
                HideSkillLeftPanel();
            }
            HideSkillRightPanel();
            closeSkillButton.gameObject.SetActive(false);
        }
    }

    private void ShowWarpPanel()
    {
        StartCoroutine(Slide(WarpPanel, WarpPanel.transform.position, WarpPanelShowPosition));
    }

    private void HideWarpPanel()
    {
        StartCoroutine(Slide(WarpPanel, WarpPanel.transform.position, WarpPanelHidePosition));
    }

    private IEnumerator Slide(RectTransform target, Vector2 start, Vector2 end)
    {
        float elapsed = 0f;

        while (elapsed < slideDuration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / slideDuration;

            // �ε巴�� �̵�
            target.anchoredPosition = Vector2.Lerp(start, end, Mathf.SmoothStep(0, 1, t));

            yield return null;
        }

        target.anchoredPosition = end;
    }
}

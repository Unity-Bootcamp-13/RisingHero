using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UISlideIn : MonoBehaviour
{
    [SerializeField] private RectTransform RightWeaponPanel;
    [SerializeField] private RectTransform LeftWeaponPanel;
    [SerializeField] private Button closeWeaponButtons;

    [SerializeField] private RectTransform RightSkillPanel;
    [SerializeField] private RectTransform LeftSkillPanel;
    [SerializeField] private Button closeSkillButtons;

    [SerializeField] private float slideDuration = 0.1f; // �ε巴�� �̵��ϴ� �ð�

    private Vector2 RightWeaponHiddenPosition;
    private Vector2 RightWeaponShownPosition;
    private Vector2 LeftWeaponHiddenPosition;
    private Vector2 LeftWeaponShownPosition;

    private Vector2 RightSkillHiddenPosition;
    private Vector2 RightSkillShownPosition;
    private Vector2 LeftSkillHiddenPosition;
    private Vector2 LeftSkillShownPosition;

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

    }

    public void OnClickInventory()
    {
        if (RightWeaponPanel.anchoredPosition != RightWeaponHiddenPosition)
        {
            return;
        }
        HideAllSkillPanel();

        closeWeaponButtons.gameObject.SetActive(true);
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
            closeWeaponButtons.gameObject.SetActive(false);
        }
    }

    public void OnClickSkillInventory()
    {
        if (RightSkillPanel.anchoredPosition != RightSkillHiddenPosition)
        {
            return;
        }
        HideAllWeaponPanel();

        closeSkillButtons.gameObject.SetActive(true);
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
            closeSkillButtons.gameObject.SetActive(false);
        }
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
        if (closeWeaponButtons.gameObject.activeSelf == true)
        {
            if (LeftWeaponPanel.anchoredPosition == LeftWeaponShownPosition)
            {
                HideWeaponLeftPanel();
            }
            HideWeaponRightPanel();
            closeWeaponButtons.gameObject.SetActive(false);
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
        if (closeSkillButtons.gameObject.activeSelf == true)
        {
            if (LeftSkillPanel.anchoredPosition == LeftSkillShownPosition)
            {
                HideSkillLeftPanel();
            }
            HideSkillRightPanel();
            closeSkillButtons.gameObject.SetActive(false);
        }
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

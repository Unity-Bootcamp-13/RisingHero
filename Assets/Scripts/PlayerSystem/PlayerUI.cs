using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class PlayerUI : MonoBehaviour
{
    [Header("HP UI")]
    [SerializeField] private Image hpFillImage;
    [SerializeField] private TMP_Text currentHpText;

    [Header("MP UI")]
    [SerializeField] private Image mpFillImage;
    [SerializeField] private TMP_Text currentMpText;

    private CharacterHealth health;
    private PlayerMana mana;

    public void Initialize(CharacterHealth health, PlayerMana mana)
    {
        this.health = health;
        this.mana = mana;

        UpdateHpUI();
        UpdateMpUI();

        health.OnDamaged += _ => UpdateHpUI();
        health.OnDie += UpdateHpUI;

        // MP�� ȸ�� �̺�Ʈ�� �����Ƿ� �ֱ������� ����
        StartCoroutine(UpdateMpRoutine());
    }

    private void UpdateHpUI()
    {
        if (health == null) return;

        float fill = (float)health.CurrentHp / health.GetMaxHp();
        hpFillImage.fillAmount = fill;
        currentHpText.text = health.CurrentHp.ToString("N0");
    }

    private void UpdateMpUI()
    {
        if (mana == null) return;

        float fill = (float)mana.GetCurrentMana() / 100f; // 100���� ����ȭ. �ʿ� �� maxMana �޾Ƽ� ����
        mpFillImage.fillAmount = fill;
        currentMpText.text = mana.GetCurrentMana().ToString("N0");
    }

    private IEnumerator UpdateMpRoutine()
    {
        while (true)
        {
            UpdateMpUI();
            yield return new WaitForSeconds(0.2f); // ���� ����ؼ� �ֱ� ����
        }
    }
}

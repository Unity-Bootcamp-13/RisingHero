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

    private PlayerHealth health;
    private PlayerMana mana;

    public void Initialize(PlayerHealth health, PlayerMana mana)
    {
        this.health = health;
        this.mana = mana;

        UpdateHpUI();
        UpdateMpUI();

        health.OnDamaged += _ => UpdateHpUI();
        health.OnHealed += _ => UpdateHpUI();
        health.OnDie += UpdateHpUI;

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

        float fill = (float)mana.GetCurrentMana() / 100f; // 필요시 maxMana 기준으로 변경
        mpFillImage.fillAmount = fill;
        currentMpText.text = mana.GetCurrentMana().ToString("N0");
    }

    private IEnumerator UpdateMpRoutine()
    {
        while (true)
        {
            UpdateMpUI();
            yield return new WaitForSeconds(0.2f);
        }
    }
}

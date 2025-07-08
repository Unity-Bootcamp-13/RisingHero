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

        // MP는 회복 이벤트가 없으므로 주기적으로 갱신
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

        float fill = (float)mana.GetCurrentMana() / 100f; // 100기준 정규화. 필요 시 maxMana 받아서 적용
        mpFillImage.fillAmount = fill;
        currentMpText.text = mana.GetCurrentMana().ToString("N0");
    }

    private IEnumerator UpdateMpRoutine()
    {
        while (true)
        {
            UpdateMpUI();
            yield return new WaitForSeconds(0.2f); // 부하 고려해서 주기 조절
        }
    }
}

using UnityEngine;

public class BossHealthUI : MonoBehaviour
{
    [SerializeField] private UnityEngine.UI.Slider healthBar;

    public void UpdateHealthBar(float ratio)
    {
        if (healthBar != null)
            healthBar.value = ratio;
    }

    public void Show() => gameObject.SetActive(true);
    public void Hide() => gameObject.SetActive(false);
}

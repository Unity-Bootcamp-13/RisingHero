using TMPro;
using UnityEngine;

public class DamageTextEffect : MonoBehaviour
{
    public float moveUpSpeed = 1f;
    public float maxLifeTime = 0.5f;
    public float fadeOutSpeed = 2f;

    private float currentLifeTime;

    private TextMeshProUGUI text;
    private Color originalColor;

    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
        originalColor = text.color;
    }

    private void OnEnable()
    {
        currentLifeTime = maxLifeTime;
        if (text != null)
            text.color = originalColor;
    }

    void Update()
    {
        transform.position += Vector3.up * moveUpSpeed * Time.deltaTime;

        if (text != null)
        {
            Color c = text.color;
            c.a -= fadeOutSpeed * Time.deltaTime;
            text.color = c;
        }

        currentLifeTime -= Time.deltaTime;
        if (currentLifeTime <= 0f)
        {
            gameObject.SetActive(false);
        }
    }
}

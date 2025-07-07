using UnityEngine;

public class SkillEffectLifetime : MonoBehaviour
{
    private float lifetime;

    public void SetLifetime(float duration)
    {
        lifetime = duration;
        Invoke(nameof(DestroySelf), lifetime);
    }

    private void DestroySelf()
    {
        Destroy(gameObject);
    }
}

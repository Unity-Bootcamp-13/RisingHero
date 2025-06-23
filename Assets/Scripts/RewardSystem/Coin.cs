using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 3.0f;

    private int coinValue = 1;
    private Transform playerTransform;
    private bool isBeingPulled = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            gameObject.SetActive(false); // Layer·Î ¼öÁ¤
        }
    }

    public void PullTowardPlayer()
    {
        isBeingPulled = true;
    }

    public void SetValue(int value)
    {
        coinValue = value;
    }

    private void OnDisable()
    {
        isBeingPulled = false;
    }
}

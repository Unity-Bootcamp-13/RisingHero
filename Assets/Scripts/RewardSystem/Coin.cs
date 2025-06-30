using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 3.0f;

    private int coinValue = 1;
    private Transform playerTransform;
    private bool isBeingPulled = false;

    private void Update()
    {
        if (isBeingPulled && playerTransform != null)
        {
            Vector3 direction = (playerTransform.position - transform.position).normalized;
            transform.position += direction * moveSpeed * Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            CoinBuffer.Instance.AddBufferedCoin(coinValue);
            gameObject.SetActive(false);
        }
    }

    public void PullTowardPlayer(Transform player)
    {
        isBeingPulled = true;
        playerTransform = player;
    }

    public void SetValue(int value)
    {
        coinValue = value;
    }

    private void OnDisable()
    {
        isBeingPulled = false;
        playerTransform = null;
    }
}

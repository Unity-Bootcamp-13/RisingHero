using UnityEngine;

public class EnemyChase : MonoBehaviour
{
    public float moveSpeed = 3.0f;
    private Transform playerTransform;
    public static float GlobalSlowFactor = 1f;

    private void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerTransform = player.transform;
        }
    }

    private void Update()
    {
        if (playerTransform != null)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                playerTransform.position,
                moveSpeed * GlobalSlowFactor * Time.deltaTime
            );
        }
    }
}
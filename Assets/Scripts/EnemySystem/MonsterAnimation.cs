using UnityEngine;

/// <summary>
/// Controls enemy animation direction based on player position.
/// Used for directional sprite animation (e.g. scorpion facing player).
/// </summary>
public class EliteAnimeController : MonoBehaviour
{
    private Animator animator;
    private Transform playerTransform;

    private void Start()
    {
        // Cache animator and player transform
        animator = GetComponent<Animator>();
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerTransform = player.transform;
        }
        else
        {
            Debug.LogWarning("Player를 찾을 수 없습니다! Player 태그를 확인해주세요.");
        }
    }

    private void Update()
    {
        if (playerTransform == null)
            return;

        Vector2 direction = (playerTransform.position - transform.position).normalized;

        int dir = 0;

        // Determine direction based on dominant axis (X or Y)
        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            dir = direction.x > 0 ? 2 : 4; // Right : Left
        }
        else
        {
            dir = direction.y > 0 ? 1 : 3; // Up : Down
        }

        animator.SetInteger("Direction", dir);
    }
}

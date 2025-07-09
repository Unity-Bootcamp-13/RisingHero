using UnityEngine;

public class EliteAnimeController : MonoBehaviour
{
    private Animator animator;
    private Transform playerTransform;

    private void Start()
    {
        animator = GetComponent<Animator>();

        if (playerTransform == null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                playerTransform = player.transform;
            }
        }
    }

    private void Update()
    {
        if (playerTransform == null)
            return;

        Vector2 direction = (playerTransform.position - transform.position).normalized;

        int dir = 0;

        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            dir = direction.x > 0 ? 2 : 4;
        }
        else
        {
            dir = direction.y > 0 ? 1 : 3;
        }

        animator.SetInteger("Direction", dir);
    }
}

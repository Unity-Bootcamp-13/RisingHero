using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public VirtualJoystick joystick;

    private Animator animator;
    private Vector2 moveDirection;
    private Direction direction;
    private PlayerAttack attack;
    private PlayerStatus status;

    private enum Direction { None = 0, Up = 1, Left = 2, Down = 3, Right = 4 }

    private static class AnimatorParams
    {
        public const string Direction = "Direction";
    }

    public bool IsMoving => moveDirection != Vector2.zero;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        attack = GetComponent<PlayerAttack>();
        status = GetComponent<PlayerStatus>();

        animator.SetInteger(AnimatorParams.Direction, (int)Direction.None);
    }

    private void Update()
    {
        Vector2 input = joystick.Direction();

        if (!attack.IsAttacking)
        {
            animator.SetLayerWeight(0, 1f);
            animator.SetLayerWeight(1, 0f);
            HandleMovementInput();
            Move();
        }
        else if (input != Vector2.zero)
        {
            animator.SetInteger("Attack", 0);
            attack.ForceCancel();
        }
    }

    private void HandleMovementInput()
    {
        Vector2 input = joystick.Direction();
        moveDirection = input.normalized;

        if (input != Vector2.zero)
        {
            if (Mathf.Abs(input.x) > Mathf.Abs(input.y))
                direction = input.x > 0 ? Direction.Right : Direction.Left;
            else
                direction = input.y > 0 ? Direction.Up : Direction.Down;
        }
        else
        {
            direction = Direction.None;
        }

        animator.SetInteger(AnimatorParams.Direction, (int)direction);
    }

    private void Move()
    {
        transform.position += (Vector3)(moveDirection * status.moveSpeed * Time.deltaTime);
    }

    public void MoveTo(Vector3 targetPos)
    {
        moveDirection = (targetPos - transform.position).normalized;
    }

    public int GetDirectionCode()
    {
        return (int)direction;
    }
}

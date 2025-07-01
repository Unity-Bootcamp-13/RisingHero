/*using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 3f;
    public VirtualJoystick joystick; // 에디터에서 연결

    private Animator animator;
    private Vector2 moveDirection;
    private Direction direction;

    private enum Direction { None = 0, Up = 1, Left = 2, Down = 3, Right = 4 }

    private static class AnimatorParams
    {
        public const string Direction = "Direction";
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
        animator.SetInteger(AnimatorParams.Direction, (int)Direction.None);
    }

    private void Update()
    {
        HandleMovementInput();
        Move();
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
        transform.position += (Vector3)(moveDirection * moveSpeed * Time.deltaTime);
    }
}*/
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private static readonly int IsRunning = Animator.StringToHash("isRunning");
    private static readonly int IsClimbing = Animator.StringToHash("isClimbing");
    [SerializeField] private float runSpeed = 10f;
    [SerializeField] private float jumpSpeed = 5f;
    [SerializeField] private float climbSpeed = 5f;
    [SerializeField] private float gravityScaleAtStart = 8f;

    private Vector2 moveInput;
    private Animator myAnimator;
    private CapsuleCollider2D myCapsuleCollider;
    private Rigidbody2D myRigidbody;

    private void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myRigidbody.gravityScale = gravityScaleAtStart;
        myAnimator = GetComponent<Animator>();
        myCapsuleCollider = GetComponent<CapsuleCollider2D>();
    }

    private void Update()
    {
        Run();
        FlipSprite();
        ClimbLadder();
    }

    private void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    private void OnJump(InputValue value)
    {
        LayerMask ground = LayerMask.GetMask("Ground");
        bool isTouchingGround = myCapsuleCollider.IsTouchingLayers(ground);

        if (!isTouchingGround) return;

        if (value.isPressed)
            myRigidbody.velocity += new Vector2(0f, jumpSpeed);
    }

    private void Run()
    {
        float velocityY = myRigidbody.velocity.y;
        float velocityX = moveInput.x * runSpeed;

        var playerVelocity = new Vector2(velocityX, velocityY);
        myRigidbody.velocity = playerVelocity;

        // Set running animation
        bool doesPlayerHasHorizontalSpeed = Mathf.Abs(velocityX) > Mathf.Epsilon;
        myAnimator.SetBool(IsRunning, doesPlayerHasHorizontalSpeed);
    }

    private void FlipSprite()
    {
        float velocityX = myRigidbody.velocity.x;
        bool doesPlayerHasHorizontalSpeed = Mathf.Abs(velocityX) > Mathf.Epsilon;
        if (doesPlayerHasHorizontalSpeed)
        {
            var newVector2 = new Vector2(Mathf.Sign(velocityX), 1f);
            transform.localScale = newVector2;
        }
    }

    private void ClimbLadder()
    {
        LayerMask climbing = LayerMask.GetMask("Climbing");
        bool isTouchingClimbingArea = myCapsuleCollider.IsTouchingLayers(climbing);

        if (!isTouchingClimbingArea)
        {
            myAnimator.SetBool(IsClimbing, false);
            myRigidbody.gravityScale = gravityScaleAtStart;
            return;
        }

        float velocityY = moveInput.y * climbSpeed;
        float velocityX = myRigidbody.velocity.x;
        var climbVelocity = new Vector2(velocityX, velocityY);

        myRigidbody.velocity = climbVelocity;
        myRigidbody.gravityScale = 0f;

        // Set climbing animation
        bool doesPlayerHasVerticalSpeed = Mathf.Abs(velocityY) > Mathf.Epsilon;
        myAnimator.SetBool(IsClimbing, doesPlayerHasVerticalSpeed);
    }
}
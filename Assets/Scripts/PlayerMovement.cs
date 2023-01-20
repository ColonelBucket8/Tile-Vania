using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private static readonly int IsRunning = Animator.StringToHash("isRunning");
    private static readonly int IsClimbing = Animator.StringToHash("isClimbing");
    private static readonly int Dying = Animator.StringToHash("Dying");
    [SerializeField] private float runSpeed = 10f;
    [SerializeField] private float jumpSpeed = 5f;
    [SerializeField] private float climbSpeed = 5f;
    [SerializeField] private float gravityScaleAtStart = 8f;
    private bool isAlive = true;

    private Vector2 moveInput;
    private Animator myAnimator;
    private CapsuleCollider2D myBodyCollider;
    private BoxCollider2D myFeetCollider;
    private Rigidbody2D myRigidbody;

    private void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myRigidbody.gravityScale = gravityScaleAtStart;
        myAnimator = GetComponent<Animator>();
        myBodyCollider = GetComponent<CapsuleCollider2D>();
        myFeetCollider = GetComponent<BoxCollider2D>();
        GetComponent<PlayerInput>();
    }

    private void Update()
    {
        if (!isAlive) return;

        Run();
        FlipSprite();
        ClimbLadder();
        Die();
    }

    private void OnMove(InputValue value)
    {
        if (!isAlive) return;

        moveInput = value.Get<Vector2>();
    }

    private void OnJump(InputValue value)
    {
        if (!isAlive) return;

        LayerMask ground = LayerMask.GetMask("Ground");
        bool isTouchingGround = myFeetCollider.IsTouchingLayers(ground);

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
        bool isTouchingClimbingArea = myFeetCollider.IsTouchingLayers(climbing);

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

    private void Die()
    {
        LayerMask enemies = LayerMask.GetMask("Enemies", "Hazards");
        bool isTouchingEnemies = myBodyCollider.IsTouchingLayers(enemies);

        if (isTouchingEnemies)
        {
            isAlive = false;

            // Throw the player up in the air
            myRigidbody.AddRelativeForce(new Vector2(0f, 1000f));

            // Set dying animation
            myAnimator.SetTrigger(Dying);
        }
    }
}
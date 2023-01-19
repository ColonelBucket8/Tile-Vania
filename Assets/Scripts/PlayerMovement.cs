using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private static readonly int IsRunning = Animator.StringToHash("isRunning");
    [SerializeField] private float runSpeed = 10f;
    [SerializeField] private float jumpSpeed = 5f;

    private Vector2 moveInput;
    private Animator myAnimator;
    private Rigidbody2D myRigidbody;

    private void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
    }

    private void Update()
    {
        Run();
        FlipSprite();
    }

    private void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    private void OnJump(InputValue value)
    {
        if (value.isPressed) myRigidbody.velocity += new Vector2(0f, jumpSpeed);
    }

    private void Run()
    {
        var velocityY = myRigidbody.velocity.y;
        var velocityX = moveInput.x * runSpeed;

        var playerVelocity = new Vector2(velocityX, velocityY);
        myRigidbody.velocity = playerVelocity;

        var doesPlayerHasHorizontalSpeed = Mathf.Abs(velocityX) > Mathf.Epsilon;
        if (doesPlayerHasHorizontalSpeed) myAnimator.SetBool(IsRunning, true);
    }

    private void FlipSprite()
    {
        var velocityX = myRigidbody.velocity.x;
        var doesPlayerHasHorizontalSpeed = Mathf.Abs(velocityX) > Mathf.Epsilon;
        if (doesPlayerHasHorizontalSpeed) transform.localScale = new Vector2(Mathf.Sign(velocityX), 1f);
    }
}
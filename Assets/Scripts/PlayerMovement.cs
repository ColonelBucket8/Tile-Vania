using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float runSpeed = 10;

    private Vector2 moveInput;
    private Rigidbody2D myRigidbody;

    private void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
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

    private void Run()
    {
        var velocityY = myRigidbody.velocity.y;
        var velocityX = moveInput.x * runSpeed;

        var playerVelocity = new Vector2(velocityX, velocityY);
        myRigidbody.velocity = playerVelocity;
    }

    private void FlipSprite()
    {
        var velocityX = myRigidbody.velocity.x;
        var playerHasHorizontalSpeed = Mathf.Abs(velocityX) > Mathf.Epsilon;
        if (playerHasHorizontalSpeed) transform.localScale = new Vector2(Mathf.Sign(velocityX), 1f);
    }
}
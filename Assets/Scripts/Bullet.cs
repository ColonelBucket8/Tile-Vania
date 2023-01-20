using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float bulletSpeed = 9f;
    private Rigidbody2D myRigidbody;
    private PlayerMovement player;
    private float xSpeed;

    private void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<PlayerMovement>();
        xSpeed = player.transform.localScale.x * bulletSpeed;
    }

    private void Update()
    {
        myRigidbody.velocity = new Vector2(xSpeed, 0);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy")) Destroy(other.gameObject);

        // Destroy me
        Destroy(gameObject);
    }
}
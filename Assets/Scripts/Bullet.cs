using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float bulletSpeed = 9f;
    [SerializeField] private int bulletDamage;
    private Rigidbody2D myRigidbody;
    private PlayerMovement player;
    private float xSpeed;
    private AudioSession audioSession;

    private void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<PlayerMovement>();
        xSpeed = player.transform.localScale.x * bulletSpeed;
        audioSession = FindObjectOfType<AudioSession>();
        flipBullet();
    }


    private void Update()
    {
        myRigidbody.velocity = new Vector2(xSpeed, 0);
    }
    private void flipBullet()
    {
        // If facing right
        if (player.transform.localScale.x > 0)
        {
            transform.localScale = new Vector2(transform.localScale.x, transform.localScale.y);
        }
        // If facing left
        else
        {
            transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            HandleEnemyHealth(other);
            audioSession.PlayBulletHitEnemyClip();
        }

        // Destroy me
        Destroy(gameObject);
    }

    private void HandleEnemyHealth(Collider2D other)
    {
        var enemyHealth = other.gameObject.GetComponent<Health>();

        if (enemyHealth == null) return;

        enemyHealth.ReduceHealth(bulletDamage);

        if (enemyHealth.GetHealth() <= 0)
        {
            enemyHealth.PlayDieParticle();
            Destroy(other.gameObject);
        }
    }
}
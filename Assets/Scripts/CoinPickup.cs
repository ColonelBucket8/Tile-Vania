using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    [SerializeField] private AudioClip coinPickupSFX;
    [SerializeField] private float coinVolume = 0.3f;
    [SerializeField] private int pointsForCoinPickup = 100;

    private bool wasCollected;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player") && !wasCollected)
        {
            wasCollected = true;
            AudioSource.PlayClipAtPoint(coinPickupSFX, Camera.main.transform.position, coinVolume);
            FindObjectOfType<GameSession>().IncreaseScore(pointsForCoinPickup);
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }
}
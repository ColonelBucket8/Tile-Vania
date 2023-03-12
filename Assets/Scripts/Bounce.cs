using UnityEngine;

public class Bounce : MonoBehaviour
{
    private AudioSession audioSession;

    private void Start()
    {
        audioSession = FindObjectOfType<AudioSession>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            audioSession.PlayBouncingClip();
        }
    }
}

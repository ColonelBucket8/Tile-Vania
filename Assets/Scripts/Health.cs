using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int health;
    [SerializeField] private ParticleSystem dieParticle;

    public int GetHealth()
    {
        return health;
    }

    public void ReduceHealth(int value)
    {
        health -= value;
    }

    // TODO: Something wrong with displaying particle effect as it doesn't show on screen
    public void PlayDieParticle()
    {
        if (dieParticle != null)
        {
            Instantiate(dieParticle, transform.position, Quaternion.identity);
            dieParticle.Play();
        }
    }
}

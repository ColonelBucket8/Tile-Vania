using UnityEngine;

public class AudioSession : MonoBehaviour
{
    private static AudioSession instance;

    [Header("Music")]
    [SerializeField] private AudioClip musicClip;
    [SerializeField][Range(0f, 1f)] private float musicVolume = 1f;

    [Header("SFX")]
    [SerializeField] private AudioClip bouncingClip;
    [SerializeField][Range(0f, 1f)] private float bouncingVolume = 1f;


    private void Awake()
    {
        ManageSingleton();
    }

    private void ManageSingleton()
    {
        if (instance != null)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void PlayBouncingClip()
    {
        PlayClip(bouncingClip, bouncingVolume);
    }

    private void PlayClip(AudioClip clip, float volume)
    {
        Vector3 cameraPos = Camera.main.transform.position;
        AudioSource.PlayClipAtPoint(clip, cameraPos, volume);
    }

}

using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSession : MonoBehaviour
{
    [SerializeField] private int playersLives = 3;
    [SerializeField] private TextMeshProUGUI livesText;
    [SerializeField] private TextMeshProUGUI scoresText;
    private int scores;

    private void Awake()
    {
        int numGameSessions = FindObjectsOfType<GameSession>().Length;

        if (numGameSessions > 1)
            Destroy(gameObject);
        else
            DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        livesText.text = playersLives.ToString();
        scoresText.text = scores.ToString();
    }


    public void ProcessPlayerDeath()
    {
        if (playersLives > 1)
            TakeLife();
        else
            ResetGameSession();
    }

    /// <summary>
    /// Reset the player position to the start of the current scene level
    /// if the player still has life 
    /// Reduce player's life by one
    /// </summary>
    private void TakeLife()
    {
        playersLives--;
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
        livesText.text = playersLives.ToString();
    }

    private void ResetGameSession()
    {
        FindObjectOfType<ScenePersist>().ResetScenePersist();
        SceneManager.LoadScene(0);
        Destroy(gameObject);
    }

    public void IncreaseScore(int pointsToAdd)
    {
        scores += pointsToAdd;
        scoresText.text = scores.ToString();
    }
}
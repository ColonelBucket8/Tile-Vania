using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{
    [SerializeField] private float levelLoadDelay = 1f;


    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player")) StartCoroutine(LoadNextLevel());
    }

    private IEnumerator LoadNextLevel()
    {
        yield return new WaitForSecondsRealtime(levelLoadDelay);
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings) nextSceneIndex = 0;

        SceneManager.LoadScene(nextSceneIndex);
    }
}
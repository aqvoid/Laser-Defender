using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }

    [Header("=== General ===")]
    [SerializeField] private float sceneLoadDelayInSeconds;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void OnEnable()
    {
        Health.OnPlayerDeath += LoadGameOver;
    }

    private void OnDisable()
    {
        Health.OnPlayerDeath -= LoadGameOver;
    }

    public void LoadMainMenu() => SceneManager.LoadScene("Main Menu");
    public void LoadGame() => SceneManager.LoadScene("Game");
    public void LoadGameOver() => StartCoroutine(SceneLoadDelay("Game Over", sceneLoadDelayInSeconds));

    private IEnumerator SceneLoadDelay(string sceneName, float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(sceneName);
    }
}

using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }

    [Header("=== General ===")]
    [SerializeField] private float toMainMenuDelay;
    [SerializeField] private float toGameDelay;
    [SerializeField] private float toGameOverDelay;

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

    public void LoadMainMenu() => StartCoroutine(TransitionToScene("Main Menu", toMainMenuDelay));

    public void LoadGame() => StartCoroutine(TransitionToScene("Game", toGameDelay));

    public void LoadGameOver() => StartCoroutine(TransitionToScene("Game Over", toGameOverDelay));

    private IEnumerator TransitionToScene(string sceneName, float delay)
    {
        AudioManager currentAudio = FindFirstObjectByType<AudioManager>();
        yield return StartCoroutine(currentAudio.FadeOutMusic(delay));

        SceneManager.LoadScene(sceneName);
        yield return null;

        AudioManager newAudio = FindFirstObjectByType<AudioManager>();
        yield return StartCoroutine(newAudio.FadeInMusic(delay));
    }
}

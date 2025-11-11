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

    private void OnEnable() => Health.OnPlayerDeath += LoadGameOver;

    private void OnDisable() => Health.OnPlayerDeath -= LoadGameOver;

    private IEnumerator TransitionToScene(string sceneName, float delay)
    {
        yield return StartCoroutine(FadeOut(delay));

        SceneManager.LoadScene(sceneName);
        yield return null;

        yield return StartCoroutine(FadeIn(delay));
    }

    private IEnumerator FadeOut(float delay)
    {
        MusicManager currentAudio = FindFirstObjectByType<MusicManager>();
        yield return StartCoroutine(ScreenFader.Instance.FadeOut(delay));
        yield return StartCoroutine(currentAudio.FadeOutMusic(delay * 1.1f));
    }

    private IEnumerator FadeIn(float delay)
    {
        MusicManager newAudio = FindFirstObjectByType<MusicManager>();
        StartCoroutine(ScreenFader.Instance.FadeIn(delay));
        yield return StartCoroutine(newAudio.FadeInMusic(delay));
    }

    public void LoadMainMenu() => StartCoroutine(TransitionToScene("Main Menu", toMainMenuDelay));
    public void LoadGame() => StartCoroutine(TransitionToScene("Game", toGameDelay));
    public void LoadGameOver() => StartCoroutine(TransitionToScene("Game Over", toGameOverDelay));
}

using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{
    public static ScoreKeeper Instance { get; private set; }

    [SerializeField] private int[] scoresFromEnemies;

    private int scorePoints;

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
        Health.OnEnemyDeath += OnEnemyDeath;
        Health.OnPlayerDeath += ResetScore;
    }

    private void OnDisable()
    {
        Health.OnEnemyDeath -= OnEnemyDeath;
        Health.OnPlayerDeath -= ResetScore;
    }

    private void OnEnemyDeath(Health enemy)
    {
        int value = GetScoreFromEnemy(enemy);
        scorePoints += value;

        Mathf.Clamp(scorePoints, 0, int.MaxValue);
    }

    private int GetScoreFromEnemy(Health enemy)
    {
        return scoresFromEnemies[0];
    }

    public int GetScore() => scorePoints;
    public void ResetScore() => scorePoints = 0;
}

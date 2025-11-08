using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }

    [SerializeField] private EnemyScoreSO enemyScoreConfig;

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
        Health.OnEnemyDeath += AddScoreByEnemy;
    }

    private void OnDisable()
    {
        Health.OnEnemyDeath -= AddScoreByEnemy;
    }

    private void AddScoreByEnemy(Health enemy)
    {
        int value = enemyScoreConfig.GetScoreByEnemy(enemy.gameObject);
        scorePoints += value;

        Mathf.Clamp(scorePoints, 0, int.MaxValue);
    }

    public int GetScore() => scorePoints;
    public void ResetScore() => scorePoints = 0;
}

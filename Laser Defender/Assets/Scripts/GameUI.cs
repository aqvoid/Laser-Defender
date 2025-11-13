using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    [Header("=== UI Elements ===")]
    [SerializeField] private Slider healthBar;
    [SerializeField] private TextMeshProUGUI scoreText;

    private Health playerHealth;

    private void Awake() => playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();

    private void Start()
    {
        ScoreManager.Instance.ResetScore();
        UpdateHealthBar(playerHealth, 0);
        UpdateScoreText(null);
    }

    private void OnEnable()
    {
        playerHealth.OnEntityDamaged += UpdateHealthBar;
        Health.OnPlayerDeath += ResetHealthBar;
        Health.OnEnemyDeath += UpdateScoreText;
    }

    private void OnDisable()
    {
        playerHealth.OnEntityDamaged -= UpdateHealthBar;
        Health.OnPlayerDeath -= ResetHealthBar;
        Health.OnEnemyDeath -= UpdateScoreText;
    }

    private void UpdateHealthBar(Health health, int damage) => healthBar.value = (float)playerHealth.GetHealth() / playerHealth.GetMaxHealth();

    private void ResetHealthBar() => healthBar.value = 0f;

    private void UpdateScoreText(Health enemy) => scoreText.text = $"Score: {ScoreManager.Instance.GetScore()}";
}

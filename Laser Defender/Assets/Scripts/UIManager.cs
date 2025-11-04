using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("=== UI Elements ===")]
    [SerializeField] private Slider healthBar;
    [SerializeField] private TextMeshProUGUI scoreText;

    [Header("=== References ===")]
    [SerializeField] private Health playerHealth;
    [SerializeField] private ScoreKeeper scoreKeeper;

    private void Start()
    {
        UpdateHealthBar();
        UpdateScoreText(null);
    }

    private void OnEnable()
    {
        playerHealth.OnPlayerDamaged += UpdateHealthBar;
        Health.OnPlayerDeath += ResetHealthBar;
        Health.OnEnemyDeath += UpdateScoreText;
    }

    private void OnDisable()
    {
        playerHealth.OnPlayerDamaged -= UpdateHealthBar;
        Health.OnPlayerDeath -= ResetHealthBar;
        Health.OnEnemyDeath -= UpdateScoreText;
    }

    private void UpdateHealthBar()
    {
        healthBar.value = (float)playerHealth.GetHealth() / playerHealth.GetMaxHealth();
    }

    private void ResetHealthBar()
    {
        healthBar.value = 0f;
    }

    private void UpdateScoreText(Health enemy)
    {
        scoreText.text = $"Score: {scoreKeeper.GetScore()}";
    }
}

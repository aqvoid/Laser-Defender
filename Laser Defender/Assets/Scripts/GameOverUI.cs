using TMPro;
using UnityEngine;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreResultText;

    private void Start() => scoreResultText.text = $"You Scored:\n{ScoreManager.Instance.GetScore()}";

    public void RestartButton() => LevelManager.Instance.LoadGame();
    public void MainMenuButton() => LevelManager.Instance.LoadMainMenu();
}
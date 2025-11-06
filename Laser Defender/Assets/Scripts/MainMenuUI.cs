using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private Button startButton;

    private void Start()
    {
        startButton.onClick.AddListener(StartGame);
    }

    public void StartGame()
    {
        ScoreKeeper.Instance.ResetScore();
        LevelManager.Instance.LoadGame();
    }

    public void QuitGame() => Application.Quit();
}

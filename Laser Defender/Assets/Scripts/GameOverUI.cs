using TMPro;
using UnityEngine;

public class GameOverUI : MonoBehaviour
{
    private TextMeshProUGUI scoreResultText;

    private void Awake()
    {
        scoreResultText = GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        scoreResultText.text = $"You Scored:\n{ScoreKeeper.Instance.GetScore()}";
    }

}
using TMPro;
using UnityEngine;

public class ScoreText : MonoBehaviour
{
    [SerializeField] private TMP_Text _currentScoreText;
    [SerializeField] private Score _currentScore;

    private void OnEnable()
    {
        _currentScore.OnScoreChanged += UpdateCurrentScoreText;
    }

    private void UpdateCurrentScoreText() 
    {
        _currentScoreText.text = "Ñ÷¸ò: " + _currentScore.CurrentScore.ToString();
    }

    private void OnDisable()
    {
        _currentScore.OnScoreChanged -= UpdateCurrentScoreText;
    }
}

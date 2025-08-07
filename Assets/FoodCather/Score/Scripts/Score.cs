using System;
using UnityEngine;

public class Score : MonoBehaviour
{
    [SerializeField] private int _currentScore;

    public event Action OnScoreChanged;

    public int CurrentScore
    {
        get { return _currentScore; }
        set
        {
            if (value <= 0)
                throw new ArgumentOutOfRangeException("value");

            _currentScore = value;
        }
    }

    public void AddScore(int score)
    {
        if (score < 0)
            throw new ArgumentOutOfRangeException(nameof(score));

        _currentScore += score;
        OnScoreChanged?.Invoke();
    }
}

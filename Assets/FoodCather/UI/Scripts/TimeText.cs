using TMPro;
using UnityEngine;

public class TimeText : MonoBehaviour
{
    [SerializeField] private TMP_Text _currentScoreText;
    [SerializeField] private Timer _timer;

    private void OnEnable()
    {
        _timer.OnUpdateTimer += UpdateCurrentScoreText;
    }

    private void UpdateCurrentScoreText()
    {
        _currentScoreText.text = "Время: " + _timer._currentTime.ToString("0");
    }

    private void OnDisable()
    {
        _timer.OnUpdateTimer -= UpdateCurrentScoreText;
    }
}

using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class GameEndPanel : MonoBehaviour
{
    [SerializeField] private TMP_Text _currentScoreText;
    [SerializeField] private GameObject _panel;

    private Timer _timer;
    private Score _score;
    private Lose _lose;

    [Inject]
    private void Constructor(Score score, Timer timer, Lose lose)
    {
        _score = score;
        _timer = timer;
        _lose = lose;
    }

    private void OnEnable()
    {
        _timer.OnTimeUp += OpenGameEndPanel;
        _lose.OnPlayerLose += OpenGameEndPanel;
    }

    private void OpenGameEndPanel()
    {
        _panel.SetActive(true);
        _currentScoreText.text = _score.CurrentScore.ToString();
    }

    public void RestartButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void BackMainMenuButton()
    {
        SceneManager.LoadScene("MainMenu");
    }

    private void OnDisable()
    {
        _timer.OnTimeUp -= OpenGameEndPanel;
        _lose.OnPlayerLose -= OpenGameEndPanel;
    }
}

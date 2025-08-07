using System;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] private float _startTime = 5;

    public float _currentTime { get; private set; }

    public event Action OnUpdateTimer;
    public event Action OnTimeUp;

    private void Start()
    {
        _currentTime = _startTime;
    }

    private void Update()
    {
        _currentTime -= Time.deltaTime;
        OnUpdateTimer?.Invoke();

        if (_currentTime <= 0f)
        {
            OnTimeUp?.Invoke();
            _currentTime = 0f;
        }
    }
}

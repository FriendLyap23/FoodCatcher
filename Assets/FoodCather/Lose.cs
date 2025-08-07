using System;
using UnityEngine;

public class Lose : MonoBehaviour
{
    [SerializeField] private int _maxNumberMistakes;

    [SerializeField] private int _currentNumberMistakes;

    public event Action OnPlayerLose;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Food"))
        {
            _currentNumberMistakes++;

            if (_currentNumberMistakes == _maxNumberMistakes)
                OnPlayerLose?.Invoke();
        }
    }
}

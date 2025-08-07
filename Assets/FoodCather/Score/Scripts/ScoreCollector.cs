using UnityEngine;
using Zenject;

public class ScoreCollector : MonoBehaviour
{
    private Score _score;

    [Inject]
    private void Constructor(Score score) 
    {
        _score = score;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Food"))
        {
            if (collision.TryGetComponent(out Food food))
            {
                _score.AddScore(food._addedScore);
            }
        }
    }
}

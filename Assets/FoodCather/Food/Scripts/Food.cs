using UnityEngine;

public class Food : MonoBehaviour
{
    [SerializeField] public int _addedScore;

    private PoolMono _pool;

    public void SetPool(PoolMono pool)
    {
        _pool = pool;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("MainCharacter") || 
            collision.gameObject.layer == LayerMask.NameToLayer("DestroyZone"))
            _pool.ReleaseElement(gameObject);
    }
}

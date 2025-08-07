using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class SpawnFood : MonoBehaviour
{
    private const float _MINDELAYBETWEENSPAWN = 0.2f;
    [SerializeField] private float _maxDelayBetweenSpawn;

    [SerializeField] private GameObject[] _foods = new GameObject[0];

    [SerializeField] private int _poolSizePerFood = 5;
    [SerializeField] private bool _autoExpandPool = true;

    private List<PoolMono> _foodPools = new List<PoolMono>();
    private Coroutine _spawnCoroutine;
    private Timer _timer;

    [Inject]
    private void Constructor(Timer timer) 
    {
        _timer = timer;
    }

    private void OnEnable()
    {
        _timer.OnTimeUp += StopSpawnFood;
    }

    private void Start()
    {
        foreach (var prefab in _foods)
        {
            _foodPools.Add(new PoolMono(prefab, _poolSizePerFood, transform));
        }

        _spawnCoroutine = StartCoroutine(SpawnFoodCorutine());
    }

    private IEnumerator SpawnFoodCorutine() 
    {
        while (true) 
        {
            float delayBetweenSpawn = Random.Range(_MINDELAYBETWEENSPAWN, _maxDelayBetweenSpawn); 
            yield return new WaitForSeconds(delayBetweenSpawn);

            var randomPool = _foodPools[Random.Range(0, _foodPools.Count)];
            
            Vector2 spawnPosition = GetRandomSpawnPosition();
            var food = randomPool.GetFreeElement();

            food.transform.position = spawnPosition;
            food.transform.rotation = Quaternion.identity;

            if (food.TryGetComponent<Food>(out var foodComponent))
                foodComponent.SetPool(randomPool);
        }
    }

    private Vector2 GetRandomSpawnPosition() 
    {
        float screenAspect = (float)Screen.width / Screen.height;
        float cameraHeight = Camera.main.orthographicSize;
        float cameraWidth = cameraHeight * screenAspect;

        float spawnY = Camera.main.transform.position.y + cameraHeight * 1.1f;
        float spawnX = Random.Range(-cameraWidth, cameraWidth);

        return new Vector2(spawnX, spawnY);
    }

    private void StopSpawnFood()
    {
        if (_spawnCoroutine != null)
            StopCoroutine(_spawnCoroutine);

        foreach (var pool in _foodPools)
        {
            var activeFoods = pool.GetActiveElementsInPool();

            foreach (var food in activeFoods)
                pool.ReleaseElement(food);
        }
    }

    private void OnDisable()
    {
        _timer.OnTimeUp -= StopSpawnFood;
    }
}

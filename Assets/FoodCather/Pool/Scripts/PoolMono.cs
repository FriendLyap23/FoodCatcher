using System.Collections.Generic;
using UnityEngine;

public class PoolMono
{
    private List<GameObject> _pool = new List<GameObject>();

    public GameObject Prefab { get; }
    public Transform Container { get; }

    public PoolMono(GameObject prefab, int count, Transform container)
    {
        Prefab = prefab;
        Container = container;

        CreatePool(count);
    }

    private void CreatePool(int count)
    {
        _pool = new List<GameObject>();

        for (int i = 0; i < count; i++)
            CreateObject();
    }

    private GameObject CreateObject(bool isActiveByDefault = false)
    {
        var createdObject = Object.Instantiate(Prefab, Container);
        createdObject.SetActive(isActiveByDefault);
        _pool.Add(createdObject);

        return createdObject;
    }

    public List<GameObject> GetActiveElementsInPool() 
    {
        List<GameObject> activeElements = new List<GameObject>();

        foreach (var elevemt in _pool) 
        {
            if (elevemt.activeInHierarchy)
                activeElements.Add(elevemt);
        }

        return activeElements;
    }

    public void ReleaseElement(GameObject element) 
    {
        if (element == null)
        {
            Debug.LogWarning("Trying to release a null object into the pool.");
            return;
        }

        if (!_pool.Contains(element))
        {
            Debug.LogWarning("This object does not belong to the pool.");
            return;
        }

        element.SetActive(false);
    }

    public GameObject GetFreeElement()
    {
        foreach (var obj in _pool)
        {
            if (!obj.activeInHierarchy)
            {
                obj.SetActive(true);
                return obj;
            }
        }
        return CreateObject(true);
    }
}
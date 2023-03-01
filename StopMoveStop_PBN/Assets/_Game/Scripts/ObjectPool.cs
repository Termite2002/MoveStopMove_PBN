using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : Singleton<ObjectPool>
{
    public GameObject poolObjectPrefab;
    public int poolCount;

    public bool canGrow;

    Queue<GameObject> poolObjects = new Queue<GameObject>();


    void Start()
    {
        for(int i = 0; i < poolCount; i++)
        {
            GameObject obj = Instantiate(poolObjectPrefab);
            obj.SetActive(false);

            poolObjects.Enqueue(obj);
        }
    }

    public GameObject GetFromPool()
    {
        if (poolObjects.Count > 0)
        {
            return poolObjects.Dequeue();
        }
        else if (canGrow)
        {
            GameObject obj = Instantiate(poolObjectPrefab);

            return obj;
        }
        else
        {
            return null;
        }
    }
    public void ReturnToPool(GameObject obj)
    {
        poolObjects.Enqueue(obj);
    }
}

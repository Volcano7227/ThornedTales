using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    private List<GameObject> pool = new List<GameObject>();

    [SerializeField] GameObject[] objectsToPool;
    [SerializeField] int[] quantityPerObject;
    public ObjectPool objectPoolInstance;

    /// <summary>
    /// Create the object pool instance
    /// </summary>
    public void Awake()
    {
        if (objectPoolInstance == null)
            objectPoolInstance = this;
    }

    /// <summary>
    /// Instantiate the object pool
    /// </summary>
    void Start()
    {
        for (int i = 0; i < Mathf.Min(objectsToPool.Length, quantityPerObject.Length); i++)
            for (int j = 0; j < quantityPerObject[i]; j++)
            {
                GameObject obj = Instantiate(objectsToPool[i]);
                obj.name = objectsToPool[i].name;
                obj.SetActive(false);
                pool.Add(obj);
            }
    }

    /// <summary>
    /// Get a pooled GameObject of the requested type
    /// </summary>
    /// <param name="typeObject"></param>
    /// <returns></returns>
    public GameObject GetPooledObject(GameObject typeObject)
    {
        for (int i = 0; i < pool.Count; i++)
            if (pool[i].name == typeObject.name && !pool[i].activeInHierarchy)
                return pool[i];
        return null;
    }
}
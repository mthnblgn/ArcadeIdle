using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;


public abstract class PoolUser<T> : MonoBehaviour where T : PoolObject<T>
{
    protected IObjectPool<T> _objectPool;
    [SerializeField] T _pooledObjectPrefab;
    protected T CreateIngredient()
    {
        T pooledObjectInstance = Instantiate(_pooledObjectPrefab);
        pooledObjectInstance.ObjectPool = _objectPool;
        return pooledObjectInstance;
    }

    // invoked when returning an item to the object pool
    protected void OnReleaseToPool(T pooledObject)
    {
        pooledObject.GetComponent<GameObject>().SetActive(false);
    }

    // invoked when retrieving the next item from the object pool
    protected void OnGetFromPool(T pooledObject)
    {
        pooledObject.GetComponent<GameObject>().SetActive(true);
    }

    // invoked when we exceed the maximum number of pooled items (i.e. destroy the pooled object)
    protected void OnDestroyPooledObject(T pooledObject)
    {
        Destroy(pooledObject.GetComponent<GameObject>());
    }
}
// invoked when creating an item to populate the object pool


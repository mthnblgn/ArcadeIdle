using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class PoolObject<T> : Object where T : Object
{
    private IObjectPool<T> objectPool;
    public IObjectPool<T> ObjectPool { set => objectPool = value; }

    public void Release(T objToRelease)
    {
        objectPool.Release(objToRelease);
    }
}

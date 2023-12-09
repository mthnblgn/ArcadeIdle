using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PooledObject : MonoBehaviour
{
    private ObjectPool _pool;
    public ObjectPool Pool { get => _pool; set => _pool = value; }

    private GameObject _gameObject;

    private void Awake()
    {
        _gameObject = transform.gameObject;
    }
    public void Release(int waitSeconds)
    {
        StartCoroutine(ReleaseCoroutine(waitSeconds));
    }
    IEnumerator ReleaseCoroutine(int waitSeconds)
    {
        yield return  new WaitForSeconds(waitSeconds);
        _pool.ReturnToPool(this);
    }
}

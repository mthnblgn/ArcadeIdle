using DesignPatterns.ObjectPool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.UI;

public class Source : MonoBehaviour
{
    Camera _mainCam;

    [SerializeField] Image _counter;
    [SerializeField] float _paddingUp;
    [SerializeField] SourcesScriptable _sourceData;
    [SerializeField] private bool _collectionCheck = true;
    [SerializeField] private int _defaultCapacity = 10;
    [SerializeField] private int _maxSize = 500;
    [SerializeField] Ingredient _ingredientPrefab;
    [SerializeField] Renderer _tokenRenderer;

    float _countdownTime;
    float _time;
    bool _isReady;
    Ingredient _currentIngredient;
    IObjectPool<Ingredient> _objectPool;

    private void Awake()
    {
        _objectPool = new ObjectPool<Ingredient>(CreateIngredient,
                OnGetFromPool, OnReleaseToPool, OnDestroyPooledObject,
                _collectionCheck, _defaultCapacity, _maxSize);
        _mainCam = Camera.main;
    }
    private void Start()
    {
        _countdownTime = _sourceData._waitingTime;
        _tokenRenderer.material = _sourceData._ingredientMat;
    }
    void FixedUpdate()
    {
        _counter.rectTransform.LookAt(_mainCam.transform.position);
        if (_countdownTime > _time)
        {
            _time += Time.deltaTime;
            _counter.fillAmount = _time / _countdownTime;
            _counter.color = LerpColor(Color.red, Color.yellow, Color.green, _counter.fillAmount);
            if (_countdownTime <= _time)
            {
                _currentIngredient = _objectPool.Get();
                _currentIngredient.ID = _sourceData.ID;
                _currentIngredient.ChangeMaterial(_sourceData._ingredientMat);
                _currentIngredient.transform.position = transform.position + Vector3.up * _paddingUp;
                _isReady = true;
            }
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (_isReady && other.TryGetComponent(out Actor actor) && actor.NextEmptyPoint() != null)
        {
            _time = 0;
            _counter.fillAmount = 0;
            actor.TakeIngredient(_currentIngredient);
            _currentIngredient = null;
            _isReady = false;
        }
    }
    // invoked when creating an item to populate the object pool
    private Ingredient CreateIngredient()
    {
        Ingredient ingredientInstance = Instantiate(_ingredientPrefab);
        ingredientInstance.ObjectPool = _objectPool;
        return ingredientInstance;
    }

    // invoked when returning an item to the object pool
    private void OnReleaseToPool(Ingredient pooledObject)
    {
        pooledObject.gameObject.SetActive(false);
    }

    // invoked when retrieving the next item from the object pool
    private void OnGetFromPool(Ingredient pooledObject)
    {
        pooledObject.gameObject.SetActive(true);
    }

    // invoked when we exceed the maximum number of pooled items (i.e. destroy the pooled object)
    private void OnDestroyPooledObject(Ingredient pooledObject)
    {
        Destroy(pooledObject.gameObject);
    }
    public void SetSource(SourcesScriptable sourceScriptable)
    {
        _sourceData = sourceScriptable;
    }
    Color LerpColor(Color a, Color b, Color c, float value)
    {
        Color color;
        color = (value < 0.5) ?
            a * (1 - value * 2) + b * (value * 2) :
            b * (2 - value * 2) + c * (value * 2 - 1);
        return color;
    }
    Color LerpColor(Color a, Color b, float value)
    {
        Color color;
        color = a * (1 - value) + b * (value);
        return color;
    }

}

using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class Source : ObjectPool
{
    Camera _mainCam;

    [SerializeField] Image _counter;
    [SerializeField] float _paddingUp;
    [SerializeField] SourcesScriptable _sourceData;
    [SerializeField] Renderer _tokenRenderer;

    float _countdownTime;
    float _time;
    bool _isReady;

    PooledObject _pooledObject;
    Ingredient _currentIngredient;

    private void OnEnable()
    {
        transform.DOScale(Vector3.one, 1f).SetEase(Ease.InBounce);
    }
    private void Awake()
    {
        _mainCam = Camera.main;
    }
    private void Start()
    {
        SetupPool();
        _countdownTime = _sourceData._waitingTime;
        _tokenRenderer.material = _sourceData.ingredientData._ingredientMat;
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
                _currentIngredient= GetPooledObject().GetComponent<Ingredient>();
                _currentIngredient.ID = _sourceData.ingredientData.ID;
                _currentIngredient.sellValue = _sourceData.ingredientData._ingredientSellValue;
                _currentIngredient.ChangeMaterial(_sourceData.ingredientData._ingredientMat);
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

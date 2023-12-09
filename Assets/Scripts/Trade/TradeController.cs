using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TradeController : ObjectPool
{
    [SerializeField] private List<BuyingSpot> _buyingSpots;
    [SerializeField] private List<SellingSpot> _sellingSpots;
     private float _counter;
    [SerializeField] private Vector3 _spawnPoint;

    private Customer _lastCustomer;
    SellingSpot _sellingSpot;
    private void Start()
    {
        _counter = GameController._currentLevel.CustomerTime;
        SetupPool();
    }
    private void FixedUpdate()
    {
        if (_counter <= 0 && ChooseSellingSpot()!=null)
        {
            _lastCustomer = GetPooledObject().GetComponent<Customer>();
            _lastCustomer.gameObject.SetActive(true);
            _lastCustomer. transform.position = _spawnPoint;
            _lastCustomer.GetInLine(ChooseSellingSpot());
            _lastCustomer = null;
            _counter = GameController._currentLevel.CustomerTime;
        }
        else
        {
            _counter -= Time.deltaTime;
        }
    }
    SellingSpot ChooseSellingSpot()
    {
        for (int i = 0; i < _sellingSpots.Count; i++)
        {
            if (_sellingSpot.IsUnityNull()) _sellingSpot = _sellingSpots[i];
            else if (_sellingSpots[i].CustomerCount() < _sellingSpot.CustomerCount())
            {
                _sellingSpot = _sellingSpots[i];
            }
        }
        if (_sellingSpot.CustomerCount() >= 3) return null;
        return _sellingSpot;
    }
}

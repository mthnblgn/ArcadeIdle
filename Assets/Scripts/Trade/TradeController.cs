using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TradeController : ObjectPool
{
    [SerializeField] private List<BuyingSpot> _buyingSpots;
    [SerializeField] private List<SellingSpot> _sellingSpots;
    [SerializeField] private float _counter = 2;

    private Customer _lastCustomer;
    SellingSpot sellingSpot;
    private void Start()
    {
        SetupPool();
    }
    private void FixedUpdate()
    {
        if (_counter <= 0 && ChooseSellingSpot()!=null)
        {
            _lastCustomer = GetPooledObject().GetComponent<Customer>();
            _lastCustomer.gameObject.SetActive(true);
            _lastCustomer.GetInLine(ChooseSellingSpot());
            _lastCustomer = null;
            _counter = 2;
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
            if (sellingSpot.IsUnityNull()) sellingSpot = _sellingSpots[i];
            else if (_sellingSpots[i].CustomerCount() < sellingSpot.CustomerCount())
            {
                sellingSpot = _sellingSpots[i];
            }
        }
        if (sellingSpot.CustomerCount() >= 3) return null;
        return sellingSpot;
    }
}

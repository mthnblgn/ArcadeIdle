using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Customer : PooledObject
{
    private SellingSpot _sellingSpot;
    public SellingSpot sellingSpot { get { return _sellingSpot; } set { _sellingSpot = value; } }

    private NavMeshAgent _agent;
    private Animator _animator;
    private bool _isInLine = false;
    private Ingredient[] _order;
    private void Awake()
    {
        _order = GameController.GetOrder() ;
        _agent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update()
    {
        if (_agent.remainingDistance <= 0.1f && !_isInLine)
        {
            _animator.SetFloat("Run", 0);
            transform.LookAt(_sellingSpot?.transform);
            _isInLine = true;
        }
        else _animator.SetFloat("Run", 1);
    }
    public void GetInLine(SellingSpot sSpot)
    {
        sellingSpot = sSpot;
        sellingSpot.AddCustomer(this);
        _isInLine=false;
        Vector3 destination = sellingSpot.transform.position + Vector3.back * 2 * sellingSpot.CustomerCount();
        _agent.SetDestination(destination);
    }
}

using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Customer : PooledObject
{
    private SellingSpot _sellingSpot;
    public SellingSpot sellingSpot { get { return _sellingSpot; } set { _sellingSpot = value; } }
    public int[] _order;

    [SerializeField] private Canvas _canvas;
    [SerializeField] private Image[] _orderImages;
    private NavMeshAgent _agent;
    private Animator _animator;
    private bool _isInLine = false;
    private bool _isBought = false;
    private void OnEnable()
    {
        _order = GameController.GetOrder();
        _agent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        for (int i = 0; i < _order.Length; i++)
        {
            _orderImages[i].color = GameController.ingredients[_order[i] - 1]._ingredientMat.color;
            _orderImages[i].gameObject.SetActive(true);
        }
    }
    // Update is called once per frame
    void Update()
    {

        if (_isInLine) return;
        if (_agent.remainingDistance <= 0.1f)
        {
            _animator.SetFloat("Run", 0);
            transform.DOLookAt(sellingSpot.transform.position, .5f);
            if (_sellingSpot._customers[0] == this) _canvas.transform.DOScale(Vector3.one, .5f);
            _isInLine = true;
        }
        else _animator.SetFloat("Run", 1);
    }
    public void GetInLine(SellingSpot sSpot)
    {
        sellingSpot = sSpot;
        sellingSpot.AddCustomer(this);
        _isInLine = false;
        Vector3 destination = sellingSpot.transform.position + Vector3.back * 2 * sellingSpot.CustomerCount();
        _agent.SetDestination(destination);
    }
    public void GetOutLine()
    {
        _isInLine = false;
        _canvas.transform.localScale = Vector3.zero;
        this.Release(0);
    }
}

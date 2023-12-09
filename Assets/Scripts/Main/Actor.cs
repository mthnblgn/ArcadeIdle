using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Experimental.GraphView.Port;

public class Actor : MonoBehaviour
{
    [SerializeField] int _carryCapacity;
    [SerializeField] float _carryGap;
    [SerializeField] Carrier _carryPointPrefab;
    [SerializeField] Transform _firstCarrierSpawnPoint;
    [SerializeField] float _velocity = 5;
    [SerializeField] float _rotationalSpeed = 20;
    [SerializeField] Animator _animController;
    [SerializeField] Carrier[] _carryPoints;
    Vector3 _direction;
    private void Awake()
    {
        _carryPoints = new Carrier[_carryCapacity];
    }
    private void Start()
    {
        for (int i = 0; i < _carryCapacity; i++)
        {
            _carryPoints[i] = Instantiate(_carryPointPrefab, _firstCarrierSpawnPoint.position + Vector3.up * _carryGap * i, Quaternion.identity, transform);
            _carryPoints[i]._isEmpty = true;
        }
    }
    void FixedUpdate()
    {
        Movement();
    }

    private void Movement()
    {
        _direction = Vector3.right * Input.GetAxis("Horizontal") + Vector3.forward * Input.GetAxis("Vertical");
        _direction.Normalize();
        transform.position += _direction * Time.deltaTime * _velocity;
        if (_direction != Vector3.zero) transform.DOLookAt(-_direction * _rotationalSpeed, .1f);
        _animController.SetFloat("Run", _direction.magnitude);
    }
    public void TakeIngredient(Ingredient ingredient)
    {
        _animController.SetBool("IsCarrying", true);
        ingredient.Carry(NextEmptyPoint());
    }
    public Transform NextEmptyPoint()
    {
        for (int i = 0; i < _carryPoints.Length; i++)
        {
            if (_carryPoints[i]._isEmpty)
            {
                return _carryPoints[i].transform;
            }
        }
        return null;
    }
    public Carrier[] ReturnLastCarryPoints(int count)
    {
        Carrier[] ret = new Carrier[count];
        ret = _carryPoints.Reverse().Skip(EmptyCarryCount()).Take(count).ToArray();
        return ret;
    }
    public void SellIngredient(int count)
    {
        Carrier[] carriers = ReturnLastCarryPoints(count);
        foreach (Carrier c in carriers)
        {
            Ingredient ingredient = c._ingredient;
            ingredient.Put();
            c._ingredient = null;
        }

        if (!IsCarry()) _animController.SetBool("IsCarrying", false);
    }
    public bool IsCarry()
    {
        for (int i = 0; i < _carryPoints.Length; i++)
        {
            if (!_carryPoints[i]._isEmpty) return true;
        }
        return false;
    }
    public int[] CarryIDs()
    {
        int[] carryIDs = new int[_carryCapacity];
        for (int i = 0; i < carryIDs.Length; i++)
        {
            Ingredient ingredient = _carryPoints[i]._ingredient;
            carryIDs[i] = (ingredient.IsUnityNull()) ? 0 : ingredient.ID;
        }
        return carryIDs;
    }
    public int EmptyCarryCount()
    {
        int count = 0;
        for (int i = 0; i < _carryPoints.Length; i++)
        {
            if (_carryPoints[i]._isEmpty) count++;
        }
        return count;
    }
}

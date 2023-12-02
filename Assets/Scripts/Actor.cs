using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class Actor : MonoBehaviour
{

    [SerializeField] float _velocity = 5;
    [SerializeField] float _rotationalSpeed = 20;
    [SerializeField] Animator _animController;
    [SerializeField] Transform _carryPoint;
    Ingredient _currentInggredient;
    Vector3 _direction;
    void FixedUpdate()
    {
        Movement();
    }

    private void Movement()
    {
        _direction = Vector3.right * Input.GetAxis("Horizontal") + Vector3.forward * Input.GetAxis("Vertical");
        _direction.Normalize();
        transform.position += _direction * Time.deltaTime * _velocity;
        transform.DOLookAt(-_direction * _rotationalSpeed, .25f);
        _animController.SetFloat("Run", _direction.magnitude);
    }
    public void TakeIngredient(Ingredient ingredient)
    {

        StartCoroutine(TakingCoroutine(ingredient));
    }

    IEnumerator TakingCoroutine(Ingredient ingredient)
    {
        _currentInggredient = ingredient;
        _currentInggredient.transform.SetParent(_carryPoint);
        _currentInggredient.transform.DOMove(_carryPoint.position,.5f);
        _animController.SetBool("IsCarrying", true);
        yield return null;
    }
}

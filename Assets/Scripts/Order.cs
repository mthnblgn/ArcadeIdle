using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Order : MonoBehaviour
{
    Ingredient[] orderIngredients;
    int level;
    private void Awake()
    {
        
    }
    void Start()
    {
        orderIngredients = new Ingredient[Random.Range(0,4)];
    }

    void Sell()
    {
        
    }
}

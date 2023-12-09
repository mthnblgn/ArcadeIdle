using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Ingredient", menuName = "Scriptables/Ingredients")]
public class IngredientScriptable : ScriptableObject
{
    public int ID;
    public string Name;
    public Material _ingredientMat;
    public int _ingredientSellValue;
}

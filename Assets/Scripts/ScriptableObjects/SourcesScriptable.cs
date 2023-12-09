using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Source", menuName = "Scriptables/Sources")]
public class SourcesScriptable : ScriptableObject
{
    public float _waitingTime;
    public IngredientScriptable ingredientData;
    public int _sourcePrice;
}

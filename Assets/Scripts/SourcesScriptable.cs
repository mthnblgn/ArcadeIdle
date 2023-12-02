using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Source", menuName = "Scriptables/Sources")]
public class SourcesScriptable : ScriptableObject
{
    public int ID;
    public string Name;
    public float _waitingTime;
    public Material _ingredientMat;
    public int _sourcePrice;
}

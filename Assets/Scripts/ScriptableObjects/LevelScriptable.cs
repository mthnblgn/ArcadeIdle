using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Level", menuName = "Scriptables/Levels")]
public class LevelScriptable : ScriptableObject
{
    public int Level;
    public int CustomerCount;
    public int OrderQuantity;
    public int OrderQuality;
    public int CustomerTime;
}

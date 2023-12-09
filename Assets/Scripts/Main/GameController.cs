using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public enum Phase
    {
        Start,
        Play,
        Pause,
        Over
    }
    public static Phase _phase;
    public static int _level { get { return _level; } set { if (value == _level + 1) _level = value; } }

    [SerializeField] LevelScriptable[] levels;
    [SerializeField] Ingredient[] _ingredients;
    public static LevelScriptable _currentLevel;
    public static Ingredient[] ingredients;
    void Start()
    {
        _currentLevel = levels[0];
        _phase = Phase.Start;
        ingredients = _ingredients;
    }

    // Update is called once per frame
    void Update()
    {

    }
    public static Ingredient[] GetOrder()
    {
        Ingredient[] order = new Ingredient[Random.Range(1,_currentLevel.OrderQuantity+1)];
        for (int i = 0; i < order.Length; i++)
        {
            order[i] = ingredients[Random.Range(0,_currentLevel.OrderQuality)];
        }
        return order;
    }
}

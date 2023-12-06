using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public enum Phase
    {
        Start,
        Game,
        Pause
    }
    private Phase phase;
    private int level;
    public Phase _phase { get { return phase; } set { phase = value; } }
    public int _level { get { return level; } /*set { level = value; }*/ }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

}

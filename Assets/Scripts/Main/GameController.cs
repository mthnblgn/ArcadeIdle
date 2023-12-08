using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    enum Phase
    {
        Start,
        Play,
        Pause,
        Over
    }
    static Phase _phase;
    void Start()
    {
        _phase = Phase.Start;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}

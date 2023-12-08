using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carrier : MonoBehaviour
{
    public bool _isEmpty=true;
    public Ingredient _ingredient;
    ParticleSystem particleSystem;
    private void Awake()
    {
        particleSystem = GetComponent<ParticleSystem>();
        particleSystem.Pause();
    }
    public void ExplodeParticle()
    {
        particleSystem.Play();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidManager : MonoBehaviour
{
    public float seperationAmount;
    public float alignmentAmount;
    public float cohesionAmount;
    public float goalForce;
    public float radius;

    public void UpdateSep(float sep)
    {
        seperationAmount = sep * 2000f;
    }

    public void UpdateAli(float ali)
    {
        alignmentAmount = ali * 10f;
    }

    public void UpdateCoh(float coh)
    {
        cohesionAmount = coh * 10f;
    }

    public void UpdateGF(float gf)
    {
        goalForce = gf * 50f;
    }

    
}

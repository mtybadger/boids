using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BoidSpawner : MonoBehaviour
{
    public GameObject boid;

    public int numberofBoids;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < numberofBoids; i++)
        {
            Instantiate(boid, new Vector3(Random.Range(-70, 70), Random.Range(-70, 70), Random.Range(-70, 70)), Quaternion.Euler(Random.Range(-180, 180), Random.Range(-180, 180), Random.Range(-180, 180)));
        }
    }
    

    public void Destroy()
    {
        foreach (var t in FindObjectsOfType<Boid>())
        {
            Destroy(t.gameObject);
        }
        
        FindObjectOfType<CameraOrbit>().polledBoids.Clear();
        
        for (int i = 0; i < numberofBoids; i++)
        {
            Instantiate(boid, new Vector3(Random.Range(-70, 70), Random.Range(-70, 70), Random.Range(-70, 70)), Quaternion.Euler(Random.Range(-180, 180), Random.Range(-180, 180), Random.Range(-180, 180)));
        }

        
    }
    
    public void UpdateCount(float count)
    {
        numberofBoids = Mathf.CeilToInt(count * 512f);
    }
}

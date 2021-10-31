using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using Random = UnityEngine.Random;

public class Boid : MonoBehaviour
{
    public List<Transform> nearbyBoids;
    
    private float _sep;
    private float _coh;
    private float _ali;
    private float _gf;
    private float _rad;
    private SphereCollider _sphere;
    private Rigidbody rb;
    private BoidManager man;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        man = GameObject.FindObjectOfType<BoidManager>();
        _sphere = GetComponent<SphereCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        _sep = man.seperationAmount;
        _coh = man.cohesionAmount;
        _ali = man.alignmentAmount;
        _gf = man.goalForce;
        _sphere.radius = man.radius;
        
        //SEPARATION
        Vector3 overallDirection = Vector3.zero;
        if (nearbyBoids.Count > 0)
        {
            foreach (Transform b in nearbyBoids)
            {
                Vector3 dir = transform.position - b.position;
                Vector3 weightedDir = dir.normalized / dir.magnitude;
                overallDirection += weightedDir;
            }
        }
        
        //ALIGNMENT
        
        Vector3 totalEulers = Vector3.zero;
        if (nearbyBoids.Count > 0)
        {
        foreach (Transform b in nearbyBoids)
        {
            totalEulers += b.transform.rotation.eulerAngles;
        }
        totalEulers = totalEulers / nearbyBoids.Count;
        totalEulers = totalEulers * _ali;
        }
        
        //cohesion
        Vector3 averagePosition = Vector3.zero;
        if (nearbyBoids.Count > 0)
        {
            foreach (Transform b in nearbyBoids)
            {
                averagePosition += b.transform.position;
            }

            averagePosition = averagePosition / nearbyBoids.Count;


            averagePosition = (averagePosition - transform.position) * _coh;
        }


        Vector3 separationDirection = overallDirection * _sep;
        Vector3 noise = new Vector3(Random.value - 0.5f, Random.value - 0.5f, 0f);


        Vector3 direction = separationDirection;
        Vector3 goalForce = -transform.position * _gf;
        Debug.DrawLine(transform.position, transform.position + direction + totalEulers + averagePosition + goalForce, Color.red);
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(transform.forward + direction + totalEulers + averagePosition + goalForce, Vector3.up), Time.deltaTime);

        rb.velocity = transform.forward * 30f;

    }
    
    private void OnTriggerEnter(Collider other)
    {
        Boid nearBoid = other.GetComponent<Boid>();
        if (nearBoid)
        {
            if (!nearBoid.nearbyBoids.Contains(transform))
            {
                nearBoid.nearbyBoids.Add(transform);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        BoidOld nearBoid = other.GetComponent<BoidOld>();
        if (nearBoid)
        {
            nearBoid.nearbyBoids.Remove(transform);
        }
    }

    private void OnBecameInvisible()
    {
        //float oldMag = transform.position.magnitude;
        //transform.position = transform.position - transform.forward * 3f;
        //while (transform.position.magnitude < oldMag)
       // {
       //     transform.position = transform.position - transform.forward;
       // }
    }
}

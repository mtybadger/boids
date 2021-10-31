using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

public class BoidOld : MonoBehaviour
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
        
        Vector3 direction = Vector3.zero;
        foreach (Transform b in nearbyBoids)
        {
            direction += transform.position - b.transform.position;
        }

        direction = direction * _sep;
        direction -= transform.position * _gf;
        Vector3 totalEulers = Vector3.zero;
        foreach (Transform b in nearbyBoids)
        {
            totalEulers += b.transform.rotation.eulerAngles;
        }

        Vector3 averagePosition = Vector3.zero;
        foreach (Transform b in nearbyBoids)
        {
            averagePosition += b.transform.position;
        } 
        averagePosition = averagePosition / nearbyBoids.Count;
        averagePosition = averagePosition - transform.position;
        averagePosition = averagePosition * _coh;
        direction += averagePosition;

        totalEulers = totalEulers / nearbyBoids.Count;
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(totalEulers), Time.deltaTime * 0.1f * _ali);
        rb.AddForce(direction);
        
    }

    private void OnTriggerEnter(Collider other)
    {
        BoidOld nearBoid = other.GetComponent<BoidOld>();
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
}

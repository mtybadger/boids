using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

public class CameraOrbit : MonoBehaviour
{
    public Vector3 rotation;
    public int numberOfBoidsToPoll;
    public List<Transform> polledBoids;

    public GameObject boid;
    public Vector3 velocity;
    public LayerMask layerMask;
    public float scrollVel;
    public Transform cam;
    public enum Mode
    {
        ClickToAdd,
        Orbit,
        Follow
    }

    public Mode ClickMode;
    // Update is called once per frame

    private void Start()
    {
        velocity = rotation;
        cam = transform.GetChild(0);
    }

    private void Update()
    {
        if (ClickMode == Mode.ClickToAdd)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                Debug.DrawRay(transform.position, ray.direction, Color.blue);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, 100f, layerMask ))
                {
                    Instantiate(boid, hit.point, Quaternion.Euler(0, 90, 0));
                }
            }
        }
        if (ClickMode == Mode.Orbit && Input.GetMouseButton(0))
        {
            velocity = new Vector3(velocity.x + Input.GetAxis("Mouse Y"), velocity.y - Input.GetAxis("Mouse X"), 0);
        }

        velocity = Vector3.Lerp(velocity, velocity.normalized * 2f, Time.deltaTime);
        scrollVel = scrollVel + Input.GetAxis("Mouse ScrollWheel");
        scrollVel = Mathf.Lerp(scrollVel, 0, Time.deltaTime * 5);
        cam.localPosition= new Vector3(0, 0, Mathf.Clamp(cam.localPosition.z + scrollVel, -300, 0));
        transform.Rotate(velocity * Time.deltaTime);


    }

    void FixedUpdate()
    {
        
        if (polledBoids.Count == 0)
        {
            foreach (var b in FindObjectsOfType<Boid>())
            {
                if (polledBoids.Count < numberOfBoidsToPoll)
                {
                    polledBoids.Add(b.transform);
                }
            }
            
        }

        Vector3 avgPos = Vector3.zero;
        foreach (var t in polledBoids)
        {
            avgPos += t.position;
        }

        avgPos = avgPos / numberOfBoidsToPoll;
        transform.position = Vector3.Lerp(transform.position, avgPos, Time.deltaTime);
        //transform.Rotate(velocity * Time.fixedDeltaTime);
    }

    public void SetClickMode(int mode)
    {
        if (mode == 0)
        {
            ClickMode = Mode.ClickToAdd;
        }
        if (mode == 1)
        {
            ClickMode = Mode.Orbit;
        }
    }
}

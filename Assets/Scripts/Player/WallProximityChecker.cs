using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallProximityChecker : MonoBehaviour
{
    Quaternion rotation;
    private bool touchingWall;
    public LayerMask layerMask;
    void Awake()
    {
        rotation = transform.rotation;
    }
    void Update()
    {

    }
    void fixedUpdate()
    {
        RaycastHit hit;
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.up) * 5.0f, Color.yellow);
        

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, layerMask))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            Debug.Log("Did Hit");
        }
    }
    void LateUpdate()
    {
        transform.rotation = rotation;
    }

}

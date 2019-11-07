using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy4WayTurret : MonoBehaviour
{
    public GameObject firingPoint1;
    public GameObject firingPoint2;
    public GameObject firingPoint3;
    public GameObject firingPoint4;

    public GameObject projectile1;
    public GameObject projectile2;
    public GameObject projectile3;
    public GameObject projectile4;

    public float rotationSpeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up* rotationSpeed * Time.deltaTime);
    }
}

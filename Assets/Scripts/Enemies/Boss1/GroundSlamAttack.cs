using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundSlamAttack : MonoBehaviour
{

    public float speed;
    public int damage = 1;
    
    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

  
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletspeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * bulletspeed * Time.deltaTime);
    }
    void FixedUpdate()
    {
        

    }
    private void OnCollisionEnter(Collision collision)
    {
        
        if (collision.gameObject.tag == "Wall")
        {
            Debug.Log("Bullet Destroyed");
            Destroy(gameObject);
        }
       
    }
}

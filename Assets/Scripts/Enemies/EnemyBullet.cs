using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float bulletspeed;
    public GameObject bulletImpact;
    public GameObject player;
    public int bulletDamage;
    
    void Start()
    {
        Destroy(gameObject, 5);
    }

    
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
            Instantiate(bulletImpact, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        if (collision.gameObject.tag == "Player")
        {         
            Instantiate(bulletImpact, transform.position, Quaternion.identity);
            Destroy(gameObject);
            
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            Instantiate(bulletImpact, transform.position, Quaternion.identity);
            Destroy(gameObject);
            other.gameObject.GetComponent<PlayerHealth>().takeDamage(bulletDamage);


        }
        if (other.gameObject.tag == "Wall")
        {
            Instantiate(bulletImpact, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }

    }
}

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
        Destroy(gameObject, 10);
    }

    
    void Update()
    {
         transform.Translate(Vector3.forward * bulletspeed * Time.deltaTime);
    
    }
    void FixedUpdate()
    {


    }
   
    private void OnTriggerEnter(Collider other)
    {

        if(other.gameObject.tag == "Player")
        {
           
            other.gameObject.GetComponent<PlayerHealth>().takeDamage(bulletDamage);
            Instantiate(bulletImpact, transform.position, Quaternion.identity);
            Destroy(gameObject);
            


        }
        if (other.gameObject.tag == "Wall")
        {
            Instantiate(bulletImpact, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }

        if( other.gameObject.tag == "Explosive")
        {
            Instantiate(bulletImpact, transform.position, Quaternion.identity);
            Destroy(gameObject);
            other.gameObject.GetComponent<Explosive>().takeDamage(bulletDamage);
        }

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShockWaveHitDetection : MonoBehaviour
{
    public int damage = 1;
    public float knockbackForce = 60;
   
    void Start()
    {
        knockbackForce = 60;
    }


    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Player")
        {
            Vector3 hitDirection = other.transform.position - transform.position;
            hitDirection = hitDirection.normalized;
            other.GetComponent<PlayerHealth>().knockBack(knockbackForce, hitDirection);

            Debug.Log("hit the player");
            other.gameObject.GetComponent<PlayerHealth>().takeDamage(damage);
           

        }
        if (other.gameObject.tag == "Wall")
        {
            Destroy(gameObject);
        }

    }
}

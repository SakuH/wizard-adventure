using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShockWaveHitDetection : MonoBehaviour
{
    public int damage = 1;
    public float knockbackForce = 60;
    // Start is called before the first frame update
    void Start()
    {
        knockbackForce = 60;
    }

    // Update is called once per frame
    void Update()
    {
        
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
            //Instantiate(bulletImpact, transform.position, Quaternion.identity);
            //Destroy(gameObject);

           

        }
        if (other.gameObject.tag == "Wall")
        {
            Destroy(gameObject);
        }

    }
}

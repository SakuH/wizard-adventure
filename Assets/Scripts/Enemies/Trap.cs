using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{

    public GameObject player;

    public int damage;

    private float nextFire;

    private bool canTakeDamage;

    public bool enemyCanTakeDamage;

    public float timeBetweenShots;


    // Start is called before the first frame update
    void Start()
    {
        canTakeDamage = false;
        player = GameObject.FindGameObjectWithTag("Player");

    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time > nextFire && canTakeDamage)
        {

        nextFire = Time.time + timeBetweenShots;
              
        player.GetComponent<PlayerHealth>().takeDamage(damage);

        }
        
        
    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.CompareTag("Player"))
        {
            canTakeDamage = true;
        }
         if(other.gameObject.CompareTag("Enemy") && enemyCanTakeDamage)
        {
            other.GetComponent<EnemyHealth>().takeDamage(damage);
            AudioManager.PlaySound("enemyHit");
        }

    }

    private void OnTriggerExit(Collider other) {
        if(other.gameObject.CompareTag("Player"))
        {
            canTakeDamage = false;
        }
    }
}

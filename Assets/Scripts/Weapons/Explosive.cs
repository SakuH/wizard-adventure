﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosive : MonoBehaviour
{
    bool hasExploded = false;
    public int health;
    public float explosionForce = 100;

    public int explosionEnemyDamage = 50;

    public int explosionPlayerDamage = 2;

    public float explosionRadius = 5f;

    public GameObject explosionEffect;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if ( health <= 0 && hasExploded == false)
        {
            Explode();
        }
    }

    void Explode()
    {
        hasExploded = true;
        Instantiate(explosionEffect, transform.position, transform.rotation);
        Destroy(gameObject);
        

        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);

        foreach ( Collider nearbyObject in colliders)
        {
            Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
            if(rb != null)
            {
                rb.AddExplosionForce(explosionForce, transform.position, explosionRadius);
            }
            EnemyHealth enemyHealth =nearbyObject.GetComponent<EnemyHealth>();
            if(enemyHealth != null)
            {
                enemyHealth.takeDamage(explosionEnemyDamage);
            }
            PlayerHealth playerHealth = nearbyObject.GetComponent<PlayerHealth>();
            if(playerHealth != null){
                playerHealth.takeDamage(explosionPlayerDamage);
            }
            
        }
        
    }

    public void takeDamage(int damage)
    {
        health = health - damage;
    }
}
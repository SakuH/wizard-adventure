using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public GameObject enemyParent;
    public int health;
    public bool dead;
    public float deathJumpSpeed;
    
    public Enemy4WayTurret turretScript; // define base script for the enemy

    
    void Start()
    {

        //find the base script from the game object
       // turretScript= gameObject.GetComponent<Enemy4WayTurret>();
    }

    
    void Update()
    {
        if (dead)
        {
            enemyParent.transform.position = new Vector3(transform.position.x, transform.position.y + deathJumpSpeed * Time.deltaTime, transform.position.z);
        }

      
    }
    public void takeDamage(int damage)
    {

        health -= damage;
        //on damage call the animation/sound/or effect you want to display when the enemy takes damage from the base script 
        if (!dead)
        {
            turretScript.takeDamageEffect();
        }
        
       
       

        
        if (health <= 0)
        {
            dead = true;
            Destroy(gameObject,5);
            
        }

    }
}

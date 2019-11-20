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
    public EnemyChaserScript chaserScript;

    public EnemyChaserShooterScript chaserShooterScript;


    
    void Start()
    {

        //find the base script from the game object
       // turretScript= gameObject.GetComponent<Enemy4WayTurret>();
    }

    
    void Update()
    {
        if (dead)
        {
            //call death animations from script
            enemyParent.transform.position = new Vector3(transform.position.x, transform.position.y + deathJumpSpeed * Time.deltaTime, transform.position.z);
        }

      
    }
    public void takeDamage(int damage)
    {

        health -= damage;
        //on damage call the animation/sound/or effect you want to display when the enemy takes damage from the base script 
        if (!dead)
        {   if(turretScript != null)
            {
                turretScript.takeDamageEffect();

            }
            

            if (chaserScript != null)
            {
                chaserScript.takeDamageEffect();
            }
            if (chaserShooterScript !=null)
            {
                chaserShooterScript.takeDamageEffect();
            }
        }
        
       
       

        
        if (health <= 0)
        {
            dead = true;
            //call gameobject destruction function here from the main script instead
            Destroy(gameObject,5);
            
        }

    }
}

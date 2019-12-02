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
    public BossScript bossScript;

    public bool isAbleToTakeDamage = true;

    
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
           // enemyParent.transform.position = new Vector3(transform.position.x, transform.position.y + deathJumpSpeed * Time.deltaTime, transform.position.z);
       
            if (turretScript != null)
            {
                //death animation here
            }
            if (chaserScript != null)
            {
               
            }
            if (chaserShooterScript != null)
            {
                
            }
            if (bossScript != null)
            {
                bossScript.deathEffect();
            }

        }


    }
    public void takeDamage(int damage)
    {
        if (isAbleToTakeDamage)
        {
            health -= damage;
        //on damage call the animation/sound/or effect you want to display when the enemy takes damage from the base script 
        if (!dead)
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<GameStats>().AddDamageDone(damage);

            if (turretScript != null)
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
            if (bossScript != null)
            {
                bossScript.takeDamageEffect();
            }


        }
      

        }
        
       
       

        
        if (!dead && health <= 0)
        {
            dead = true;
            GameObject.FindGameObjectWithTag("Player").GetComponent<GameStats>().AddEnemyDefeated();
            //call gameobject destruction function here from the main script instead
            Destroy(gameObject,5);
            
        }

    }
}

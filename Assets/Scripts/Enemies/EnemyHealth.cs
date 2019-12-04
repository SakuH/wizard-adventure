using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public GameObject enemyParent;
    public int health;
    public bool dead;
    public float deathJumpSpeed;

    public int timeToDestroy;
    
    public Enemy4WayTurret turretScript; // define base script for the enemy
    public EnemyChaserScript chaserScript;
    public EnemyChaserShooterScript chaserShooterScript;
    public EnemyShooterScript shooterScript;
    public BossScript bossScript;

    public bool isAbleToTakeDamage = true;

    public bool itemChanceRolled = false;
    public GameObject hpPickup;

    private GameObject player;
    public int dropChance = 10;
    void Start()
    {

        //find the base script from the game object
        // turretScript= gameObject.GetComponent<Enemy4WayTurret>();
        player = GameObject.FindGameObjectWithTag("Player");
    }
    void Update()
    {
        if (dead)
        {
            //call death animations from script 
            // enemyParent.transform.position = new Vector3(transform.position.x, transform.position.y + deathJumpSpeed * Time.deltaTime, transform.position.z);
            if (!itemChanceRolled&& hpPickup != null)
            {
                itemChanceRolled = true;
                int randomNumber = Random.Range(1,100);
                Debug.Log(randomNumber);
                if (randomNumber <= dropChance)
                {
                    Vector3 spawnPos = transform.position;
                    spawnPos.y = player.transform.position.y - 1 ;
                    Instantiate(hpPickup, spawnPos, Quaternion.identity);

                }
            }
       
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
            if (shooterScript != null)
            {
                
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
            if(shooterScript != null)
            {
                shooterScript.takeDamageEffect();
            }


        }
      

        }
        
       
       

        
        if (!dead && health <= 0)
        {
            dead = true;
            GameObject.FindGameObjectWithTag("Player").GetComponent<GameStats>().AddEnemyDefeated();
            //call gameobject destruction function here from the main script instead
            if(turretScript != null)
            {
                Destroy(enemyParent, 2);
            }
            else
            {
                Destroy(gameObject,timeToDestroy);
            }
            
            
        }

    }
}

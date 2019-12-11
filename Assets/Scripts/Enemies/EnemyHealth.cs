using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public GameObject enemyParent;
    public int health;
    public bool dead;
    public float deathJumpSpeed;

    public float timeToDestroy;
    
    public Enemy4WayTurret turretScript; // define base script for the enemy
    public EnemyChaserScript chaserScript;
    public EnemyChaserShooterScript chaserShooterScript;
    public EnemyShooterScript shooterScript;
    public BossScript bossScript;

    public bool isAbleToTakeDamage = true;

    public bool itemChanceRolled = false;

    public bool itemDropped = false;
    public GameObject hpPickup;
    public GameObject heartPickup;

    private GameObject player;
    private GameStats gameStats;
    public GameObject[] objects;
    public int hpDropChance = 10;
    public int weaponDropChance = 20;
    public int heartDropChance = 3;
    void Start()
    {

        //find the base script from the game object
        // turretScript= gameObject.GetComponent<Enemy4WayTurret>();
        player = GameObject.FindGameObjectWithTag("Player");
        gameStats = GameObject.Find("Player").GetComponentInChildren<GameStats>();
    }
    void Update()
    {
        if (dead)
        {
            
            // enemyParent.transform.position = new Vector3(transform.position.x, transform.position.y + deathJumpSpeed * Time.deltaTime, transform.position.z);
            if (!itemChanceRolled&& hpPickup != null)
            {
                itemChanceRolled = true;
                int randomNumber = Random.Range(1,100);
                Debug.Log(randomNumber);
                if (randomNumber <= heartDropChance && itemDropped == false)
                {
                    Vector3 spawnPos = transform.position;
                    spawnPos.y = player.transform.position.y - 1 ;
                    Instantiate(heartPickup, spawnPos, Quaternion.identity);
                    itemDropped = true;

                }
                if (randomNumber <= hpDropChance && itemDropped == false)
                {
                    Vector3 spawnPos = transform.position;
                    spawnPos.y = player.transform.position.y - 1 ;
                    Instantiate(hpPickup, spawnPos, Quaternion.identity);
                    itemDropped = true;

                }
                if (randomNumber <= weaponDropChance && itemDropped == false)
                {
                    int rand = Random.Range(0, objects.Length);
                    Vector3 spawnPos = transform.position;
                    spawnPos.y = player.transform.position.y + 1 ;

                    if(objects[rand].CompareTag("Weapon"))
                    {   
                    GameObject random = objects[rand];
               
                    Instantiate(random, spawnPos, Quaternion.identity);
              
                    }

                    itemDropped = true;

                }
                
            }
       
            if (turretScript != null)
            {
                //death animation here
                 enemyParent.transform.position = new Vector3(transform.position.x, transform.position.y + deathJumpSpeed * Time.deltaTime, transform.position.z);
            }
            if (chaserScript != null)
            {
                enemyParent.transform.position = new Vector3(transform.position.x, transform.position.y + deathJumpSpeed * Time.deltaTime, transform.position.z);
                Destroy(enemyParent, 1);
            }
            if (chaserShooterScript != null)
            {
                enemyParent.transform.position = new Vector3(transform.position.x, transform.position.y + deathJumpSpeed * Time.deltaTime, transform.position.z);
                Destroy(enemyParent, 1);
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
                gameStats.AddDamageDone(damage);
                //GameObject.FindGameObjectWithTag("Player").GetComponent<GameStats>().AddDamageDone(damage);
                //GameStats gameStats = GameObject.FindGameObjectWithTag("Player").GetComponent<GameStats>();
                //gameStats.AddDamageDone(damage);
                //if (GameObject.FindGameObjectWithTag("Player").GetComponent<GameStats>() != null)
                //{
                //    player.GetComponent<GameStats>().AddDamageDone(damage);
                //}
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
            gameStats.AddEnemyDefeated();
            //GameObject.FindGameObjectWithTag("Player").GetComponent<GameStats>().AddEnemyDefeated();
            //if (GameObject.FindGameObjectWithTag("Player").GetComponent<GameStats>() != null)
            //{
            //    player.GetComponent<GameStats>().AddEnemyDefeated();
            //}
            //call gameobject destruction function here from the main script instead
            if (turretScript != null)
            {
                Destroy(enemyParent, 2);
            }
            if(bossScript != null)
            {
                Destroy(enemyParent,3);
            }
            else
            {
                Destroy(gameObject,timeToDestroy);
            }
            
            
        }

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChaserScript : MonoBehaviour
{
    private int health;
     public GameObject player;
    public float movementSpeed = 4;

    public float playerDistanceOffset;
    public EnemyHealth chaserHealthScript;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        
    }


   
    void Update()
    {
        health = GetComponent<EnemyHealth>().health;


        float dist = Vector3.Distance(player.transform.position, transform.position);
        if (dist > playerDistanceOffset)
        {
            transform.LookAt(new Vector3 (player.transform.position.x , transform.position.y,player.transform.position.z));
        transform.position += transform.forward * movementSpeed * Time.deltaTime;

        }
  
    }
    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player"&& health> 0)
        {
            player.GetComponent<PlayerHealth>().isTouchingEnemy = true;
           
        }
    }
    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            player.GetComponent<PlayerHealth>().isTouchingEnemy = false;
           
        }
    }


    public void takeDamageEffect()
    {
        takeDamageSound();
       
    }

    public void takeDamageSound()
    {
        AudioManager.PlaySound("enemyHit");
    }



}

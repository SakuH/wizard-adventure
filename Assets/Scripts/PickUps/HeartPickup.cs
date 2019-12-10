using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartPickup : MonoBehaviour
{
    PlayerHealth healthScript;
    public int heartAmount = 2;
    // Start is called before the first frame update
    

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.tag == "Player")
        {
            healthScript = other.GetComponent<PlayerHealth>();
            
           /* if(healthScript.health < healthScript.maxHealth)
            {
                if(healthScript.health + heartAmount >healthScript.maxHealth)
                {
                    healthScript.health = healthScript.maxHealth;
                }else
                {
                    healthScript.health = healthScript.health + heartAmount;
                }
                healthScript.HpPickUpSound();
                Destroy(gameObject);
            }*/

           Debug.Log(healthScript.hearts.Length * 2); 
           if( healthScript.maxHealth >= healthScript.hearts.Length * 2)
           {    
               
               healthScript.maxHealth = healthScript.hearts.Length * 2;
           }
           else
           {
               healthScript.maxHealth = healthScript.maxHealth + heartAmount;
           }
            healthScript.HpPickUpSound();
            Destroy(gameObject);

        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartPickup : MonoBehaviour
{
    PlayerHealth healthScript;
    public int heartAmount = 2;

    void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.tag == "Player")
        {
            healthScript = other.GetComponent<PlayerHealth>();
            
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

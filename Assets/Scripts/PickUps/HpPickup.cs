using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpPickup : MonoBehaviour
{
    PlayerHealth healthScript;
    public int healAmount = 2;

    void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.tag == "Player")
        {
            healthScript = other.GetComponent<PlayerHealth>();
            
            if(healthScript.health < healthScript.maxHealth)
            {
                if(healthScript.health + healAmount >healthScript.maxHealth)
                {
                    healthScript.health = healthScript.maxHealth;
                }else
                {
                    healthScript.health = healthScript.health + healAmount;
                }
                healthScript.HpPickUpSound();
                Destroy(gameObject);
            }
            

        }
    }
}

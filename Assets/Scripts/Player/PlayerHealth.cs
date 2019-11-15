﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public GameObject playerParent;
    public float health;
    public bool dead; 
    public float maxHealth;
    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;
    public Sprite halfHeart;
    public Image backgroundImage;
    public float fadeToBlackColor;

    public bool vulnerable = true;
    public float vulnerableCoolDown;
    public float vulnerableCoolDownMax;


    public Material normalMat;
    public Material invulnerableMat;
    public GameObject head;

    PlayerMovement playerScript;
    
    // Start is called before the first frame update
    void Start()
    {
        playerScript = playerParent.GetComponent<PlayerMovement>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if(health > maxHealth)
        {
            health = maxHealth;
        }
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < health / 2)
            {
                if (i + 0.5f == health / 2)
                {
                    hearts[i].sprite = halfHeart;

                }
                else
                {
                    hearts[i].sprite = fullHeart;
                }


            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }


            if (i < maxHealth / 2&&!dead)
            {
                hearts[i].enabled = true;
            }else
            {
                hearts[i].enabled = false;
            }

            if (dead)
            {
                hearts[i].enabled = false;
            }


        }

        if(health <= 0)
        {
            var tempColor = backgroundImage.color;

            if(fadeToBlackColor < 1)
            {
                fadeToBlackColor += Time.deltaTime;
            }

            tempColor.a = fadeToBlackColor;
            backgroundImage.color = tempColor;
            
            FindObjectOfType<gameManagement>().endGame();
        }

        if (vulnerableCoolDown > 0)
        {
            vulnerableCoolDown -= Time.deltaTime;
           
            MeshRenderer gameObjectRenderer = head.GetComponent<MeshRenderer>();
            gameObjectRenderer.material = invulnerableMat;

        }
        else
        {
            vulnerable = true;
            MeshRenderer gameObjectRenderer = head.GetComponent<MeshRenderer>();
            gameObjectRenderer.material = normalMat;
        }
    }
    public void takeDamage(int damage)
    {
        if (vulnerable && playerScript.isDashing == false)
        {
            takeDamageSound();
        health -= damage;
            vulnerableCoolDown = vulnerableCoolDownMax;
            vulnerable = false;

        }
      
        if (health <= 0)
        {
            dead = true;          
        }

    }
    public void playerReset()
    {
        dead = false;
        health = maxHealth;
    }

    public void takeDamageSound()
    {
        AudioManager.PlaySound("playerHit");
    }


}

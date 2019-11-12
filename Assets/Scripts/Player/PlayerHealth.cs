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
    
    
    // Start is called before the first frame update
    void Start()
    {
        
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
    }
    public void takeDamage(int damage)
    {

        health -= damage;
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
}
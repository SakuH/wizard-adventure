﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScriptFinal : MonoBehaviour
{
    public Transform firingPoint1;
    public Transform firingPoint2;
    public Transform firingPoint3;
    public Transform firingPoint4;

    public GameObject projectile1;
    public GameObject projectile2;
    public GameObject projectile3;
    public GameObject projectile4;

    public float timeBetweenShot;
    public float timeBetweenShotMax;
    public float rotationSpeed;
    public int health;

    public float damageColorCooldownTime;
    public float damageColorCooldownTimeMax;
    public bool normalColor;

    public float whiteColorCooldownTime;
    public float whiteColorCooldownTimeMax;
    public bool whiteColorCooldown;

    public Material normalColorMaterial;
    public Material whiteMaterial;

    public GameObject enemyParent;
    public bool dead;
    public float deathJumpSpeed;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);

        if (timeBetweenShot <= 0)
        {
            Instantiate(projectile1, firingPoint1.position, firingPoint1.rotation);
            Instantiate(projectile1, firingPoint2.position, firingPoint2.rotation);
            Instantiate(projectile1, firingPoint3.position, firingPoint3.rotation);
            Instantiate(projectile1, firingPoint4.position, firingPoint4.rotation);

            timeBetweenShot = timeBetweenShotMax;



        }
        else if (health > 0)
        {
            timeBetweenShot -= Time.deltaTime;
        }

 

        if (damageColorCooldownTime > 0 && !normalColor)
        {
            damageColorCooldownTime -= Time.deltaTime;
        }
        else if (!normalColor)
        {
            GameObject whateverGameObject = gameObject;
            MeshRenderer gameObjectRenderer = whateverGameObject.GetComponent<MeshRenderer>();
            gameObjectRenderer.material = normalColorMaterial;
            normalColor = true;

            whiteColorCooldownTime = whiteColorCooldownTimeMax;
            whiteColorCooldown = true;

        }

        if (whiteColorCooldown)
        {
            if (whiteColorCooldownTime > 0)
            {
                whiteColorCooldownTime -= Time.deltaTime;

            }
            else
            {
                whiteColorCooldown = false;
            }

        }
        if(!dead && health <= 0)
        {
            dead = true;
        }
        if (dead)
        {
            //call death animations turretscript 
            enemyParent.transform.position = new Vector3(transform.position.x, transform.position.y + deathJumpSpeed * Time.deltaTime, transform.position.z);
        }



    }
    public void takeDamageEffect()
    {
        takeDamageSound();
        if (normalColor && !whiteColorCooldown)
        {
            GameObject whateverGameObject = gameObject;
            MeshRenderer gameObjectRenderer = whateverGameObject.GetComponent<MeshRenderer>();
            gameObjectRenderer.material = whiteMaterial;
            normalColor = false;
            damageColorCooldownTime = damageColorCooldownTimeMax;
        }
    }

    public void takeDamageSound()
    {
        AudioManager.PlaySound("enemyHit");
    }

    public void takeDamage(int damage)
    {

        health -= damage;

        //on damage call the animation/sound/or effect you want to display when the enemy takes damage from the base script 
        if (!dead)
        {
            takeDamageEffect();
        }

        if (health <= 0)
        {

            Destroy(gameObject, 5);

        }

    }
}

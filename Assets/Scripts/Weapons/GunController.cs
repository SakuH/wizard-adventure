﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    public bool isFiring;

    public Bullet bullet;
    public float bulletSpeed;

    public float timeBetweenShots;
    private float shotCounter;

    public Transform firePoint;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isFiring){

            shotCounter -= Time.deltaTime;
 
            if(shotCounter <= 0)
            {
                shotCounter = timeBetweenShots;
                Bullet newBullet = Instantiate(bullet,firePoint.position, firePoint.rotation) as Bullet;
                newBullet.setX = 0;
                newBullet.setY = 0;
                newBullet.setZ = 1;

                Bullet newBullet2 = Instantiate(bullet,firePoint.position, firePoint.rotation) as Bullet;
                newBullet2.setX = 0;
                newBullet2.setY = -1;
                newBullet2.setZ = 1;
                newBullet2.bulletspeed = bulletSpeed;

                Bullet newBullet3 = Instantiate(bullet,firePoint.position, firePoint.rotation) as Bullet;
                newBullet3.setX = 0;
                newBullet3.setY = 1;
                newBullet3.setZ = 1;
                
                // firePoint.Rotate(new Vector3(3,0,0));
                // firePoint.SetPositionAndRotation(firePoint.position,new Quaternion().se);
               // Bullet newBullet2 = Instantiate(bullet,firePoint.position, firePoint.rotation) as Bullet;
                
            }

        }
        else
        {
            shotCounter = 0;
        }
        
    }
}

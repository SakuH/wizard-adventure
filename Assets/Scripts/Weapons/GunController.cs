using System.Collections;
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
                newBullet.bulletspeed = bulletSpeed;


                Bullet newBullet2 = Instantiate(bullet,firePoint.position, firePoint.rotation) as Bullet;
                newBullet2.setX = 0;
                newBullet2.setY = -1;
                newBullet2.setZ = 1;
                newBullet2.bulletspeed = bulletSpeed;

                Bullet newBullet3 = Instantiate(bullet,firePoint.position, firePoint.rotation) as Bullet;
                newBullet3.setX = 0;
                newBullet3.setY = 1;
                newBullet3.setZ = 1;
                newBullet3.bulletspeed = bulletSpeed;

                 Bullet newBullet4 = Instantiate(bullet,firePoint.position, firePoint.rotation) as Bullet;
                newBullet4.setX = 0;
                newBullet4.setY = -0.5f;
                newBullet4.setZ = 1;
                newBullet4.bulletspeed = bulletSpeed;

                Bullet newBullet5 = Instantiate(bullet,firePoint.position, firePoint.rotation) as Bullet;
                newBullet5.setX = 0;
                newBullet5.setY = 0.5f;
                newBullet5.setZ = 1;
                newBullet5.bulletspeed = bulletSpeed;

                
                
                
            }

        }
        else
        {
            shotCounter = 0;
        }
        
    }
}

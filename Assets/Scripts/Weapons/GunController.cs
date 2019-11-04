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
                newBullet.bulletspeed = bulletSpeed;
            }

        }
        else
        {
            shotCounter = 0;
        }
        
    }
}

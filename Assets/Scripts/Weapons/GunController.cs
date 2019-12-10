using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GunController : MonoBehaviour
{
    public bool isFiring;
    public bool weaponIsEquiped;
    public bool canPickup;
    public bool overlapping;
    public Bullet bullet;
    public Bullet shotgunBullet;
    public Bullet snipperBullet;
    public Bullet godBullet;
    private PlayerMovement playerMovement;
    public float bulletSpeed;
    public int weaponDamage;
    public int equipedWeapon;
    public float timeBetweenShots;
    private float nextFire;
    public float sfxVolume;
    public float minPitch = 0.7f;
    public float maxPitch = 1.2f;
    public AudioClip weaponSound;
    public GameObject mainPlayerHand;
    public GameObject player;
    public GameObject weapon;
    public GameObject impactEffect;
    public GameObject[] anotherWeapons;
    public float rotatingSpeed = 5f;
    public float rotatingHeight = 0.5f;
    Vector3 rotatingPos;
    Vector3 tempPos;
    public bool isRotating;
    public bool equipedOnSpawn;
    public Transform firePoint;
    private LineRenderer line;
    private Light lightWeapon;
    private GameObject laserSoundGameObject;
    private AudioSource continuousWeaponAudiosource;

    void Start()
    {
        mainPlayerHand = GameObject.Find ("GameObjectHand");
        player = GameObject.Find("Player");
        playerMovement = player.GetComponent<PlayerMovement>();
        sfxVolume = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<GameAudioSettings>().sfxVolume;
        canPickup = false;
        rotatingPos =transform.position;
        line = firePoint.GetComponent<LineRenderer>();
        line.enabled = false;
        lightWeapon = firePoint.GetComponent<Light>();
        lightWeapon.enabled = false;

       if (equipedOnSpawn)
       {
        pickWeaponUp();
       }
          
    }

    void Update()
    {

        
        if (equipedWeapon == 0)
        {
            
            if(isFiring && Time.time > nextFire)
            {

            nextFire = Time.time + timeBetweenShots;
            StartCoroutine("pistolProjectiles");

            }
        }

        if (equipedWeapon == 1)
        {
           
            if(isFiring && Time.time > nextFire)
            {

                nextFire = Time.time + timeBetweenShots;
                shotgunProjectiles();
               
            }
            
        }

        if (equipedWeapon == 2)
        {
           
            if(isFiring && Time.time > nextFire)
            {

                nextFire = Time.time + timeBetweenShots;
                sniperProjectiles();

            }
           
        }

        if (equipedWeapon == 3)
        {

            StartCoroutine("laserProjectiles");
               
        }

        if (equipedWeapon == 4)
        {
             if(isFiring && Time.time > nextFire)
            {

                nextFire = Time.time + timeBetweenShots;
                godProjectile();
               
            }
        }
        
        if(Input.GetKeyDown("e") && canPickup)
        {
    
            pickWeaponUp();
            
        }

        if (Input.GetMouseButtonUp(0))
        {
            isFiring = false;
        }
        

         if ( isRotating){
        
        transform.Rotate(new Vector3(0,30,45) *Time.deltaTime , Space.World);
        tempPos = rotatingPos;
        tempPos.y += Mathf.Sin(Time.fixedTime * Mathf.PI * 1f) * 0.5f;
        transform.position = tempPos;
        
        }
    }

    private void shotgunProjectiles()
    {
        GunBlastSound(weaponSound);
          
        Bullet newBullet = Instantiate(shotgunBullet,firePoint.position, firePoint.rotation) as Bullet;
        newBullet.setX = 0;
        newBullet.setY = 0;
        newBullet.setZ = 1;
        newBullet.bulletspeed = bulletSpeed;
        newBullet.setDamage = weaponDamage;
        
        Bullet newBullet2 = Instantiate(shotgunBullet,firePoint.position, firePoint.rotation) as Bullet;
        newBullet2.setX = -0.06f;
        newBullet2.setY = 0;
        newBullet2.setZ = 1;
        newBullet2.bulletspeed = bulletSpeed;
        newBullet2.setDamage = weaponDamage;

        Bullet newBullet3 = Instantiate(shotgunBullet,firePoint.position, firePoint.rotation) as Bullet;
        newBullet3.setX = 0.06f;
        newBullet3.setY = 0;
        newBullet3.setZ = 1;
        newBullet3.bulletspeed = bulletSpeed;
        newBullet3.setDamage = weaponDamage;

        Bullet newBullet4 = Instantiate(shotgunBullet,firePoint.position, firePoint.rotation) as Bullet;
        newBullet4.setX = -0.03f;
        newBullet4.setY = 0;
        newBullet4.setZ = 1;
        newBullet4.bulletspeed = bulletSpeed;
        newBullet4.setDamage = weaponDamage;

        Bullet newBullet5 = Instantiate(shotgunBullet,firePoint.position, firePoint.rotation) as Bullet;
        newBullet5.setX = 0.03f;
        newBullet5.setY = 0;
        newBullet5.setZ = 1;
        newBullet5.bulletspeed = bulletSpeed;
        newBullet5.setDamage = weaponDamage;
    }

    IEnumerator pistolProjectiles()
    {
        GunBlastSound(weaponSound);
        Bullet newBullet = Instantiate(bullet,firePoint.position, firePoint.rotation) as Bullet;
        newBullet.setX = 0;
        newBullet.setY = 0;
        newBullet.setZ = 1;
        newBullet.bulletspeed = bulletSpeed;
        newBullet.setDamage = weaponDamage;

        yield return null;
    }

    private void sniperProjectiles()
    {
        GunBlastSound(weaponSound);
        Bullet newBullet = Instantiate(snipperBullet,firePoint.position, firePoint.rotation) as Bullet;
        newBullet.bulletspeed = bulletSpeed;
        newBullet.setX = 0;
        newBullet.setY = 0;
        newBullet.setZ = 1;
        newBullet.setDamage = weaponDamage;
        
    }

    private void godProjectile()
    {
        GunBlastSound(weaponSound);
        Bullet newBullet = Instantiate(godBullet,firePoint.position,firePoint.rotation) as Bullet;
        newBullet.bulletspeed = bulletSpeed;
        newBullet.setX = 0;
        newBullet.setY = 0;
        newBullet.setZ = 1;
        newBullet.setDamage = weaponDamage;
    }

    IEnumerator laserProjectiles()
    {   
        line.enabled = true;
        lightWeapon.enabled = true;
        RaycastHit hit;
        if (laserSoundGameObject == null)
        {
            laserSoundGameObject = new GameObject("Laser Firing Sound");
            continuousWeaponAudiosource = laserSoundGameObject.AddComponent<AudioSource>();
            laserSoundGameObject.transform.SetParent(gameObject.transform);
            continuousWeaponAudiosource.clip = weaponSound;
            continuousWeaponAudiosource.volume = sfxVolume;
            continuousWeaponAudiosource.loop = true;
            
        }
        continuousWeaponAudiosource.pitch = Random.Range(minPitch, maxPitch);

        while ( isFiring)
        {
            if (!continuousWeaponAudiosource.isPlaying)
            {
                continuousWeaponAudiosource.Play();
            }
        Ray ray = new Ray(firePoint.position, firePoint.forward);
        line.SetPosition(0,ray.origin);

        if(Physics.Raycast(ray, out hit,100) )
        {
            line.SetPosition(1, hit.point);
            
            if(isFiring && Time.time > nextFire)
            {

                nextFire = Time.time + timeBetweenShots;
                
                GameObject laserHit = Instantiate(impactEffect,hit.point,Quaternion.LookRotation(hit.normal));
                Destroy(laserHit,2f);

            if(hit.collider.gameObject.CompareTag("Enemy"))
            {
                hit.collider.GetComponent<EnemyHealth>().takeDamage(weaponDamage);
        
            }
            if(hit.collider.gameObject.CompareTag("Explosive"))
            {
                hit.collider.GetComponent<Explosive>().takeDamage(weaponDamage);
            }

            }
            
        }
        else
        {
            line.SetPosition(1,ray.GetPoint(100));
        }
        yield return null;
        }

        line.enabled = false;
        lightWeapon.enabled = false;
        continuousWeaponAudiosource.Stop();

    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.CompareTag("Player"))
        {
           
            canPickup = true;
            playerMovement.setWeaponPickUpText("Press [E] to switch weapons");

        }
        if (other.gameObject.CompareTag("Weapon") && weaponIsEquiped == false)
        {
            GunController gun2 =  other.GetComponent<GunController>();
            gun2.rotatingPos = rotatingPos;
            overlapping = true;
        }
    }
    private void OnTriggerExit(Collider other) {
        if(other.gameObject.CompareTag("Player"))
        {
            canPickup = false;
            playerMovement.setWeaponPickUpText("");

        }
          if (other.gameObject.CompareTag("Weapon"))
        {
            overlapping = false;
        }

    }

    public void pickWeaponUp(){
            
        playerMovement.weapon = this;
        isRotating = false;

        weapon.transform.parent = player.transform;
        weapon.transform.SetSiblingIndex(1);
        weapon.transform.rotation = mainPlayerHand.transform.rotation;
        weapon.transform.position = mainPlayerHand.transform.position;

        if (playerMovement.hasWeaponEquiped == true)
        {     
        player.transform.GetChild(2).transform.parent = null;
                  
            anotherWeapons = GameObject.FindGameObjectsWithTag("Weapon");
            foreach(GameObject anotherWeapon in anotherWeapons)
            {
                    
            GunController gun =  anotherWeapon.GetComponent<GunController>();
                  
                if (gun.weaponIsEquiped == true)
                {
                   
                    gun.isRotating = true;
                    gun.isFiring = false;
                    gun.weaponIsEquiped = false;
                    gun.rotatingPos = rotatingPos;

                }

            }      
        }
            
        player.GetComponent<PlayerMovement>().hasWeaponEquiped = true;
        weaponIsEquiped = true;
           
    }

    void GunBlastSound(AudioClip clip)
    {
        GameObject clipGameObject = new GameObject("Weapon Shot Sound");
        AudioSource source = clipGameObject.AddComponent<AudioSource>();
        clipGameObject.transform.position = transform.position;
        source.clip = clip;
        source.volume = sfxVolume;
        source.pitch = Random.Range(minPitch, maxPitch);
        source.Play();
        Destroy(clipGameObject, clip.length / source.pitch);
    }


}

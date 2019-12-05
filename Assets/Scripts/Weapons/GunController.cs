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

    private int playerChildCount;

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

    //private Light light;

    // Start is called before the first frame update
    void Start()
    {
       // transform.parent = null;
        mainPlayerHand = GameObject.Find ("GameObjectHand");
        player = GameObject.Find("Player");
        playerMovement = player.GetComponent<PlayerMovement>();

        sfxVolume = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<GameAudioSettings>().sfxVolume;
        
        //player = GameObject.FindGameObjectWithTag("Player");
       // originalRotation = mainPlayerHand.transform.rotation;
       // weapon = GameObject.Find("WeaponShotgun");
        //originalRotation = weapon.transform.rotation;
       // rotator =  weapon.GetComponent<Rotator>();
       // weaponEquiped = false;
        canPickup = false;
        rotatingPos =transform.position;
        Debug.Log(rotatingPos);
        Debug.Log(equipedWeapon);
        

       //rotatingPos = this.transform.InverseTransformPoint(transform.position);

        line = firePoint.GetComponent<LineRenderer>();
        line.enabled = false;
        
        lightWeapon = firePoint.GetComponent<Light>();
        lightWeapon.enabled = false;
       // light = firePoint.GetComponent<Light>();
       if ( equipedOnSpawn)
       {
        pickWeaponUp();

       }else{
       playerChildCount = player.transform.childCount + 2;

       }
        
       
    }

    // Update is called once per frame
    void Update()
    {

        
        if (equipedWeapon == 0)
        {
            
            if(isFiring && Time.time > nextFire)
            {

               /*  shotCounter -= Time.deltaTime;
 
                if(shotCounter <= 0)
               {
                shotCounter = timeBetweenShots;
                pistolProjectiles();
               }

            }
            else
            {
            shotCounter = 0;*/

            nextFire = Time.time + timeBetweenShots;
            //pistolProjectiles();
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
           // timeBetweenShots = 1;
            if(isFiring && Time.time > nextFire)
            {

                nextFire = Time.time + timeBetweenShots;
                sniperProjectiles();
               

            }
           
        }

        if (equipedWeapon == 3)
        {


            //if(isFiring){
            //  line.enabled = true;
            // while(isFiring)
            //  {


            StartCoroutine("laserProjectiles");
               

          //  }
          //  line.enabled = false;
           // }
            
            
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
        //float newY = Mathf.Sin(Time.time * rotatingSpeed) * rotatingHeight + rotatingPos.y;
        //transform.position = new Vector3(rotatingPos.x,newY,rotatingPos.z) * rotatingHeight;
        //transform.position= new Vector3(rotatingPos.x,newY,rotatingPos.z) * rotatingHeight;
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

        /*GameObject newBullet2 = Instantiate(secondBullet,firePoint.position, firePoint.rotation) ;
        newBullet2.GetComponent<Bullet>().setX = 0;
        newBullet2.GetComponent<Bullet>().setY = 0;
        newBullet2.GetComponent<Bullet>().setZ = 1;
        newBullet2.GetComponent<Bullet>().bulletspeed = bulletSpeed;
        newBullet2.GetComponent<Bullet>().setDamage = weaponDamage;*/


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
        //RaycastHit hit;
         //line.SetPosition(0,firePoint.localPosition);
        line.SetPosition(0,ray.origin);

        if(Physics.Raycast(ray, out hit,100) )
        {
           // line.SetPosition(1, line.transform.InverseTransformPoint(hit.point));
            line.SetPosition(1, hit.point);
            
               if(isFiring && Time.time > nextFire)
            {

                nextFire = Time.time + timeBetweenShots;
                
               GameObject laserHit = Instantiate(impactEffect,hit.point,Quaternion.LookRotation(hit.normal));
            Destroy(laserHit,2f);
            if(hit.collider.gameObject.CompareTag("Enemy"))
            {
                hit.collider.GetComponent<EnemyHealth>().takeDamage(weaponDamage);
        
                //hit.rigidbody.AddForceAtPosition(firePoint.forward * 5,hit.point);
            }
            if(hit.collider.gameObject.CompareTag("Explosive"))
            {
                hit.collider.GetComponent<Explosive>().takeDamage(weaponDamage);
            }

            }
            
        }
        else{
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
            
           //     if(Time.time > nextPickup)
           // {

           // nextPickup = Time.time + timeBetweenPickup;
           /*    if(weaponEquiped == false)
                {
                   //player.transform.GetChild(1).transform.parent = null; 
                 rotator.isRotating = false;
                   
                    weapon.transform.parent = player.transform;
            weapon.transform.SetSiblingIndex(1);
            weapon.transform.rotation = mainPlayerHand.transform.rotation;
            weapon.transform.position = mainPlayerHand.transform.position;
            player.transform.GetChild(2).transform.parent = null;
            weaponEquiped = true;
            playerMovement.weapon = this;
                }
                else if (weaponEquiped)
                {
                    rotator.isRotating = false;
                   
                    weapon.transform.parent = player.transform;
            weapon.transform.SetSiblingIndex(1);
            weapon.transform.rotation = mainPlayerHand.transform.rotation;
            weapon.transform.position = mainPlayerHand.transform.position;
            player.transform.GetChild(1).transform.parent = null;
            weaponEquiped = false;
            playerMovement.weapon = this;
                }*/
            
            

           // }

            /*rotator.isRotating = false;
            player.transform.GetChild(1).transform.parent = null;
            weapon.transform.parent = player.transform;
            weapon.transform.SetSiblingIndex(1);
            weapon.transform.rotation = mainPlayerHand.transform.rotation;
            weapon.transform.position = mainPlayerHand.transform.position;
            playerMovement.weapon = this;*/
            
           // player.transform.GetChild(0).transform.parent = null;
       // weaponPickUpText.text = "Press [E] to switch weapons";
        playerMovement.setWeaponPickUpText("Press [E] to switch weapons");

        }
        if (other.gameObject.CompareTag("Weapon") && weaponIsEquiped == false)
        {
            //other.ge = 
            
            GunController gun2 =  other.GetComponent<GunController>();
            gun2.rotatingPos = rotatingPos;
            overlapping = true;
        }
    }
    private void OnTriggerExit(Collider other) {
        if(other.gameObject.CompareTag("Player"))
        {
            canPickup = false;
           // weaponPickUpText.text = "";
            playerMovement.setWeaponPickUpText("");

        }
          if (other.gameObject.CompareTag("Weapon"))
        {
            overlapping = false;
        }

    }

    public void pickWeaponUp(){

            
           /* if(weaponEquiped == true)
            {
            isRotating = true;
            weaponEquiped = false;
            }
            if (weaponEquiped == false)
            {
             isRotating = false; 
             weaponEquiped = true;      
            }*/
            //GetComponent<Collider>().enabled = false;
            //GetComponent<Collider>().isTrigger = false;
            
            playerMovement.weapon = this;
           isRotating = false;
                     //  playerMovement.weapon = this;
           /* if (weaponEquiped == true)
            {
            //weaponEquiped = false;
            //isRotating = true;
            weaponEquiped = false;
            isRotating = true;
            Destroy(weapon,0);
            
            }
            else{
                weaponEquiped = true;
            }*/

            weapon.transform.parent = player.transform;
            weapon.transform.SetSiblingIndex(1);
            weapon.transform.rotation = mainPlayerHand.transform.rotation;
            weapon.transform.position = mainPlayerHand.transform.position;
            //this.gameObject.GetComponent("GunController")
          //  player.transform.GetChildCount;
          Debug.Log(" "+player.transform.childCount);
          Debug.Log(playerChildCount);
            
         //   if (player.transform.childCount == playerChildCount)
            if (playerMovement.hasWeaponEquiped == true)
            {
                
            player.transform.GetChild(2).transform.parent = null;
            
            
            
                anotherWeapons = GameObject.FindGameObjectsWithTag("Weapon");
                foreach(GameObject anotherWeapon in anotherWeapons)
                {
                    
                    GunController gun =  anotherWeapon.GetComponent<GunController>();
                   // if(gun.isRotating == false && equipedWeapon != gun.equipedWeapon)
                   // {
                   //     gun.transform.position = player.transform.position;
                   // }
                    if (gun.weaponIsEquiped == true)
                    {
                        //gun.rotatingPos = rotatingPos;
                       // rotatingPos = weapon.transform.position;
                        gun.isRotating = true;
                        gun.isFiring = false;
                        gun.weaponIsEquiped = false;
                        gun.rotatingPos = rotatingPos;
                    }
                   /*  if  (equipedWeapon == gun.equipedWeapon && gun.weaponIsEquiped == true )
                    {
                        gun.rotatingPos = player.transform.position;
                        gun.isRotating = true;
                        gun.weaponIsEquiped = false;
                    }*/
                  /*  if(gun.weaponIsEquiped == true){

                        gun.rotatingPos = transform.position;
                        gun.isRotating = true;
                        gun.isFiring = false;
                        gun.weaponIsEquiped = false;
                    }*/
                    
              //  }
            
            //Instantiate(player.transform.GetChild(2).gameObject,new Vector3(0, 0, 0),weapon.transform.rotation) ;
            //anotherWeapon = player.transform.GetChild(2).gameObject;
           // Instantiate(player.transform.GetChild(2).gameObject,weapon.transform.position,weapon.transform.rotation) ;
           // Destroy(player.transform.GetChild(2).gameObject,0);
           // gunController =  anotherWeapon.GetComponent(typeof(GunController)) as GunController;
            //gunController.isRotating = true;
        
          //  if(equipedWeapon == 0){
              // Instantiate(weapon,weapon.transform.position,weapon.transform.rotation) ;
            }
            
             //isRotating = true;
            }
            
            player.GetComponent<PlayerMovement>().hasWeaponEquiped = true;
            weaponIsEquiped = true;
            //player.transform.
            //weaponEquiped = true;
           // isRotating = true;
            
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

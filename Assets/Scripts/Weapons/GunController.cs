using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    public bool isFiring;

    public bool weaponEquiped;
    public bool canPickup;

    public Bullet bullet;
    private  GunController gunController;

    public PlayerMovement playerMovement;
    public float bulletSpeed;

    public int equipedWeapon;

    public float timeBetweenShots;

    private float nextFire;

    public GameObject mainPlayerHand;

    public GameObject player;

    public GameObject weapon;

    public GameObject anotherWeapon;

     public float rotatingSpeed = 5f;
    public float rotatingHeight = 0.5f;
    Vector3 rotatingPos;
    public bool isRotating;

    public Rotator rotator;

    public Transform firePoint;

    //private Quaternion originalRotation;
    // Start is called before the first frame update
    void Start()
    {
        mainPlayerHand = GameObject.Find ("GameObjectHand");
        player = GameObject.Find("Player");
       // originalRotation = mainPlayerHand.transform.rotation;
       // weapon = GameObject.Find("WeaponShotgun");
        //originalRotation = weapon.transform.rotation;
       // rotator =  weapon.GetComponent<Rotator>();
       // weaponEquiped = false;
        canPickup = false;
        rotatingPos = transform.position;
        
       
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
            pistolProjectiles();
            
            
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
        
        if(Input.GetKeyDown("e") && canPickup)
        {
           // isRotating = false;
            playerMovement.weapon = this;
            pickWeaponUp();
            
        }
        if (Input.GetMouseButtonUp(0))
        {
            isFiring = false;

        }
        
       // if(weaponEquiped == false){
        //    isRotating = true;
       // }

         if ( isRotating){

        float newY = Mathf.Sin(Time.time * rotatingSpeed) * rotatingHeight + rotatingPos.y;
        transform.position = new Vector3(rotatingPos.x,newY,rotatingPos.z) * rotatingHeight;
        transform.Rotate(new Vector3(0,30,45)*Time.deltaTime);

        }
    }

    private void shotgunProjectiles()
    {
          
        Bullet newBullet = Instantiate(bullet,firePoint.position, firePoint.rotation) as Bullet;
        newBullet.setX = 0;
        newBullet.setY = 0;
        newBullet.setZ = 1;
        newBullet.bulletspeed = bulletSpeed;


        Bullet newBullet2 = Instantiate(bullet,firePoint.position, firePoint.rotation) as Bullet;
        newBullet2.setX = 0;
        newBullet2.setY = -0.06f;
        newBullet2.setZ = 1;
        newBullet2.bulletspeed = bulletSpeed;

        Bullet newBullet3 = Instantiate(bullet,firePoint.position, firePoint.rotation) as Bullet;
        newBullet3.setX = 0;
        newBullet3.setY = 0.06f;
        newBullet3.setZ = 1;
        newBullet3.bulletspeed = bulletSpeed;

        Bullet newBullet4 = Instantiate(bullet,firePoint.position, firePoint.rotation) as Bullet;
        newBullet4.setX = 0;
        newBullet4.setY = -0.03f;
        newBullet4.setZ = 1;
        newBullet4.bulletspeed = bulletSpeed;

        Bullet newBullet5 = Instantiate(bullet,firePoint.position, firePoint.rotation) as Bullet;
        newBullet5.setX = 0;
        newBullet5.setY = 0.03f;
        newBullet5.setZ = 1;
        newBullet5.bulletspeed = bulletSpeed;
    }

    private void pistolProjectiles()
    {
        Bullet newBullet = Instantiate(bullet,firePoint.position, firePoint.rotation) as Bullet;
        newBullet.setX = 0;
        newBullet.setY = 0;
        newBullet.setZ = 1;
        newBullet.bulletspeed = bulletSpeed;
    }

    private void sniperProjectiles()
    {
        Bullet newBullet = Instantiate(bullet,firePoint.position, firePoint.rotation) as Bullet;
        newBullet.bulletspeed = 80;
        newBullet.setX = 0;
        newBullet.setY = 0;
        newBullet.setZ = 1;
        
    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.CompareTag("MainPlayer"))
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
            
        }
    }
    private void OnTriggerExit(Collider other) {
        if(other.gameObject.CompareTag("MainPlayer"))
        {
            canPickup = false;
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
           // weaponEquiped = true;
            
            if (player.transform.childCount == 3)
            {
                
            //player.transform.GetChild(2).transform.parent = null;
            //Instantiate(player.transform.GetChild(2).gameObject,new Vector3(0, 0, 0),weapon.transform.rotation) ;
            //anotherWeapon = player.transform.GetChild(2).gameObject;
            Instantiate(player.transform.GetChild(2).gameObject,weapon.transform.position,weapon.transform.rotation) ;
            Destroy(player.transform.GetChild(2).gameObject,0);
           // gunController =  anotherWeapon.GetComponent(typeof(GunController)) as GunController;
            //gunController.isRotating = true;
        
          //  if(equipedWeapon == 0){
              // Instantiate(weapon,weapon.transform.position,weapon.transform.rotation) ;
           // }
            
             //isRotating = true;
            }
            
            //weaponEquiped = false;
            //player.transform.
            //weaponEquiped = true;
            //isRotating = true;
            
    }


}

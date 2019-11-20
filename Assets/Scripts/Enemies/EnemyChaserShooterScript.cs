using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChaserShooterScript : MonoBehaviour
{
    private int health;
     public GameObject player;
    public float movementSpeed = 4;
    public float knocbackForce;

    public float turnSpeed = 4;
   
    public float playerDistanceOffset;
    public float raycastLength;

    private float nextFire;

    public float timeBetweenShots;
    public int damage = 1;

    public Transform firingPoint;
    public GameObject projectile;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        
    }


   
    void Update()
    {
        health = GetComponent<EnemyHealth>().health;


        float dist = Vector3.Distance(player.transform.position, transform.position);
        if (dist > playerDistanceOffset)
        {

            //var q = Quaternion.LookRotation(player.transform.position - transform.position);
           // transform.rotation = Quaternion.RotateTowards(transform.rotation, q, turnSpeed * Time.deltaTime);
        
            transform.LookAt(new Vector3 (player.transform.position.x , transform.position.y,player.transform.position.z));

            transform.position += transform.forward * movementSpeed * Time.deltaTime;

            if( Time.time > nextFire)
            {

            Instantiate(projectile,firingPoint.position,firingPoint.rotation);
            
            
            
            nextFire = Time.time + timeBetweenShots;   

            }
            
        }
  
    }
    void FixedUpdate()
    {
        raycastToPlayer();

    }
    /*
    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player"&& health> 0)
        {
            player.GetComponent<PlayerHealth>().isTouchingEnemy = true;

            Vector3 hitDirection = collision.transform.position - transform.position;
            hitDirection = hitDirection.normalized;
            player.GetComponent<PlayerHealth>().knockBack(knocbackForce,hitDirection);
            

        }
    }
    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            player.GetComponent<PlayerHealth>().isTouchingEnemy = false;
           
        }
    }

    */
    public void raycastToPlayer()
    {
        Vector3 fwd = transform.TransformDirection(Vector3.forward);
        Debug.DrawRay(transform.position, fwd * raycastLength, Color.green);
        RaycastHit objectHit;
        if (Physics.Raycast(transform.position, fwd, out objectHit, raycastLength)&&objectHit.transform.tag == "Player")
        {
            //do something if hit object ie
            if (objectHit.transform.tag == "Player")
            {
               

                //player.GetComponent<PlayerHealth>().isTouchingEnemy = true;
                Vector3 hitDirection = objectHit.transform.position - transform.position;
                hitDirection = hitDirection.normalized;
                player.GetComponent<PlayerHealth>().knockBack(knocbackForce, hitDirection);
                player.GetComponent<PlayerHealth>().takeDamage(damage);


            }

        }
        else
        {
            player.GetComponent<PlayerHealth>().isTouchingEnemy = false;
        }

    }


    public void takeDamageEffect()
    {
        takeDamageSound();
       
    }

    public void takeDamageSound()
    {
        AudioManager.PlaySound("enemyHit");
    }

    /*IEnumerator Sleep(int time)
    {
        yield return new WaitForSeconds(time);
    }*/

   /* IEnumerator Move()
    {
        transform.LookAt(new Vector3 (player.transform.position.x , transform.position.y,player.transform.position.z));

            transform.position += transform.forward * movementSpeed * Time.deltaTime;

            if( Time.time > nextFire)
            {

              
            Instantiate(projectile,firingPoint.position,firingPoint.rotation);
            yield return new WaitForSeconds(50f);
            
            
            nextFire = Time.time + timeBetweenShots;   

            }
        yield return null;
    }*/


}

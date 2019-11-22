using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossScript : MonoBehaviour
{
    private int health;
    public GameObject player;
    public float turnSpeed = 200;
    public float  movementSpeed = 10;
    public bool move = false;
    public bool lookAtPlayer;
    public bool spinning;
    public bool slamForward;
    public bool slamAoe;
    public bool spinningStarted = false;
    public float slamRotation = 0;
    public bool attacking;
    public float attackCooldown;
    public float attackCooldownMax = 10;
    public int amountOfAttacks;
    public int amountOfAoeAttacksMax = 6;
    public int amountOfForwardAttacksMax = 3;
    public bool closeToPlayer;
    public float raycastLength;

    public GameObject groundslamPrefab;
    public GameObject groundslamForwardPrefab;
    public Transform hammerHeadPoint;


    public float midAttackCooldownTime;
    public float midForwardAttackCooldownTimeMax = 3;
    public float midAoeAttackCooldownTimeMax= 0.4f;
    public float midSpinAttackCooldownTimeMax= 10f;
    public float spinRotationSpeed;

    public int attackrotation = 0;

    public Animator bossAnimator;

    public float waitForAnimation;
    public float waitForAnimationMax;
    public bool hasWaitedForAnimation;

    public float knockbackForce = 120;
    public int damage = 1;
    public bool finalPhase;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
       // attackCooldown = 10;
       
    }
    
    void Update()
    {
        health = GetComponent<EnemyHealth>().health;

        if (!finalPhase && health< 500)
        {
            finalPhase= true;
        }

        if (lookAtPlayer)
        {
            transform.LookAt(new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z));
            //transform.position += transform.forward * movementSpeed * Time.deltaTime;
        }

        if (move)
        {       
           

            float step = movementSpeed * Time.deltaTime; // calculate distance to move
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, step);
        }
        if (attackCooldown <= 0)
        {
            if(attackrotation == 0)
            {
                slamForward = true;
                
            }
            if (attackrotation == 1)
            {
                slamAoe = true;
            }
            if (attackrotation == 2)
            {
                spinning = true;

            }
           
        }

        if (spinning)
        {
            spinAttack();
        }

        if (slamForward)
        {

            slamHammerForwardAttack();   
        }
        if (slamAoe)
        {
            slamHammerAoeAttack();
        }


        if (attackCooldown > 0 && !attacking)
        {
            attackCooldown -= Time.deltaTime;
        }
        raycastToPlayer();

    }

    public void slamHammer()
    {//slow movement speed or stop,play animations
        if(slamRotation < 90)
        {
            slamRotation += 15;
        }
        else
        {
            slamRotation = 0;
        }

        Vector3 slamRotationVector = new Vector3(0, slamRotation, 0);
        Instantiate(groundslamPrefab, hammerHeadPoint.position, Quaternion.Euler(slamRotationVector));
        attackCooldown = attackCooldownMax;
    }

    public void slamHammerForward()
    {
        

        float angleY = transform.rotation.eulerAngles.y-180;
        Vector3 slamRotationForward = new Vector3(Quaternion.identity.x, angleY, Quaternion.identity.z);

        Instantiate(groundslamForwardPrefab, hammerHeadPoint.position,Quaternion.Euler(slamRotationForward));
        attackCooldown = attackCooldownMax;
    }


    public void spinHammer()
    {
        transform.Rotate(Vector3.up * spinRotationSpeed * Time.deltaTime);
        attackCooldown = attackCooldownMax;
    }
    
    public void distanceToPlayer()
    {

    }

    public void attack()
    {

        if (closeToPlayer)
        {

        }else if (!closeToPlayer)
        {
            
        }
        
    }


    public void slamHammerForwardAttack()
    {
        if (move)
        {
            move = false;
        }
        if (!lookAtPlayer)
        {
            lookAtPlayer = true;
        }
        if (!attacking)
        {
            attacking = true;
        }
        bossAnimator.SetBool("running", false);
        bossAnimator.SetBool("forwardSlamming", true);


        if (amountOfAttacks < amountOfForwardAttacksMax)
        {
            if (midAttackCooldownTime <= 0)
            {
                slamHammerForward();
                midAttackCooldownTime = midForwardAttackCooldownTimeMax;
                amountOfAttacks++;
            }
            else
            {
                midAttackCooldownTime -= Time.deltaTime;
            }
        }
        else
        {
            attacking = false;
            move = true;
            slamForward = false;
            amountOfAttacks = 0;
            if (attackrotation < 2)
            {
                attackrotation++;
            }
            else
            {
                attackrotation = 0;
            }
            bossAnimator.SetBool("forwardSlamming", false);
            bossAnimator.SetBool("running", true);
        }
    }

    public void slamHammerAoeAttack()
    {
        if (move)
        {
             move = false;
            
        }
        if (lookAtPlayer)
        {
            lookAtPlayer = false;
        }
        if (!attacking)
        {
            attacking = true;
        }
        bossAnimator.SetBool("running", false);
        bossAnimator.SetBool("aoeSlamming", true);

        if (amountOfAttacks < amountOfAoeAttacksMax)
        {
            if (midAttackCooldownTime <= 0)
            {   
                slamHammer();
                midAttackCooldownTime = midAoeAttackCooldownTimeMax;
                amountOfAttacks++;
            }
            else
            {
                midAttackCooldownTime -= Time.deltaTime;
            }
        }
        else
        {
            lookAtPlayer = true;
            attacking = false;
            move = true;
            slamAoe = false;
            amountOfAttacks = 0;
           
            if (attackrotation < 2)
            {
                attackrotation++;
            }
            else
            {
                attackrotation = 0;
            }
            bossAnimator.SetBool("running", true);
            bossAnimator.SetBool("aoeSlamming", false);
        }
    }
    public void spinAttack()
    {
        
        if (lookAtPlayer)
        {
            lookAtPlayer = false;
        }
        if (!attacking)
        {
            attacking = true;
        }

        bossAnimator.SetBool("spinning", true);
        bossAnimator.SetBool("running", true);

        if (midAttackCooldownTime<= 0&&!spinningStarted)
        {
            midAttackCooldownTime = midSpinAttackCooldownTimeMax;
            spinningStarted = true;
        }

        if (midAttackCooldownTime > 0)
        {
            spinHammer();
            midAttackCooldownTime -= Time.deltaTime;
        }
        else{
            lookAtPlayer = true;
            attacking = false;
            spinning = false;
            spinningStarted = false;

            if (attackrotation < 2)
            {
                attackrotation++;
            }
            else
            {
                attackrotation = 0;
            }
            bossAnimator.SetBool("spinning", false);
            bossAnimator.SetBool("running", true);

        }
        

    }

  
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Vector3 hitDirection = collision.transform.position - transform.position;
            hitDirection.y = 0;
            hitDirection = hitDirection.normalized;
            player.GetComponent<PlayerHealth>().knockBack(knockbackForce, hitDirection);

            Debug.Log("hit the player");
            collision.gameObject.GetComponent<PlayerHealth>().takeDamage(damage);
            //Instantiate(bulletImpact, transform.position, Quaternion.identity);
            //Destroy(gameObject);



        }
    }

    public void raycastToPlayer()
    {
        Vector3 fwd = transform.TransformDirection(Vector3.forward);
        Debug.DrawRay(transform.position, fwd * raycastLength, Color.green);
        RaycastHit objectHit;
        if (Physics.Raycast(transform.position, fwd, out objectHit, raycastLength) && objectHit.transform.tag == "Player")
        {

                Vector3 hitDirection = objectHit.transform.position - transform.position;
                hitDirection.y = 0;
                hitDirection = hitDirection.normalized;
                player.GetComponent<PlayerHealth>().knockBack(knockbackForce, hitDirection);
                player.GetComponent<PlayerHealth>().takeDamage(damage);


        }
       
    }

}

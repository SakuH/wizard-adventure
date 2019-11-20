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
   
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
       // attackCooldown = 10;
       
    }
    
    void Update()
    {
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

    }

    public void slamHammer()
    {//slow movement speed or stop,play animations
        if(slamRotation == 45)
        {
            slamRotation = 0;
        }
        else
        {
            slamRotation = 45;
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
    }
    public void raycastToPlayer()
    {
        Vector3 fwd = transform.TransformDirection(Vector3.forward);
        Debug.DrawRay(transform.position, fwd * raycastLength, Color.green);
        RaycastHit objectHit;
        if (Physics.Raycast(transform.position, fwd, out objectHit, raycastLength) && objectHit.transform.tag == "Player")
        {
            closeToPlayer = true;
        }else
        {
            closeToPlayer = false;
        }
     

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

        if(midAttackCooldownTime<= 0&&!spinningStarted)
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
            
        }
        

    }

}

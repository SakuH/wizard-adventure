using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class BossScript : MonoBehaviour
{   public callAttackScript animationAttackCallScript;
    private int health;
    private float maxHealth;
    public float healthPercentage;
    public GameObject player;
    public float turnSpeed = 200;
    public float  movementSpeed = 10;
    public float  movementSpeedFinalPhase = 13;
    public float originalMovementSpeed;
    public float spinAttackMovementSpeed;
    public bool move = false;
    public bool lookAtPlayer;
    public bool spinning;
    public bool slamForward;
    public bool slamAoe;
    public bool finalPhaseIsTransforming;
    public bool spinningStarted = false;
    public float slamRotation = 0;
    public bool attacking;
    public float attackCooldown;
    public float attackCooldownMax = 10;
    public int amountOfAttacks;
    public int amountOfAoeAttacksMax = 6;
    public int amountOfForwardAttacksMax = 3;
    public int amountOfForwardAttacksMaxFinalPhase = 6;
    public bool closeToPlayer;
    public float raycastLength;
    public float raycastHeight = 3;

    public GameObject groundslamPrefab;
    public GameObject groundslamForwardPrefab;
    public Transform hammerHeadPoint;
    public GameObject deathExplosion;
    
    public float midAttackCooldownTime;
    public float midForwardAttackCooldownTimeMax = 3;
    public float midAoeAttackCooldownTimeMax= 0.4f;
    public float midSpinAttackCooldownTimeMax= 10f;
    public float midSpinAttackCooldownTimeMaxFinalPhase= 7f;
    public float midFinalPhaseTransformationCooldownTimeMax = 5f;
    public float spinRotationSpeed;
    public float spinRotationSpeedFinalPhase;
    public GameObject bossSkin;
    public GameObject bossShoulders;
    public Material finalPhaseSkinColor;
    public Material takeDamageSkinColor;
    public Material normalShoulderColor;

    public float damageColorCooldownTime;
    public float damageColorCooldownTimeMax = 0.3f;
    public bool normalColor;
    public float normalColorCooldownTime;
    public float normalColorCooldownTimeMax = 0.5f;
    public bool takeDamageColorCooldown;
   
    public int attackrotation = 0;

    public Animator bossAnimator;

    public bool instantiateShockwave = false;

    public float knockbackForce = 120;
    public int damage = 1;
    public bool finalPhase;
    private int explosionCount;
    public TextMeshProUGUI bossHealthBar;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
       // attackCooldown = 10;
       maxHealth = GetComponent<EnemyHealth>().health;
        bossHealthBar = FindObjectOfType<TextMeshProUGUI>();
        


    }
    
    void Update()
    {
        
        health = GetComponent<EnemyHealth>().health;

        if (health > 0)
        {
            bossHealthBar.text = "BossHp:" + health;
        }
        else
        {
            bossHealthBar.text = " ";
        }
        float tempHp = health;
        healthPercentage = tempHp / maxHealth;
        if (!finalPhase && health< 500&&!attacking)
        {
            finalPhase= true;

            GetComponent<EnemyHealth>().isAbleToTakeDamage = false; //change hp bar color to make it clear that the enemy does not take damage for the duration of the transformation 
            attackCooldownMax = midSpinAttackCooldownTimeMaxFinalPhase;
            movementSpeed = movementSpeedFinalPhase;

            attackrotation = 3;
            
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
            if(attackrotation == 3)
            {
                finalPhaseIsTransforming = true;
            }
           
        }
        
        
        if (spinning)
        {
            if (!finalPhase && health < 500)
            {
                GetComponent<EnemyHealth>().isAbleToTakeDamage = false;          
            }
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

        if (finalPhaseIsTransforming)
        {
            finalPhaseTransformation();
        }


    
        if (attackCooldown > 0 && !attacking)
        {
            attackCooldown -= Time.deltaTime;
        }

        raycastToPlayer();


        if (damageColorCooldownTime > 0 && !normalColor)
        {
            damageColorCooldownTime -= Time.deltaTime;
        }
        else if (!normalColor)
        {
            if(bossShoulders.GetComponent<SkinnedMeshRenderer>().material != normalColor)
            {
                bossShoulders.GetComponent<SkinnedMeshRenderer>().material = normalShoulderColor;
                normalColor = true;
                normalColorCooldownTime = normalColorCooldownTimeMax;
                
            }
        }

        if(normalColorCooldownTime > 0)
        {
            if (!takeDamageColorCooldown)
            {
                takeDamageColorCooldown = true;
            }

           normalColorCooldownTime -= Time.deltaTime;
        }
        else
        {
            if (takeDamageColorCooldown)
            {
                takeDamageColorCooldown = false;
            }
           
        }
      
        
        

        


 
    }

    public void slamHammer()
    {
        if (!finalPhase)
        {
            if (slamRotation < 101)
        {
            slamRotation += 20;
        }
        else
        {
            slamRotation = 0;
        }

        Vector3 slamRotationVector = new Vector3(0, slamRotation, 0);
        Instantiate(groundslamPrefab, hammerHeadPoint.position, Quaternion.Euler(slamRotationVector));
        attackCooldown = attackCooldownMax;

        }
        if (finalPhase)
        {
            if (slamRotation < 101)
            {
                slamRotation += 20;
            }
            else
            {
                slamRotation = 0;
            }

            Vector3 slamRotationVector = new Vector3(0, slamRotation, 0);
            Vector3 slamRotationVector2 = new Vector3(0, slamRotation-45, 0);
            Instantiate(groundslamPrefab, hammerHeadPoint.position, Quaternion.Euler(slamRotationVector));
            Instantiate(groundslamPrefab, hammerHeadPoint.position, Quaternion.Euler(slamRotationVector2));
            attackCooldown = attackCooldownMax;
        }
        }

    public void slamHammerForward()
    {

        if (!finalPhase)
        {
            float angleY = transform.rotation.eulerAngles.y - 180;
            Vector3 slamRotationForward = new Vector3(Quaternion.identity.x, angleY, Quaternion.identity.z);

            Instantiate(groundslamForwardPrefab, hammerHeadPoint.position, Quaternion.Euler(slamRotationForward));
            attackCooldown = attackCooldownMax;
        }
        if (finalPhase)
        {
            float angleY = transform.rotation.eulerAngles.y - 140;
            float angleY2 = transform.rotation.eulerAngles.y - 220;
            Vector3 slamRotationForward = new Vector3(Quaternion.identity.x, angleY, Quaternion.identity.z);
            Vector3 slamRotationForward2 = new Vector3(Quaternion.identity.x, angleY2, Quaternion.identity.z);

            Instantiate(groundslamForwardPrefab, hammerHeadPoint.position, Quaternion.Euler(slamRotationForward));
            Instantiate(groundslamForwardPrefab, hammerHeadPoint.position, Quaternion.Euler(slamRotationForward2));
            attackCooldown = attackCooldownMax;
        }





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

        if (finalPhase&&amountOfForwardAttacksMax < amountOfForwardAttacksMaxFinalPhase)
        {
            amountOfForwardAttacksMax = amountOfForwardAttacksMaxFinalPhase;
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
        if (finalPhase && spinRotationSpeed < spinRotationSpeedFinalPhase&&!attacking)
        {
            spinRotationSpeed = spinRotationSpeedFinalPhase;
        }
        
        if (lookAtPlayer)
        {
            lookAtPlayer = false;
            originalMovementSpeed = movementSpeed;
            movementSpeed = spinAttackMovementSpeed;
            if (finalPhase)
            {
                movementSpeed = spinAttackMovementSpeed + 5;
            }
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
            movementSpeed = originalMovementSpeed;
            midAttackCooldownTime = 0.68f;

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

            //Debug.Log("hit the player");
            collision.gameObject.GetComponent<PlayerHealth>().takeDamage(damage);
            //Instantiate(bulletImpact, transform.position, Quaternion.identity);
            //Destroy(gameObject);



        }
    }

    public void raycastToPlayer()
    {
        Vector3 fwd = transform.TransformDirection(Vector3.forward);
        
        RaycastHit objectHit;
        Vector3 raycastLocation = transform.position;
        raycastLocation.y = raycastHeight;
        Debug.DrawRay(raycastLocation, fwd * raycastLength, Color.green);
        if (Physics.Raycast(raycastLocation, fwd, out objectHit, raycastLength))
        {
            Debug.Log(objectHit.transform.name);

            if (objectHit.transform.tag == "Player")
            {
                Debug.Log("player");
                Vector3 hitDirection = objectHit.transform.position - transform.position;
                hitDirection.y = 0;
                hitDirection = hitDirection.normalized;
                player.GetComponent<PlayerHealth>().knockBack(knockbackForce, hitDirection);
                player.GetComponent<PlayerHealth>().takeDamage(damage);
            }
                


        }
       
    }

    public void finalPhaseTransformation()
    {
        if (lookAtPlayer)
        {
            lookAtPlayer = false;
            attacking = true;
        }
        if (move)
        {
            move = false;

        }


       
        if (bossAnimator.GetBool("finalPhase")==false)
        {
            bossAnimator.SetBool("finalPhase", true);
            midAttackCooldownTime = midFinalPhaseTransformationCooldownTimeMax;

        }

        if (midAttackCooldownTime > 0)
        {
            if (midAttackCooldownTime <= 2.5f&& bossSkin.GetComponent<SkinnedMeshRenderer>().material != finalPhaseSkinColor)
            {
                bossSkin.GetComponent<SkinnedMeshRenderer>().material = finalPhaseSkinColor;             
            }
            midAttackCooldownTime -= Time.deltaTime;
        }
        else
        {
            lookAtPlayer = true;
            attacking = false;
            move = true;


            if (attackrotation < 2)
            {
                attackCooldown = attackCooldownMax;
                attackrotation++;
            }
            else
            {
                attackCooldown = attackCooldownMax;
                attackrotation = 0;
            }
            GetComponent<EnemyHealth>().isAbleToTakeDamage = true;
            finalPhaseIsTransforming = false;
            bossAnimator.SetBool("finalPhase", false);
            bossAnimator.SetBool("running", true);
            midAttackCooldownTime = 0.68f;
        }


    }

    public void takeDamageEffect()
    {
        if (normalColor && !takeDamageColorCooldown)
        {
            bossShoulders.GetComponent<SkinnedMeshRenderer>().material =takeDamageSkinColor;
            normalColor = false;
            damageColorCooldownTime = damageColorCooldownTimeMax;
        }
    }
    public void deathEffect()
    {
        Destroy(gameObject, 2);
        if(explosionCount<1)    
        {
            bossAnimator.SetBool("dead", true);
            move = false;
            lookAtPlayer = false;
            //Instantiate(deathExplosion, transform.position,transform.rotation);
            explosionCount++;
        }
    }
    void OnDestroy()
    {
        Instantiate(deathExplosion, transform.position, transform.rotation);
    }
    public void showBossHealth()
    {

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooterScript : MonoBehaviour
{
    
    public GameObject player;
    public GameObject deathExplosion;
    public GameObject shootAnimation;
    bool isQuiting;
    public float knocbackForce;
    public float playerDistanceOffset;
    public float raycastLength;
    private float nextFire;
    public float timeBetweenShots;
    public int damage = 1;
    public Transform firingPoint;
    public GameObject projectile;
    public float sfxVolume;
    public float minPitch = 0.9f;
    public float maxPitch = 1.1f;
    public AudioClip takeDamageClip;
    public AudioClip shootClip;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        sfxVolume = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<GameAudioSettings>().sfxVolume;
    }

    void Update()
    {
        


        float dist = Vector3.Distance(player.transform.position, transform.position);
        if (dist > playerDistanceOffset)
        {
        
            transform.LookAt(new Vector3 (player.transform.position.x , transform.position.y ,player.transform.position.z));

            if( Time.time > nextFire)
            {
                EnemySound(shootClip);
            Instantiate(projectile,firingPoint.position,firingPoint.rotation);
            Instantiate(shootAnimation, firingPoint.position, firingPoint.rotation);
            nextFire = Time.time + timeBetweenShots;   

            }
            
        }
  
    }
    void FixedUpdate()
    {
        raycastToPlayer();

    }
  
    public void raycastToPlayer()
    {
        Vector3 fwd = transform.TransformDirection(Vector3.forward);
        Debug.DrawRay(transform.position, fwd * raycastLength, Color.green);
        RaycastHit objectHit;
        if (Physics.Raycast(transform.position, fwd, out objectHit, raycastLength)&&objectHit.transform.tag == "Player")
        {
        
            if (objectHit.transform.tag == "Player")
            {
               
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
        EnemySound(takeDamageClip);
    }

    public void explode()
    {
        
        Instantiate(deathExplosion, transform.position, transform.rotation);
        
    }
     private void OnDestroy() {
         if (!isQuiting){
            
            explode();

         }
     }
     private void OnApplicationQuit() {
         isQuiting = true;
     }

    void EnemySound(AudioClip clip)
    {
        GameObject clipGameObject = new GameObject("Enemy Chaser Shooter Sound");
        AudioSource source = clipGameObject.AddComponent<AudioSource>();
        clipGameObject.transform.position = transform.position;
        source.clip = clip;
        source.volume = sfxVolume;
        source.pitch = Random.Range(minPitch, maxPitch);
        source.Play();
        Destroy(clipGameObject, clip.length / source.pitch);
    }

}

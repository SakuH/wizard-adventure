using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletspeed;
    public float setX;
    public float setY;
    public float setZ;
    public GameObject bulletImpact;

    public GameObject explosionEffect;

    public bool isExplosiveBullet;

    public float sfxVolume;

    public float minPitch = 0.8f;
    public float maxPitch = 1.1f;

    public AudioClip bulletCollidingSound;


    private EnemyHealth enemyHP;


    public int setDamage;
    // Start is called before the first frame update
    void Start()
    {
        
        sfxVolume = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<GameAudioSettings>().sfxVolume;

        Destroy(gameObject,5);

    }

    // Update is called once per frame
    void Update()
    {
       
       // transform.Translate(Vector3.forward * bulletspeed * Time.deltaTime);
        transform.Translate(new Vector3(setX,setY,setZ) * bulletspeed * Time.deltaTime);
    }
    void FixedUpdate()
    {
        

    }
    private void OnCollisionEnter(Collision collision)
    {
        
        if (collision.gameObject.tag == "Wall")
        {
            
            Instantiate(bulletImpact, transform.position, Quaternion.identity);
            BulletCollisionSound(bulletCollidingSound);
            
         
            Destroy(gameObject);
        }
        if (collision.gameObject.tag == "Enemy")
        {         
            Instantiate(bulletImpact, transform.position, Quaternion.identity);
            collision.gameObject.GetComponent<EnemyHealth>().takeDamage(setDamage);

         
            Destroy(gameObject);   
            
        }
        if ( collision.gameObject.tag == "Explosive")
        {
            Instantiate(bulletImpact, transform.position, Quaternion.identity);
            collision.gameObject.GetComponent<Explosive>().takeDamage(setDamage);

           
            Destroy(gameObject);
        }
    }
    void BulletCollisionSound(AudioClip clip)
    {
        GameObject clipGameObject = new GameObject("Bullet Collision Sound");
        AudioSource source = clipGameObject.AddComponent<AudioSource>();
        clipGameObject.transform.position = transform.position;
        source.clip = clip;
        source.volume = sfxVolume;
        source.pitch = Random.Range(minPitch, maxPitch);
        source.Play();
        Destroy(clipGameObject, clip.length / source.pitch);
    }

    void explode()
    {
        Instantiate(explosionEffect,transform.position,transform.rotation);  
    }
    private void OnDestroy() {
           if(isExplosiveBullet)
            {
              //explode();
              this.GetComponent<Explosive>().Explode();
                
                
            }
    }
}

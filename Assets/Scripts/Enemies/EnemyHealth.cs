using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public GameObject enemyParent;
    public int health;
    public bool dead;
    public float deathJumpSpeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (dead)
        {
            enemyParent.transform.position = new Vector3(transform.position.x, transform.position.y + deathJumpSpeed * Time.deltaTime, transform.position.z);
        }   
    }
    public void takeDamage(int damage)
    {

        health -= damage;
        if (health <= 0)
        {
            dead = true;
            Destroy(gameObject,5);
            
        }

    }
}

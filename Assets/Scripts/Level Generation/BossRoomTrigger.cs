using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRoomTrigger : MonoBehaviour
{
    public List<GameObject> enemies;
    public bool enemiesPresent = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(enemiesPresent)
        {
            enemiesPresent = false;
            foreach (GameObject enemy in enemies)
            {
                if (enemy != null)
                {
                    if (!enemy.GetComponentInChildren<EnemyHealth>().dead)
                    {
                        enemiesPresent = true;
                    }
                }
            }
            if (!enemiesPresent)
            {
                Debug.Log("Spawn next level teleporter here");
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            enemies.Add(other.gameObject);
            enemiesPresent = true;
        }
        
    }

    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.CompareTag("Enemy"))
    //    {
    //        enemiesPresent = false;
    //    }
    //}

    //private void OnTriggerStay(Collider other)
    //{
    //    bool enemy = false;
    //    if (other.CompareTag("Enemy"))
    //    {
    //        if (other.gameObject.GetComponentInChildren<EnemyHealth>().dead)
    //        {
    //            enemy = false;
    //        }
    //        enemiesPresent = true;
    //    }
    //}
}

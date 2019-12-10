﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRoomTrigger : MonoBehaviour
{
    public List<GameObject> enemies;
    public bool enemiesPresent = false;

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
                GameObject.FindGameObjectWithTag("MainCamera").GetComponent<BackgroundAudio>().PlayVictoryMusic();
                Debug.Log("Spawn next level teleporter here");
                gameObject.GetComponentInChildren<NextLevelTeleport>().ActivateTeleporter();
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
        if (other.CompareTag("Player"))
        {
            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<BackgroundAudio>().PlayBossMusic();
        }

    }


}

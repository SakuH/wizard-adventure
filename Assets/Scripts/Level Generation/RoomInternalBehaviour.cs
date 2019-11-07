using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomInternalBehaviour : MonoBehaviour
{
    public List<GameObject> enemies;
    public GameObject[] doors;


    public bool openDoors = false;
    public bool enemiesInRoom = false;
    void Start()
    {
        
    }

    void Update()
    {
        if (enemiesInRoom)
        {
            foreach (GameObject enemy in enemies)
            {
                if (!enemy.GetComponent<EnemyAsPartOfRoom>().GetAliveStatus())
                {
                    Destroy(enemy, 2f);
                    enemies.Remove(enemy);
              
                }
            }
            if (enemies.Count == 0)
            {
                openDoors = true;
                enemiesInRoom = false;
            }
        }
    }

    public void AddEnemyToRoom(GameObject enemy)
    {
        enemiesInRoom = true;
        enemies.Add(enemy);
    }
}

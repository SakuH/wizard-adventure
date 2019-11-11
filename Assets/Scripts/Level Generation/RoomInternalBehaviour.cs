using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomInternalBehaviour : MonoBehaviour
{
    public List<GameObject> enemies;
    public List<GameObject> doors;
    public bool teleporterRoom = false;
    public bool enemiesInRoom = false;
    void Start()
    {
        
    }

    void Update()
    {
        if (enemiesInRoom)
        {
            if (enemies.Count > 0)
            {
                try
                {
                    foreach (GameObject enemy in enemies)
                    {
                        /*if (!enemy.GetComponent<EnemyAsPartOfRoom>().GetAliveStatus())
                        {
                            Destroy(enemy, 2f);
                            enemies.Remove(enemy);
                        }
                        */
                        if (enemy.GetComponentInChildren<EnemyHealth>().dead)
                        {
                            enemies.Remove(enemy);
                        }
                    }
                }
                catch (System.Exception)
                {
                }

            }
            if (enemies.Count == 0)
            {
                OpenRoomDoors();
                if (teleporterRoom)
                {
                    GetComponentInChildren<BossTeleporter>().ActivateTeleporter();
                }
                enemiesInRoom = false;
                enemies.Clear();
            }
        }
    }

    public void AddEnemyToRoom(GameObject enemy)
    {
        enemies.Add(enemy);
        enemiesInRoom = true;
        
    }

    public void AddDoorToRoom(GameObject door)
    {
        doors.Add(door);
    }

    void OpenRoomDoors()
    {
        if (doors.Count > 0)
        {
            foreach (GameObject door in doors)
            {
                Destroy(door);
            }
        }
    }

    public void SetAsTeleporterRoom()
    {
        teleporterRoom = true;
    }
}

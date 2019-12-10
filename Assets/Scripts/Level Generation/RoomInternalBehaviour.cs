using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomInternalBehaviour : MonoBehaviour
{
    public List<GameObject> enemies;
    public List<GameObject> doors;

    public bool teleporterRoom = false;
    public bool enemiesInRoom = false;
    public bool bonusLevelRoom = false;

    private GameStats gameStats;
    void Start()
    {
        gameStats = GameObject.Find("Player").GetComponent<GameStats>();
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
                gameStats.AddClearedRoom();
                if (teleporterRoom)
                {
                    GetComponentInChildren<BossTeleporter>().ActivateTeleporter();
                }
                if (bonusLevelRoom)
                {
                    //bonusLevelTeleporter.SetActive(true);
                    gameObject.transform.Find("BonusLevelTeleporter").gameObject.SetActive(true);
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

    public void SetAsBonusLevelRoom()
    {
        bonusLevelRoom = true;
    }

    public void TurnOnRoomLights()
    {
        gameObject.transform.BroadcastMessage("LightOn");
    }
}

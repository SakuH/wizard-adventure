using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameStatsData
{
    public int maxLevel;
    public int roomsCleared;
    public int damageDone;
    public int enemiesDefeated;

    public GameStatsData(GameObject player)
    {
        maxLevel = 0;
        GameStatsData data = SaveSystem.LoadStats();
        if(data != null)
        {
            maxLevel = SaveSystem.LoadStats().maxLevel;
        }
        
        if (maxLevel < player.GetComponent<PlayerMovement>().currentTowerFloor)
        {
            maxLevel = player.GetComponent<PlayerMovement>().currentTowerFloor;
        }
        roomsCleared = player.GetComponent<GameStats>().roomsCleared;
        damageDone = player.GetComponent<GameStats>().damageDone;
        enemiesDefeated = player.GetComponent<GameStats>().enemiesDefeated;


    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStats : MonoBehaviour
{
    public int roomsCleared;
    public int damageDone;
    public int enemiesDefeated;
    
    void Start()
    {
        roomsCleared = 0;
        damageDone = 0;
        enemiesDefeated = 0;

        GameStatsData data = SaveSystem.LoadStats();
        if(data == null)
        {
            SaveSystem.SaveGameStats(gameObject);
        }
        else
        {
            roomsCleared = data.roomsCleared;
            damageDone = data.damageDone;
            enemiesDefeated = data.enemiesDefeated;
        }
    }

    public void SaveStats()
    {
        SaveSystem.SaveGameStats(gameObject);
    }

    public void AddDamageDone(int damage)
    {
        damageDone = damageDone + damage;
    }

    public void AddEnemyDefeated()
    {
        enemiesDefeated++;
    }

    public void AddClearedRoom()
    {
        roomsCleared++;
        SaveStats();
    }
}

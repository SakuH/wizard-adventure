using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StatisticsScreen : MonoBehaviour
{
    public TextMeshProUGUI maxLevelText;
    public TextMeshProUGUI roomsClearedText;
    public TextMeshProUGUI damageDoneText;
    public TextMeshProUGUI enemiesDefeatedText;

    void Start()
    {
        LoadGameStats();
    }



    void LoadGameStats()
    {
        GameStatsData data = SaveSystem.LoadStats();
        if(data != null)
        {
            maxLevelText.SetText(data.maxLevel.ToString());
            roomsClearedText.SetText(data.roomsCleared.ToString());
            damageDoneText.SetText(data.damageDone.ToString());
            enemiesDefeatedText.SetText(data.enemiesDefeated.ToString());
        }
    }
}

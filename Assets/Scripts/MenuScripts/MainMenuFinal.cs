using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class MainMenuFinal : MonoBehaviour
{
    public GameObject continueButton;
    public GameObject statsButton;

    private void Start()
    {
        continueButton.SetActive(false);
        PlayerData pData = SaveSystem.LoadPlayer();
        if(pData != null)
        {
            continueButton.SetActive(true);
        }
        GameStatsData gsData = SaveSystem.LoadStats();
        if (gsData != null)
        {
            statsButton.SetActive(true);
        }

    }

    public void ContinueGame()
    {
        PlayerData pData = SaveSystem.LoadPlayer();
        SceneManager.LoadScene(pData.currentFloor);
    }

    public void StartNewGame()
    {
        SaveSystem.DeletePlayer();
        SceneManager.LoadScene(5);
    }
    public void Quitgame()
    {
        Application.Quit();
    }
}

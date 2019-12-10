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
        SettingsData settingsData = SaveSystem.LoadSettings();
        if (settingsData != null)
        {
            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioSource>().volume = settingsData.bgm;
        }

    }

    public void ContinueGame()
    {
        PlayerData pData = SaveSystem.LoadPlayer();
        GameObject.Find("LevelChanger").GetComponent<LevelChanger>().FadeToScene(pData.currentFloor);
    }

    public void StartNewGame()
    {
        SaveSystem.DeletePlayer();
        GameObject.Find("LevelChanger").GetComponent<LevelChanger>().FadeToScene("IntroScene");

    }
    public void Quitgame()
    {
        Application.Quit();
    }
}

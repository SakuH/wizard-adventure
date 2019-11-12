using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class gameManagement : MonoBehaviour
{
    public static bool GameIsPaused = false;
    bool gameOver = false;
    public float resetDelay = 2f;

    public GameObject pauseMenu;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)){
           if (GameIsPaused){
                resume();
            }else
            {
                pause();
            }
        }
    }
  public void endGame()
    {
        if(gameOver== false)
        {
            
            gameOver = true;          
            Invoke("restart", resetDelay);
        }
    }

    public void restart()
    { Time.timeScale = 1f;
        SceneManager.LoadScene(0);
       
    }

    public void resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }
    void pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }
    public void Quit()
    {
        Application.Quit();
    }
    
}

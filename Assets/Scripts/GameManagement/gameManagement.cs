using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class gameManagement : MonoBehaviour
{
    bool gameOver = false;
    
    public float resetDelay = 2f;
  public void endGame()
    {
        if(gameOver== false)
        {
            
            gameOver = true;          
            Invoke("restart", resetDelay);
        }
    }
    void restart()
    {
        SceneManager.LoadScene(0);
    }
}

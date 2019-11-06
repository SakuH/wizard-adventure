using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public bool isStart;
    public bool isQuit;
    public bool isOptions;
    public bool isStartText;
    public bool isQuitText;
    public bool isOptionsText;
    public Renderer textRenderer;


    
    void Start()
    {
        GetComponent<TextMesh>().color = Color.white;
    }

    void Update()
    {
        
    }
    void OnMouseEnter()
    {
        GetComponent<TextMesh>().color = Color.grey;
        if (isQuitText)
        {
            isQuit = true;
        }
        if (isOptionsText)
        {
            isOptions = true;
        }
        if (isStartText)
        {
            isStart = true;
        }
    }

    void OnMouseExit()
    {
        GetComponent<TextMesh>().color = Color.white;
        if (isQuitText)
        {
            isQuit = false;
        }
        if (isOptionsText)
        {
            isOptions = false;
        }
        if (isStartText)
        {
            isStart = false;
        }



    }

    void OnMouseUp()
    {
        if (isStart)
        {
            SceneManager.LoadScene(1);
            
        }
        if (isQuit)
        {
            Application.Quit();
           
        }
        if (isOptions)
        {

        }
    }
}

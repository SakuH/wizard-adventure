using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public bool isStart;
    public bool isQuit;
    public bool isOptions;

    public bool isResolution;
    public bool isFullScreen;
    public bool isSound;
    public bool isBGMt;
    public bool isBack;

    public bool isStartText;
    public bool isQuitText;
    public bool isOptionsText;

    public bool isResolutionText;
    public bool isFullScreenText;
    public bool isSoundText;
    public bool isBGMText;
    public bool isBackText;

    public Camera mainCam;
    private float cameraPositionOptions;
    private float cameraPositionDefault;
    
    void Start()
    {
       

        cameraPositionDefault = -1.32f;
        cameraPositionOptions = 33.41f;
    GetComponent<TextMesh>().color = Color.white;
        if(mainCam != null)
        mainCam.transform.position = new Vector3(cameraPositionDefault, 15.0f, 0.52f);

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
        if (isResolutionText)
        {
            isResolution = true;
        }
        if (isFullScreenText)
        {
           isFullScreen = true;
        }
        if (isBackText)
        {
           isBack = true;
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
        if (isResolutionText)
        {
            isResolution =false;
        }
        if (isFullScreenText)
        {
            isFullScreen = false;
        }
        if (isBackText)
        {
            isBack = false;
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
            if (mainCam != null)
            {
                
                
                   
                    mainCam.transform.position = new Vector3(cameraPositionOptions, 15.0f, 0.52f);

                
            }
        }
        if (isBackText)
        {
            mainCam.transform.position = new Vector3(cameraPositionDefault, 15.0f, 0.52f);

        }
        if (isFullScreenText)
        {
            if (Screen.fullScreen)
            {
                setFullscreen(false);
                Debug.Log("windowed");
            }
            else
            {
                setFullscreen(true);
                Debug.Log("Fullscreen");
            }
        }
        if (isResolutionText)
        {
           
        }
    }

    public void setFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }
}

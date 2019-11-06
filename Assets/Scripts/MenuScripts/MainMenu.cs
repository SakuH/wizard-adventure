using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;
using UnityEngine.UI;
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

    Resolution[] resolutions;
    public GameObject resolution;
    public GameObject fullScreen;

    public Dropdown resolutionDropdown;
    TextMesh resolutionMesh;
    TextMesh fullScreenMesh;

    public bool hideDropdown = true;



    void Start()
    {
        
        if (isResolutionText)
        {
            resolutionMesh =resolution.GetComponent<TextMesh>();
            resolutionMesh.text = "Resolution<" + Screen.width + " x "+Screen.height+">";
        }
        if (isFullScreenText)
        {
            fullScreenMesh = fullScreen.GetComponent<TextMesh>();
            if (Screen.fullScreen)
            {
                
                fullScreenMesh.text = "Fullscreen";           
            }else
            {
                fullScreenMesh.text = "windowed";

            }

        }

        
       
        
        cameraPositionDefault = -1.32f;
        cameraPositionOptions = 33.41f;
    GetComponent<TextMesh>().color = Color.white;
        if(mainCam != null)
        mainCam.transform.position = new Vector3(cameraPositionDefault, 15.0f, 0.52f);

        if (isResolutionText)
        {
            resolutions = Screen.resolutions;

            resolutionDropdown.gameObject.SetActive(false);

            resolutionDropdown.ClearOptions();


            int currentResolutionIndex = 0;

            List<string> options = new List<string>();

            for (int i = 0; i < resolutions.Length; i++)
            {
                string option = resolutions[i].width + " x " + resolutions[i].height;
                options.Add(option);

                if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
                {
                    currentResolutionIndex = i;
                }
            }



            resolutionDropdown.AddOptions(options);
            resolutionDropdown.value = currentResolutionIndex;
            resolutionDropdown.RefreshShownValue();

        }
       
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
            if (isResolutionText)
            {
                

            }
            if (mainCam != null)
            {             
                
                mainCam.transform.position = new Vector3(cameraPositionOptions, 15.0f, 0.52f);
                resolutionDropdown.gameObject.SetActive(true);
            }
        }
        if (isBackText)
        {
            mainCam.transform.position = new Vector3(cameraPositionDefault, 15.0f, 0.52f);
            resolutionDropdown.gameObject.SetActive(false);

        }
        if (isFullScreenText)
        {
            if (Screen.fullScreen)
            {
                setFullscreen(false);
                fullScreenMesh.text = "Windowed";
                Debug.Log("windowed");
            }
            else
            {
                setFullscreen(true);
                fullScreenMesh.text = "Fullscreen";

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
    public void setResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
}

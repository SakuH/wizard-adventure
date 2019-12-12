using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class FadeToBlackEnding : MonoBehaviour
{
    public bool fadeToBlack= false;
    public Image backgroundColor;
    public float fadeToBlackColor;
    public GameObject endingText;
    public float timeToText;
    public float timeToTextmax = 2;
    public bool saveDeleted = false;

    void Start()
    {
        timeToText = timeToTextmax;
    }

    void Update()
    {
        if (!saveDeleted)
        {
            SaveSystem.DeletePlayer();
            saveDeleted = true;
        }
        
        if (fadeToBlack)
        {
            fadeToBlackCanvas();
            if (timeToText > 0)
            {
                timeToText -= Time.deltaTime;
            }
            else
            {
                endingText.SetActive(true);
            }
            if (Input.GetKeyDown("space"))
            {

                SceneManager.LoadScene("MainMenuFinal");
            }
        }
       
    }
    public void fadeToBlackCanvas()
    {
        var tempColor = backgroundColor.color;

        if (fadeToBlackColor < 1)
        {
            fadeToBlackColor += Time.deltaTime;
        }

        tempColor.a = fadeToBlackColor;
        backgroundColor.color = tempColor;

    }
    public void callFadeToBlack()
    {
        fadeToBlack = true;
    }
     
    }

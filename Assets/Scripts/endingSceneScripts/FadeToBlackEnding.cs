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

    void Start()
    {
        timeToText = timeToTextmax;
    }

    // Update is called once per frame
    void Update()
    {
        if (fadeToBlack)
        {
            fadeToBlackCanvas();
            if (timeToText > 0)
            {
                timeToText -= Time.deltaTime;
            }
            else
            {
                endingText.active = true;
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

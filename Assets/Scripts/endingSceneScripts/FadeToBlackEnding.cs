using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class FadeToBlackEnding : MonoBehaviour
{
    public bool fadeToBlack= false;
    public Image backgroundColor;
    public float fadeToBlackColor;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (fadeToBlack)
        {
            fadeToBlackCanvas();
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

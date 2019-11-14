using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SettingsData
{
    public bool fullScreen;
    public int[] resolution;
    public float bgm;
    public float sfx;

    public SettingsData(Settings settings)
    {
        fullScreen = settings.fullScreen;
        resolution[0] = settings.currentResolution.width;
        resolution[1] = settings.currentResolution.height;
        bgm = settings.currentbgm;
        sfx = settings.currentsfx;
       
    }
}

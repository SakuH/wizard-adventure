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
       
    }
}

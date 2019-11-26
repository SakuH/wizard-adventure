using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAudioSettings : MonoBehaviour
{

    public float bgmVolume = 1.0f;
    public float sfxVolume = 1.0f;

    private void Awake()
    {
        SettingsData settingsData;
        settingsData = SaveSystem.LoadSettings();
        if(settingsData != null)
        {
            bgmVolume = settingsData.bgm;
            sfxVolume = settingsData.sfx;
        }
    }


}

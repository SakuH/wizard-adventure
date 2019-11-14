using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Settings : MonoBehaviour
{
    Resolution[] resolutions;
    public Dropdown resolutionDropdown;

    public bool fullScreen;
    public Resolution currentResolution;
    public float currentbgm = 0.5f;
    public float currentsfx = 0.5f;
    public Slider bgmSlider;
    public Slider sfxSlider;
    void Start()
    {
        LoadSettings();

        resolutions = Screen.resolutions;

        resolutionDropdown.ClearOptions();


        int currentResolutionIndex = 0;

        List<string> options = new List<string>();

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

           // if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
                if (resolutions[i].width == Screen.width && resolutions[i].height == Screen.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();

    }
    public void setFullscreen()
    {
        if(Screen.fullScreen == false)
        {
            Screen.fullScreen = true;
        }
    }
    public void setWindowed()
    {
        if (Screen.fullScreen == true)
        {
            Screen.fullScreen = false;
        }
    }
    public void setResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        currentResolution = resolution;
    }

    public void SetBackgroundMusic(float value)
    {
        currentbgm = value;
    }

    public void SetSoundEffects(float value)
    {
        currentsfx = value;
    }

    public void SaveSettings()
    {
        this.fullScreen = Screen.fullScreen;
        SaveSystem.SaveSettings(this);
    }

    public void LoadSettings()
    {
        SettingsData data = SaveSystem.LoadSettings();
        if (data != null)
        {
            currentResolution.width = data.resolution[0];
            currentResolution.height = data.resolution[1];
            this.fullScreen = data.fullScreen;
            this.currentbgm = data.bgm;
            this.currentsfx = data.sfx;

            Screen.SetResolution(currentResolution.width, currentResolution.height, fullScreen);
            this.bgmSlider.value = data.bgm;
            this.sfxSlider.value = data.sfx;
        }
    }
}

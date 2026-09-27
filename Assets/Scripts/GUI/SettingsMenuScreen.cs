using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.Audio;

public class SettingsMenuScreen : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private TMP_Dropdown resolutionsDropdown;
    [SerializeField] private GameObject settingsMenu;
    Resolution[] resolutions;

    void Start()
    {
        resolutions = Screen.resolutions;

        resolutionsDropdown.ClearOptions();
        List<string> options = new List<string>();

        int currentResolutionIndex = 0;
        for (int i = 0; i< resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);
            if(resolutions[i].height == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }
        resolutionsDropdown.AddOptions(options);
        resolutionsDropdown.value = currentResolutionIndex;
        resolutionsDropdown.RefreshShownValue();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            initDebugScreen();
        }
    }
    private void initDebugScreen()
    {
        if(settingsMenu.activeSelf == false)
        {
            settingsMenu.SetActive(true);
            Time.timeScale = 0;
        }
        else if (settingsMenu.activeSelf == true)
        {
            settingsMenu.SetActive(false);
            Time.timeScale = 1;
        }
    }
    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Debug.Log("Recieved resolution: " +resolution.width + " x " + resolution.height);
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
    public void SetVolume(float volume)
    {
        Debug.Log("Volume: " + volume);
        audioMixer.SetFloat("volume", volume);
    }
    public void SetQuality(int qualityIndex)
    {
        Debug.Log("Recieved quality: " + qualityIndex);
        QualitySettings.SetQualityLevel(qualityIndex);
    }
    public void SetFullscreen(bool isFullscreen)
    {
        Debug.Log("Fullscreen: " + isFullscreen);
        Screen.fullScreen = isFullscreen;
    }
}

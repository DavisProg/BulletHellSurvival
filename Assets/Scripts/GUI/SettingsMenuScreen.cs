using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using System.Linq;

public class SettingsMenuScreen : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private TMP_Dropdown resolutionsDropdown;
    [SerializeField] private TMP_Dropdown graphicsDropdown;
    [SerializeField] private GameObject settingsMenu;
    [SerializeField] private Toggle fullscreenToggle;
    [SerializeField] private Slider volumeSlider;
    string path;
    private GraphicsSettings settings;
    GraphicsSettings defaultSettings = new GraphicsSettings();
    Resolution[] resolutions;
    void Start()
    {
        path = Path.Combine(Application.persistentDataPath, "text.txt");

        defaultSettings.resolutionWidth = 1920;
        defaultSettings.resolutionHeight = 1080;
        defaultSettings.fullscreen = true;
        defaultSettings.quality = 5;
        defaultSettings.volume = 0;

        if(settings == null)
        {
            settings = new GraphicsSettings();
        }
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

        LoadSettings();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            InitSettingsScreen();
        }
    }
    public void InitSettingsScreen()
    {
        if(settingsMenu.activeSelf == false)
        {
            settingsMenu.SetActive(true);
            Time.timeScale = 0;
        }
        else if (settingsMenu.activeSelf == true)
        {
            settingsMenu.SetActive(false);
            SaveSettings();
            Time.timeScale = 1;
        }
    }
    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Debug.Log("Recieved resolution: " +resolution.width + " x " + resolution.height);
        
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        settings.resolutionWidth = resolution.width;
        settings.resolutionHeight = resolution.height;
    }
    public void SetVolume(float volume)
    {
        Debug.Log("Volume: " + volume);
        audioMixer.SetFloat("volume", volume);
        settings.volume = volume;
        
    }
    public void SetQuality(int qualityIndex)
    {
        Debug.Log("Recieved quality: " + qualityIndex);
        QualitySettings.SetQualityLevel(qualityIndex);
        settings.quality = qualityIndex;
    }
    public void SetFullscreen(bool isFullscreen)
    {
        Debug.Log("Fullscreen: " + isFullscreen);
        Screen.fullScreen = isFullscreen;
        settings.fullscreen = isFullscreen;
    }
    public class GraphicsSettings
    {
        public int resolutionWidth;
        public int resolutionHeight;
        public bool fullscreen;
        public int quality;
        public float volume;

    }
    public void SaveSettings()
    {
        if(settings == null)
        {
            settings = new GraphicsSettings();
        }
        if(settings.resolutionWidth == 0 || settings.resolutionHeight == 0)
        {
            settings = defaultSettings;
        }
        string settingsData = JsonUtility.ToJson(settings);
        Debug.Log(Application.persistentDataPath);
        File.WriteAllText(path, settingsData);
    }
    public void LoadSettings()
    {
        if (!File.Exists(path))
        {
            settings = defaultSettings;
            setValues();
            return;
        }

        string settingsData = File.ReadAllText(path);
        try
        {
            settings = JsonUtility.FromJson<GraphicsSettings>(settingsData);
            if(settings.resolutionWidth <= 0 || settings.resolutionHeight <= 0 || settings.volume > 0 || settings.volume < -80)
            {
                settings = defaultSettings;
            }
            setValues();
        }
        catch
        {
            settings = defaultSettings;
            setValues();
        }      
    }       
    private void displayValues()
    {
        int dropdownIndex = 0;
        for(int i = 0; i < resolutions.Count(); i++)
        {
            if(resolutions[i].height == settings.resolutionHeight && resolutions[i].width == settings.resolutionWidth)
            {
                dropdownIndex = i;
            }
        }
        resolutionsDropdown.value = dropdownIndex;
        resolutionsDropdown.RefreshShownValue();

        graphicsDropdown.value = settings.quality;
        graphicsDropdown.RefreshShownValue();

        fullscreenToggle.isOn = settings.fullscreen;
        volumeSlider.value = settings.volume;
    }
    private void setValues()
    {
        SetFullscreen(settings.fullscreen);
        SetQuality(settings.quality);
        SetVolume(settings.volume);
        Screen.SetResolution(settings.resolutionWidth, settings.resolutionHeight, Screen.fullScreen);
        displayValues();
    }
}

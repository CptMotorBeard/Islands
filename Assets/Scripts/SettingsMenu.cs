using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour {

    public Dropdown resolutionDropdown;
    Resolution[] resolutions;

    void Start ()
    {
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();

        List<string> StringResolutions = new List<string>();

        int currentResolutionIndex = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            StringResolutions.Add(option);

            if (resolutions[i].height == Screen.currentResolution.height && resolutions[i].width == Screen.currentResolution.width)
                currentResolutionIndex = i;
        }

        resolutionDropdown.AddOptions(StringResolutions);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    public void SetQuality(int index)
    {
        QualitySettings.SetQualityLevel(index);
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    public void SetResolution(int index)
    {
        Resolution r = resolutions[index];
        Screen.SetResolution(r.width, r.height, Screen.fullScreen);
    }
}

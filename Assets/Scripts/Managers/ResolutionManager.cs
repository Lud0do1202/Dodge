using System.Collections.Generic;
using UnityEngine;

public class ResolutionManager : MonoBehaviour
{
    [HideInInspector]
    public Resolution[] resolutions;
    [HideInInspector]
    public List<Dictionary<string, int>> resolutionsAccepted;

    // Singleton
    public static ResolutionManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than one instance of ResolutionManager");
            return;
        }
        instance = this;

        // Resolutions accepted
        resolutionsAccepted = new List<Dictionary<string, int>> {
            new Dictionary<string, int>() { { "width", 720 }, { "height", 480 } },
            new Dictionary<string, int>() { { "width", 1080 }, { "height", 720 } },
            new Dictionary<string, int>() { { "width", 1920 }, { "height", 1080 } },
            new Dictionary<string, int>() { { "width", 4096 }, { "height", 2304 } }
        };

        resolutions = Screen.resolutions;

        // Fulscreen
        SetFullscreen(GetFullscreen());

        // Set default reso
        int indexResoSelected = GetResolutionSelected();
        SetResolution(
                resolutionsAccepted[indexResoSelected]["width"],
                resolutionsAccepted[indexResoSelected]["height"],
                indexResoSelected
            );
    }

    public int GetResolutionSelected()
    {
        return PlayerPrefs.GetInt("indexReso", GetLastResolutionSupported());
    }

    public int GetLastResolutionSupported()
    {
        int lastIndexResolution = 0;

        for (int i = 1; i < resolutionsAccepted.Count; i++)
        {
            if (resolutionsAccepted[i]["width"] > resolutions[resolutions.Length - 1].width
                || resolutionsAccepted[i]["height"] > resolutions[resolutions.Length - 1].height)
                break;

            lastIndexResolution++;
        }

        return lastIndexResolution;
    }

    public void SetResolution(int width, int height, int indexReso)
    {
        Screen.SetResolution(width, height, Screen.fullScreen);
        PlayerPrefs.SetInt("indexReso", indexReso);
    }

    public int GetFullscreen()
    {
        return PlayerPrefs.GetInt("isFullscreen", 1);
    }

    public void SetFullscreen(bool fullscreen)
    {
        Screen.fullScreen = fullscreen;
        PlayerPrefs.SetInt("isFullscreen", fullscreen?1:0);
    }

    public void SetFullscreen(int fullscreen)
    {
        Screen.fullScreen = fullscreen == 1 ? true : false;
        PlayerPrefs.SetInt("isFullscreen", fullscreen);
    }
}

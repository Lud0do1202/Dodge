using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Tilemaps;

public class OptionsManager : MonoBehaviour
{
    // Music
    public GameObject[] musicLevels;
    private int indexMusicLevel;

    // Sounds
    public GameObject[] soundsLevels;
    private int indexSoundsLevel;

    // Resolutions
    private Resolution[] resolutions;
    private List<Dictionary<string, int>> resolutionsAccepted;
    private int indexLastReso;

    public GameObject fullscreenSelected;

    public Tilemap[] resoTilemap;
    public GameObject[] resoButton;
    public GameObject[] resoSelected;

    // Singleton
    public static OptionsManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than one instance of OptionsManager");
            return;
        }
        instance = this;

        // get the indexMusic/SoundsLevel
        indexMusicLevel = PlayerPrefs.GetInt("indexMusicLevel", 9);
        indexSoundsLevel = PlayerPrefs.GetInt("indexSoundsLevel", 9);

        // Résolutions
        resolutionsAccepted = new List<Dictionary<string, int>>()
        {
            new Dictionary<string, int>() { { "width", 720 }, { "height", 480 } },
            new Dictionary<string, int>() { { "width", 1080 }, { "height", 720 } },
            new Dictionary<string, int>() { { "width", 1920 }, { "height", 1080 } },
            new Dictionary<string, int>() { { "width", 4096 }, { "height", 2304 } }
        };

        resolutions = Screen.resolutions;
    }

    private void Start()
    {
        // Activer le bon music level
        musicLevels[indexMusicLevel].SetActive(true);
        AudioManager.instance.SetMusicVolume(indexMusicLevel);

        soundsLevels[indexSoundsLevel].SetActive(true);
        AudioManager.instance.SetSoundsVolume(indexSoundsLevel);

        // Afficher les Résolutions
        ChangeFullscreen((PlayerPrefs.GetInt("isFullscreen", 1) == 1) ? true : false);
        SelectValidResolutions();
        int defaultWidth = resolutionsAccepted[indexLastReso]["width"],
            defaultHeight = resolutionsAccepted[indexLastReso]["height"];
        ChangeResolution(PlayerPrefs.GetInt("widthReso", defaultWidth), PlayerPrefs.GetInt("heightReso", defaultHeight), PlayerPrefs.GetInt("indexReso", indexLastReso));
    }

    // Hightlight the button
    public void HightLighted(GameObject gameObject)
    {
        gameObject.GetComponent<Animator>().SetTrigger("Hightlighted");
    }
    public void NotHightLighted(GameObject gameObject)
    {
        gameObject.GetComponent<Animator>().SetTrigger("NotHightlighted");
    }

    // Reduce / increase the volume of the music
    public void ReduceMusicLevel()
    {
        if (indexMusicLevel >= 0)
        {
            // Desable the last musicLevel
            musicLevels[indexMusicLevel].SetActive(false);
            indexMusicLevel--;

            // Save the new indexMusicLevel
            PlayerPrefs.SetInt("indexMusicLevel", indexMusicLevel);

            // Reduce the musicVolume
            AudioManager.instance.SetMusicVolume(indexMusicLevel);

            // Enable the new musicLevel
            if (indexMusicLevel > -1)
                musicLevels[indexMusicLevel].SetActive(true);
        }
    }
    public void IncreaseMusicLevel()
    {
        if (indexMusicLevel < musicLevels.Length - 1)
        {
            // Desable the last musicLevel
            if (indexMusicLevel > -1)
                musicLevels[indexMusicLevel].SetActive(false);

            // Enable the new musicLevel
            indexMusicLevel++;
            musicLevels[indexMusicLevel].SetActive(true);

            // Save the new indexMusicLevel
            PlayerPrefs.SetInt("indexMusicLevel", indexMusicLevel);

            // Increase the musicVolume
            AudioManager.instance.SetMusicVolume(indexMusicLevel);
        }
    }

    // Reduce / increase the volume of the sounds
    public void ReduceSoundsLevel()
    {
        if (indexSoundsLevel >= 0)
        {
            // Desable the last musicLevel
            soundsLevels[indexSoundsLevel].SetActive(false);
            indexSoundsLevel--;

            // Save the new indexMusicLevel
            PlayerPrefs.SetInt("indexSoundsLevel", indexSoundsLevel);

            // Reduce the musicVolume
            AudioManager.instance.SetSoundsVolume(indexSoundsLevel);

            // Enable the new musicLevel
            if (indexSoundsLevel > -1)
                soundsLevels[indexSoundsLevel].SetActive(true);
        }
    }
    public void IncreaseSoundsLevel()
    {
        if (indexSoundsLevel < soundsLevels.Length - 1)
        {
            // Desable the last musicLevel
            if (indexSoundsLevel > -1)
                soundsLevels[indexSoundsLevel].SetActive(false);

            // Enable the new musicLevel
            indexSoundsLevel++;
            soundsLevels[indexSoundsLevel].SetActive(true);

            // Save the new indexMusicLevel
            PlayerPrefs.SetInt("indexSoundsLevel", indexSoundsLevel);

            // Increase the musicVolume
            AudioManager.instance.SetSoundsVolume(indexSoundsLevel);
        }
    }

    // Resolutions
    public void SelectValidResolutions()
    {
        indexLastReso = resolutionsAccepted.Count - 1;
        for (int i = 0; i < resolutionsAccepted.Count; i++)
        {
            // La résolution accepté est trop grande en x ou y
            if (resolutionsAccepted[i]["width"] > resolutions[resolutions.Length - 1].width
                || resolutionsAccepted[i]["height"] > resolutions[resolutions.Length - 1].height)
            {
                // Désactiver la résolution
                resoButton[i].GetComponent<BoxCollider2D>().enabled = false;
                resoButton[i].GetComponent<Light2D>().color = new Color(0, 0.2f, 0);
                resoTilemap[i].color = new Color(0, 0.2f, 0);

                if (indexLastReso == resolutionsAccepted.Count - 1)
                    indexLastReso = i - 1;
            }
        }
    }

    public void ChangeResolution(int width, int height, int indexReso)
    {
        // Modifier la résolution
        Screen.SetResolution(width, height, Screen.fullScreen);

        // Désactiver l'ancienne réso selectionnnée et activer la nouvelle
        resoSelected[PlayerPrefs.GetInt("indexReso", 0)].SetActive(false);
        resoSelected[indexReso].SetActive(true);

        // Enregistrer les infos
        PlayerPrefs.SetInt("widthReso", width);
        PlayerPrefs.SetInt("heightReso", height);
        PlayerPrefs.SetInt("indexReso", indexReso);
    }

    public void ChangeFullscreen()
    {
        // Si il est désactivé -> on active le fullscreen
        if (PlayerPrefs.GetInt("isFullscreen", 0) == 0)
        {
            Screen.fullScreen = true;
            fullscreenSelected.SetActive(true);
            PlayerPrefs.SetInt("isFullscreen", 1);
        }
        else
        {
            Screen.fullScreen = false;
            fullscreenSelected.SetActive(false);
            PlayerPrefs.SetInt("isFullscreen", 0);
        }
    }
    public void ChangeFullscreen(bool isFullscreen)
    {
        // Si il est désactivé -> on active le fullscreen
        if (isFullscreen)
        {
            Screen.fullScreen = true;
            fullscreenSelected.SetActive(true);
            PlayerPrefs.SetInt("isFullscreen", 1);
        }
        else
        {
            Screen.fullScreen = false;
            fullscreenSelected.SetActive(false);
            PlayerPrefs.SetInt("isFullscreen", 0);
        }
    }
}

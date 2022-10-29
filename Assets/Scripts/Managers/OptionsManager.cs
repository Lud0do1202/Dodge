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
    private Color notSupportedResoColor;

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

        // Color of a resolution no
        notSupportedResoColor = new Color(0, 0.2f, 0);
    }

    private void Start()
    {
        // get the indexMusic/SoundsLevel
        indexMusicLevel = AudioManager.instance.GetIndexMusicLevel();
        indexSoundsLevel = AudioManager.instance.GetIndexSoundsLevel();

        // Activer le bon music level
        musicLevels[indexMusicLevel].SetActive(true);
        soundsLevels[indexSoundsLevel].SetActive(true);

        // Afficher les Résolutions
        DisplaySupportedResolutions();

        // Fullscreen
        ResolutionManager.instance.SetFullscreen(ResolutionManager.instance.GetFullscreen() == 1);
        fullscreenSelected.SetActive(ResolutionManager.instance.GetFullscreen() == 1);
    }

    // Reduce / increase the volume of the music
    public void ReduceMusicLevel()
    {
        if (indexMusicLevel >= 0)
        {
            // Desable the last musicLevel
            musicLevels[indexMusicLevel].SetActive(false);
            indexMusicLevel--;

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

            // Increase the musicVolume
            AudioManager.instance.SetSoundsVolume(indexSoundsLevel);
        }
    }

    // Resolutions
    public void DisplaySupportedResolutions()
    {
        // Activer Selected 
        resoSelected[ResolutionManager.instance.GetResolutionSelected()].SetActive(true);

        for (int i = ResolutionManager.instance.GetLastResolutionSupported() + 1; i < ResolutionManager.instance.resolutionsAccepted.Count; i++)
        {
            // Désactiver la résolution
            resoButton[i].GetComponent<BoxCollider2D>().enabled = false;
            resoButton[i].GetComponent<Light2D>().color = notSupportedResoColor;
            resoTilemap[i].color = notSupportedResoColor;
        }
    }

    public void ChangeResolution(int width, int height, int indexReso)
    {
        // Désactiver l'ancienne réso selectionnnée et activer la nouvelle
        resoSelected[ResolutionManager.instance.GetResolutionSelected()].SetActive(false);
        resoSelected[indexReso].SetActive(true);

        // Modifier la résolution
        ResolutionManager.instance.SetResolution(width, height, indexReso);

    }

    public void ChangeFullscreen()
    {
        // Si il est désactivé -> on active le fullscreen
        if (ResolutionManager.instance.GetFullscreen() == 0)
        {
            ResolutionManager.instance.SetFullscreen(true);
            fullscreenSelected.SetActive(true);
        }
        else
        {
            ResolutionManager.instance.SetFullscreen(false);
            fullscreenSelected.SetActive(false);
        }
    }
}

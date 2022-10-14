using UnityEngine;

public class MenuManager : MonoBehaviour
{
    // Ref
    public GameObject helpWindow;

    public GameObject mainMenu;
    public GameObject optionsMenu;

    public Transform spawnMainMenu;
    public Transform spawnOptionsMenu;

    // Every Button
    [HideInInspector]
    public const int NO_BUTTON_SELECTED = 0,
        PLAY_BUTTON = 1,
        SELECT_LEVEL_BUTTON = 2,
        OPTIONS_BUTTON = 3,
        QUIT_BUTTON = 4,
        REDUCE_MUSIC_BUTTON = 5,
        INCREASE_MUSIC_BUTTON = 6,
        REDUCE_SOUNDS_BUTTON = 7,
        INCREASE_SOUNDS_BUTTON = 8;
    [HideInInspector]
    public int buttonSelected = 0;

    [HideInInspector]
    public string[] tagsString;
    [HideInInspector]
    public int[] tagsInt;

    // Music
    public GameObject[] musicLevels;
    private int indexMusicLevel;

    // Sounds
    public GameObject[] soundsLevels;
    private int indexSoundsLevel;

    // Singleton
    public static MenuManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than one instance of MenuManager");
            return;
        }
        instance = this;

        // fill var
        tagsString = new string[] {
            "PlayButton",
            "SelectLevelButton",
            "OptionsButton",
            "QuitButton",
            "ReduceMusicButton",
            "IncreaseMusicButton",
            "ReduceSoundsButton",
            "IncreaseSoundsButton"
        };

        tagsInt = new int[] {
            PLAY_BUTTON,
            SELECT_LEVEL_BUTTON,
            OPTIONS_BUTTON,
            QUIT_BUTTON,
            REDUCE_MUSIC_BUTTON,
            INCREASE_MUSIC_BUTTON,
            REDUCE_SOUNDS_BUTTON,
            INCREASE_SOUNDS_BUTTON
        };

        // Say if we have to show the help window
        if (PlayerPrefs.GetInt("ShowHelpWindow", 1) == 1)
            helpWindow.SetActive(true);
        else
            helpWindow.SetActive(false);

        // get the indexMusic/SoundsLevel
        indexMusicLevel = PlayerPrefs.GetInt("indexMusicLevel", 9);
        indexSoundsLevel = PlayerPrefs.GetInt("indexSoundsLevel", 9);
    }

    private void Start()
    {
        // Activer le bon music level
        musicLevels[indexMusicLevel].SetActive(true);
        AudioManager.instance.SetMusicVolume(indexMusicLevel);

        soundsLevels[indexSoundsLevel].SetActive(true);
        AudioManager.instance.SetSoundsVolume(indexSoundsLevel);
    }

    // Show / Close the help window
    public void ShowHelpWindow(bool toggle)
    {
        if (toggle)
            PlayerPrefs.SetInt("ShowHelpWindow", 0);
        else
            PlayerPrefs.SetInt("ShowHelpWindow", 1);
    }
    public void CloseHelpWindow()
    {
        helpWindow.SetActive(false);
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
}

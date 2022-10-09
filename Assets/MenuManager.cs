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
        INCREASE_MUSIC_BUTTON = 6;
    [HideInInspector]
    public int buttonSelected = 0;

    [HideInInspector]
    public string[] tagsString = new string[] {
        "PlayButton",
        "SelectLevelButton",
        "OptionsButton",
        "QuitButton",
        "ReduceMusicButton",
        "IncreaseMusicButton"
    };
    [HideInInspector]
    public int[] tagsInt = new int[] {
        PLAY_BUTTON,
        SELECT_LEVEL_BUTTON,
        OPTIONS_BUTTON,
        QUIT_BUTTON,
        REDUCE_MUSIC_BUTTON,
        INCREASE_MUSIC_BUTTON
    };

    // Music
    public GameObject[] musicLevels;
    private int indexMusicLevel = -1;

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

        // Say if we have to show the help window
        if (PlayerPrefs.GetInt("ShowHelpWindow", 1) == 1)
            helpWindow.SetActive(true);
        else
            helpWindow.SetActive(false);
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
            musicLevels[indexMusicLevel].SetActive(false);
            indexMusicLevel--;

            if (indexMusicLevel > -1)
                musicLevels[indexMusicLevel].SetActive(true);
        }
    }
    public void IncreaseMusicLevel()
    {
        if (indexMusicLevel < musicLevels.Length - 1)
        {
            if (indexMusicLevel > -1)
                musicLevels[indexMusicLevel].SetActive(false);

            indexMusicLevel++;
            musicLevels[indexMusicLevel].SetActive(true);
        }
    }
}

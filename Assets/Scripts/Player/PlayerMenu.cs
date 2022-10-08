using System;
using UnityEngine;

public class PlayerMenu : MonoBehaviour
{
    // Every Button
    private const int NO_BUTTON_SELECTED = 0,
        PLAY_BUTTON = 1,
        SELECT_LEVEL_BUTTON = 2,
        OPTIONS_BUTTON = 3,
        QUIT_BUTTON = 4,
        REDUCE_MUSIC_BUTTON = 5,
        INCREASE_MUSIC_BUTTON = 6;
    private int buttonSelected = 0;

    private string[] tagsString = new string[] {
        "PlayButton",
        "SelectLevelButton",
        "OptionsButton",
        "QuitButton",
        "ReduceMusicButton",
        "IncreaseMusicButton"
    };
    private int[] tagsInt = new int[] {
        PLAY_BUTTON,
        SELECT_LEVEL_BUTTON,
        OPTIONS_BUTTON,
        QUIT_BUTTON,
        REDUCE_MUSIC_BUTTON,
        INCREASE_MUSIC_BUTTON
    };

    // Options Button
    public GameObject mainMenu;
    public GameObject optionsMenu;

    public Transform spawnMainMenu;
    public Transform spawnOptionsMenu;

    // Music
    public GameObject[] musicLevels;
    private int indexMusicLevel = -1;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return) && buttonSelected != 0)
        {
            switch (buttonSelected)
            {
                case PLAY_BUTTON:
                    GameManager.instance.LoadSceneWithName(CurrentSceneManager.instance.nextSceneToLoad);
                    break;

                case SELECT_LEVEL_BUTTON:
                    GameManager.instance.LoadSceneWithName("Select Level Menu");
                    break;

                case OPTIONS_BUTTON:
                    mainMenu.SetActive(false);
                    optionsMenu.SetActive(true);
                    transform.position = spawnOptionsMenu.position;
                    break;

                case QUIT_BUTTON:
                    GameManager.instance.Quit();
                    break;

                case REDUCE_MUSIC_BUTTON:
                    ReduceMusicLevel();
                    break;

                case INCREASE_MUSIC_BUTTON:
                    IncreaseMusicLevel();
                    break;

                default:
                    break;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Comparer tous les tags
        for(int i = 0; i<tagsString.Length; i++)
        {
            if (collision.CompareTag(tagsString[i]))
            {
                HightLighted(collision.gameObject);
                buttonSelected = tagsInt[i];
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // Comparer tous les tags
        for (int i = 0; i < tagsString.Length; i++)
        {
            if (collision.CompareTag(tagsString[i]))
            {
                NotHightLighted(collision.gameObject);
                buttonSelected = NO_BUTTON_SELECTED;
            }
        }
    }

    private void HightLighted(GameObject gameObject)
    {
        gameObject.GetComponent<Animator>().SetTrigger("Hightlighted");
    }
    private void NotHightLighted(GameObject gameObject)
    {
        gameObject.GetComponent<Animator>().SetTrigger("NotHightlighted");
    }

    private void ReduceMusicLevel()
    {
        if (indexMusicLevel >= 0)
        {
            musicLevels[indexMusicLevel].SetActive(false);
            indexMusicLevel--;

            if(indexMusicLevel > -1)
                musicLevels[indexMusicLevel].SetActive(true);
        }
    }
    private void IncreaseMusicLevel()
    {
        if (indexMusicLevel < musicLevels.Length - 1)
        {
            if(indexMusicLevel > -1)
                musicLevels[indexMusicLevel].SetActive(false);

            indexMusicLevel++;
            musicLevels[indexMusicLevel].SetActive(true);
        }
    }
}

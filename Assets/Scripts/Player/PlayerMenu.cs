using UnityEngine;

public class PlayerMenu : MonoBehaviour
{
    private MenuManager menuManager;

    private void Start()
    {
        menuManager = MenuManager.instance;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return) && menuManager.buttonSelected != MenuManager.NO_BUTTON_SELECTED)
        {
            switch (menuManager.buttonSelected)
            {
                case MenuManager.PLAY_BUTTON:
                    GameManager.instance.LoadSceneWithName(CurrentSceneManager.instance.nextSceneToLoad);
                    break;

                case MenuManager.SELECT_LEVEL_BUTTON:
                    GameManager.instance.LoadSceneWithName("Select Level Menu");
                    break;

                case MenuManager.OPTIONS_BUTTON:
                    menuManager.mainMenu.SetActive(false);
                    menuManager.optionsMenu.SetActive(true);
                    transform.position = menuManager.spawnOptionsMenu.position;
                    break;

                case MenuManager.QUIT_BUTTON:
                    GameManager.instance.Quit();
                    break;

                case MenuManager.REDUCE_MUSIC_BUTTON:
                    menuManager.ReduceMusicLevel();
                    break;

                case MenuManager.INCREASE_MUSIC_BUTTON:
                    menuManager.IncreaseMusicLevel();
                    break;

                case MenuManager.REDUCE_SOUNDS_BUTTON:
                    menuManager.ReduceSoundsLevel();
                    break;

                case MenuManager.INCREASE_SOUNDS_BUTTON:
                    menuManager.IncreaseSoundsLevel();
                    break;

                case MenuManager.FULLSCREEN:
                    menuManager.ChangeFullscreen();
                    break;

                case MenuManager.RESO_720_480_BUTTON:
                    menuManager.ChangeResolution(720, 480, 0);
                    break;

                case MenuManager.RESO_1080_720_BUTTON:
                    menuManager.ChangeResolution(1080, 720, 1);
                    break;

                case MenuManager.RESO_1920_1080_BUTTON:
                    menuManager.ChangeResolution(1920, 1080, 2);
                    break;

                case MenuManager.RESO_4096_2304_BUTTON:
                    menuManager.ChangeResolution(4096, 2304, 3);
                    break;

                default:
                    break;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Comparer tous les tags
        for(int i = 0; i < menuManager.tagsInt.Length; i++)
        {
            if (collision.CompareTag(menuManager.tagsString[i]))
            {
                menuManager.HightLighted(collision.gameObject);
                menuManager.buttonSelected = menuManager.tagsInt[i];
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // Comparer tous les tags
        for (int i = 0; i < menuManager.tagsString.Length; i++)
        {
            if (collision.CompareTag(menuManager.tagsString[i]))
            {
                menuManager.NotHightLighted(collision.gameObject);
                menuManager.buttonSelected = MenuManager.NO_BUTTON_SELECTED;
            }
        }
    }
}

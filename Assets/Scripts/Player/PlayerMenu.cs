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
        if(Input.GetKeyDown(KeyCode.Return) && menuManager.buttonSelected != 0)
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

                default:
                    break;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Comparer tous les tags
        for(int i = 0; i< menuManager.tagsString.Length; i++)
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

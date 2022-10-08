using UnityEngine;

public class PlayerMenu : MonoBehaviour
{
    private GameManager gameManager;
    private CurrentSceneManager currentSceneManager;

    private const int NO_BUTTON_SELECTED = 0,
        PLAY_BUTTON = 1,
        SELECT_LEVEL_BUTTON = 2,
        OPTIONS_BUTTON = 3,
        QUIT_BUTTON = 4;
    private int buttonSelected = 0;

    private void Start()
    {
        gameManager = GameManager.instance;
        currentSceneManager = CurrentSceneManager.instance;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return) && buttonSelected != 0)
        {
            switch (buttonSelected)
            {
                case PLAY_BUTTON:
                    gameManager.LoadSceneWithName(currentSceneManager.nextSceneToLoad);
                    break;

                case SELECT_LEVEL_BUTTON:
                    gameManager.LoadSceneWithName("Select Level Menu");
                    break;

                case OPTIONS_BUTTON:
                    Debug.Log("Options menu");
                    break;

                case QUIT_BUTTON:
                    Debug.Log("Quit");
                    gameManager.Quit();
                    break;

                default:
                    break;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Play button
        if (collision.CompareTag("PlayButton"))
        {
            HightLighted(collision.gameObject);
            buttonSelected = PLAY_BUTTON;
        }

        // Select button
        else if (collision.CompareTag("SelectLevelButton"))
        {
            HightLighted(collision.gameObject);
            buttonSelected = SELECT_LEVEL_BUTTON;
        }

        // Options
        else if (collision.CompareTag("OptionsButton"))
        {
            HightLighted(collision.gameObject);
            buttonSelected = OPTIONS_BUTTON;
        }

        // Quit
        else if (collision.CompareTag("QuitButton"))
        {
            HightLighted(collision.gameObject);
            buttonSelected = QUIT_BUTTON;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        // Play button
        if (collision.CompareTag("PlayButton"))
        {
            NotHightLighted(collision.gameObject);
            buttonSelected = NO_BUTTON_SELECTED;
        }

        // Select button
        else if (collision.CompareTag("SelectLevelButton"))
        {
            NotHightLighted(collision.gameObject);
            buttonSelected = NO_BUTTON_SELECTED;
        }

        // Options
        else if (collision.CompareTag("OptionsButton"))
        {
            NotHightLighted(collision.gameObject);
            buttonSelected = NO_BUTTON_SELECTED;
        }

        // Quit
        else if (collision.CompareTag("QuitButton"))
        {
            NotHightLighted(collision.gameObject);
            buttonSelected = NO_BUTTON_SELECTED;
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
}

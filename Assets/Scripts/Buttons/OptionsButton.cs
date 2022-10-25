using UnityEngine;

public class OptionsButton : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject options;

    private bool triggerStay = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) && triggerStay)
        {
            CurrentSceneManager csm = CurrentSceneManager.instance;
            GameManager.instance.LoadSubScene(csm.optionsSubScene, csm.optionsPlayer, csm.optionsSpawnPlayer, false, true);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            MainMenuManager.instance.HightLighted(gameObject);
            triggerStay = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            MainMenuManager.instance.NotHightLighted(gameObject);
            triggerStay = false;
        }
    }
}
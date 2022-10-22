using UnityEngine;

public class OptionsButton : MonoBehaviour
{
    public Transform spawnOptions;
    public Transform playerOptions;

    public GameObject mainMenu;
    public GameObject options;

    private bool triggerStay = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) && triggerStay)
        {
            MainMenuManager.instance.NotHightLighted(gameObject);
            triggerStay = false;

            mainMenu.SetActive(!mainMenu.activeInHierarchy);
            options.SetActive(!mainMenu.activeInHierarchy);

            // Replacer le player
            playerOptions.position = spawnOptions.position;

            // Changer le focus sur le player
            CameraFollow.instance.ChangeFocusPlayer();
            CameraFollow.instance.SetPosOnPlayer();
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
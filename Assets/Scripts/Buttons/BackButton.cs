using UnityEngine;

public class BackButton : MonoBehaviour
{
    public Transform spawnMainMenu;
    public Transform playerMainMenu;

    public GameObject mainMenu;
    public GameObject options;

    private bool triggerStay = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) && triggerStay)
        {
            OptionsManager.instance.NotHightLighted(gameObject);
            triggerStay = false;

            mainMenu.SetActive(!mainMenu.activeInHierarchy);
            options.SetActive(!mainMenu.activeInHierarchy);

            // Replacer le player
            playerMainMenu.position = spawnMainMenu.position;

            // Changer le focus sur le player
            CameraFollow.instance.ChangeFocusPlayer();
            CameraFollow.instance.SetPosOnPlayer();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            OptionsManager.instance.HightLighted(gameObject);
            triggerStay = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            OptionsManager.instance.NotHightLighted(gameObject);
            triggerStay = false;
        }
    }
}
